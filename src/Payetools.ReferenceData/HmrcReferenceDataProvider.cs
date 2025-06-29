// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.AttachmentOrders.Model;
using Payetools.AttachmentOrders.ReferenceData;
using Payetools.Common.Diagnostics;
using Payetools.Common.Model;
using Payetools.IncomeTax.ReferenceData;
using Payetools.NationalInsurance.ReferenceData;
using Payetools.NationalMinimumWage.ReferenceData;
using Payetools.ReferenceData.Employer;
using Payetools.ReferenceData.IncomeTax;
using Payetools.ReferenceData.NationalInsurance;
using Payetools.ReferenceData.NationalMinimumWage;
using Payetools.ReferenceData.Pensions;
using Payetools.ReferenceData.StudentLoans;
using Payetools.StudentLoans.ReferenceData;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Text;

namespace Payetools.ReferenceData;

/// <summary>
/// Provider of HMRC reference data.  Should be instantiated via the <see cref="HmrcReferenceDataProviderFactory"/>.
/// </summary>
public class HmrcReferenceDataProvider : IHmrcReferenceDataProvider
{
    private readonly Dictionary<TaxYearEnding, HmrcTaxYearReferenceDataSet> _referenceDataSets;

    /// <summary>
    /// Gets the health of this reference data provider as human-readable string.
    /// </summary>
    public string Health { get; }

    /// <summary>
    /// Initialises a new intance of <see cref="IHmrcReferenceDataProvider"/>.
    /// </summary>
    internal HmrcReferenceDataProvider()
    {
        _referenceDataSets = [];
        Health = "No tax years added";
    }

    /// <summary>
    /// Initialised a new instance of <see cref="HmrcReferenceDataProvider"/> using the supplied data sets.
    /// </summary>
    /// <param name="dataSets">IEnumerable of <see cref="HmrcTaxYearReferenceDataSet"/>s to initialise this
    /// provider with.</param>
    /// <remarks>Although this constructor is public, it is recommended that instances of this class be
    /// initialised through a <see cref="HmrcReferenceDataProviderFactory"/>.</remarks>
    public HmrcReferenceDataProvider(IEnumerable<HmrcTaxYearReferenceDataSet> dataSets)
    {
        _referenceDataSets = [];

        var health = new List<string>();

        foreach (var dataSet in dataSets)
        {
            health.Add(TryAdd(dataSet, out var errorMessage) ?
                $"{dataSet.ApplicableTaxYearEnding}:OK" :
                $"{dataSet.ApplicableTaxYearEnding}:Data set failed validation; {errorMessage}");
        }

        Health = string.Join('|', health);
    }

    /// <summary>
    /// Retrieves the tax bands for a given tax year in the form of a dictionary (<see cref="ReadOnlyDictionary{CountriesForTaxPurposes, TaxBandwidthSet}"/>)
    /// keyed on tax regime, i.e., <see cref="CountriesForTaxPurposes"/>.
    /// </summary>
    /// <param name="taxYear">Desired tax year.</param>
    /// <param name="payFrequency">Pay frequency pertaining.  Used in conjunction with the taxPeriod parameter to
    /// determine the applicable date (in case of in-year tax changes).</param>
    /// <param name="taxPeriod">Tax period, e.g., 1 for month 1.  Currently ignored on the assumption that in-year
    /// tax changes are not anticipated but provided for future .</param>
    /// <returns>ReadOnlyDictionary of <see cref="TaxBandwidthSet"/>'s keyed on tax regime.</returns>
    /// <remarks>Although ReadOnlyDictionary is not guaranteed to be thread-safe, in the current implementation the
    /// underlying Dictionary is guaranteed not to change, so thread-safety can be assumed.</remarks>
    public ReadOnlyDictionary<CountriesForTaxPurposes, TaxBandwidthSet> GetTaxBandsForTaxYearAndPeriod(
        TaxYear taxYear,
        PayFrequency payFrequency,
        int taxPeriod)
    {
        var referenceDataSet = GetReferenceDataSetForTaxYear(taxYear);

        var taxBands = FindApplicableEntry<IncomeTaxReferenceDataEntry>(referenceDataSet.IncomeTax,
            taxYear, payFrequency, taxPeriod);

        return new ReadOnlyDictionary<CountriesForTaxPurposes, TaxBandwidthSet>(taxBands.TaxEntries
            .ToDictionary(e => e.ApplicableCountries, e => new TaxBandwidthSet(e.GetTaxBandwidthEntries())));
    }

    /// <summary>
    /// Gets a read-only dictionary that maps <see cref="NiCategory"/> values to the set of rates to be applied
    /// for the specified pay date.
    /// </summary>
    /// <param name="payDate">Applicable pay date.</param>
    /// <returns>Read-only dictionary that maps <see cref="NiCategory"/> values to the appropriate set of rates for
    /// the specified point in time.</returns>
    public ReadOnlyDictionary<NiCategory, INiCategoryRatesEntry> GetNiRatesForPayDate(PayDate payDate)
    {
        var referenceDataSet = GetReferenceDataSetForTaxYear(payDate.TaxYear);

        var niReferenceDataEntry = FindApplicableEntry<NiReferenceDataEntry>(referenceDataSet.NationalInsurance, payDate);

        var rates = MakeNiCategoryRatesEntries(niReferenceDataEntry.EmployerRates, niReferenceDataEntry.EmployeeRates);

        return new ReadOnlyDictionary<NiCategory, INiCategoryRatesEntry>(rates);
    }

    /// <summary>
    /// Gets a read-only dictionary that maps <see cref="NiCategory"/> values to the set of rates to be applied
    /// for the specified pay date, for directors.  (For most tax years, this method returns null, but if
    /// there have been in-year changes, specific directors' rates may apply.)
    /// </summary>
    /// <param name="payDate">Applicable pay date.</param>
    /// <returns>Read-only dictionary that maps <see cref="NiCategory"/> values to the appropriate set of rates for
    /// the specified point in time.  If specific rates apply for directors, theses are returned, otherwise the
    /// regular employee/employer rates are returned.</returns>
    public ReadOnlyDictionary<NiCategory, INiCategoryRatesEntry> GetDirectorsNiRatesForPayDate(PayDate payDate)
    {
        var referenceDataSet = GetReferenceDataSetForTaxYear(payDate.TaxYear);

        var niReferenceDataEntry = FindApplicableEntry<NiReferenceDataEntry>(referenceDataSet.NationalInsurance, payDate);

        var employerRates = niReferenceDataEntry.DirectorEmployerRates ?? niReferenceDataEntry.EmployerRates;
        var employeeRates = niReferenceDataEntry.DirectorEmployeeRates ?? niReferenceDataEntry.EmployeeRates;

        var rates = MakeNiCategoryRatesEntries(employerRates, employeeRates);

        return new ReadOnlyDictionary<NiCategory, INiCategoryRatesEntry>(rates);
    }

    /// <summary>
    /// Gets the NI thresholds for the specified pay date.
    /// </summary>
    /// <param name="payDate">Applicable pay date.</param>
    /// <returns>An instance of <see cref="INiThresholdSet"/> containing the thresholds for the specified point
    /// in time.</returns>
    public INiThresholdSet GetNiThresholdsForPayDate(PayDate payDate)
    {
        var referenceDataSet = GetReferenceDataSetForTaxYear(payDate.TaxYear);

        var niReferenceDataEntry = FindApplicableEntry<NiReferenceDataEntry>(referenceDataSet.NationalInsurance, payDate);

        var thresholds = niReferenceDataEntry.NiThresholds.OrderBy(nit => nit.ThresholdType).Select(nit => new NiThresholdEntry()
        {
            ThresholdType = nit.ThresholdType,
            ThresholdValuePerYear = nit.ThresholdValuePerYear
        }).ToImmutableArray<INiThresholdEntry>();

        return new NiThresholdSet(thresholds, payDate.TaxYear.TaxYearEnding);
    }

    /// <summary>
    /// Gets the thresholds for Qualifying Earnings for the specified tax year and tax period, as denoted by the
    /// supplied pay frequency and pay period.
    /// </summary>
    /// <param name="taxYear">Applicable tax year.</param>
    /// <param name="payFrequency">Applicable pay frequency.</param>
    /// <param name="taxPeriod">Applicable tax period.</param>
    /// <returns>A tuple containing the lower and upper thresholds for the specified pay frequency and point in time.</returns>
    public (decimal LowerLimit, decimal UpperLimit) GetThresholdsForQualifyingEarnings(TaxYear taxYear, PayFrequency payFrequency, int taxPeriod)
    {
        var referenceDataSet = GetReferenceDataSetForTaxYear(taxYear);

        var pensionsReferenceDataEntry = FindApplicableEntry<PensionsReferenceDataSet>(referenceDataSet.Pensions,
            taxYear, payFrequency, taxPeriod);

        return (pensionsReferenceDataEntry.QualifyingEarningsLowerLevel.GetThresholdForPayFrequency(payFrequency),
            pensionsReferenceDataEntry.QualifyingEarningsUpperLevel.GetThresholdForPayFrequency(payFrequency));
    }

    /// <summary>
    /// Gets the basic rate of tax applicable across all tax regimes for relief at source pension contributions, for the specified
    /// tax year.  (As at the time of writing, one basic rate of tax is used across all jurisdictions in spite of the fact that
    /// some have a lower basic rate of tax.)
    /// </summary>
    /// <param name="taxYear">Applicable tax year.</param>
    /// <param name="payFrequency">Applicable pay frequency.  Only used if there has been an in-year change.</param>
    /// <param name="taxPeriod">Applicable tax period.  Only used if there has been an in-year change.</param>
    /// <returns>Basic rate of tax applicable for the tax year.</returns>
    public decimal GetBasicRateOfTaxForTaxRelief(TaxYear taxYear, PayFrequency payFrequency, int taxPeriod)
    {
        var referenceDataSet = GetReferenceDataSetForTaxYear(taxYear);

        var pensionsReferenceDataEntry = FindApplicableEntry<PensionsReferenceDataSet>(referenceDataSet.Pensions,
            taxYear, payFrequency, taxPeriod);

        return pensionsReferenceDataEntry.BasicRateOfTaxForTaxRelief;
    }

    /// <summary>
    /// Gets the NMW/NLW levels for the specified tax year and tax period, as denoted by the supplied pay frequency
    /// and pay period.
    /// </summary>
    /// <param name="taxYear">Applicable tax year.</param>
    /// <param name="payFrequency">Applicable pay frequency.</param>
    /// <param name="taxPeriod">Applicable tax period.</param>
    /// <returns>An instance of <see cref="INmwLevelSet"/> containing the levels for the specified point
    /// in time.</returns>
    public INmwLevelSet GetNmwLevelsForTaxYearAndPeriod(TaxYear taxYear, PayFrequency payFrequency, int taxPeriod)
    {
        var referenceDataSet = GetReferenceDataSetForTaxYear(taxYear);

        return FindApplicableEntry<NmwReferenceDataEntry>(referenceDataSet.NationalMinimumWage,
            taxYear, payFrequency, taxPeriod);
    }

    /// <summary>
    /// Gets the set of annual thresholds to be applied for a given tax year and tax period.
    /// </summary>
    /// <param name="taxYear">Applicable tax year.</param>
    /// <param name="payFrequency">Applicable pay frequency.</param>
    /// <param name="taxPeriod">Applicable tax period.</param>
    /// <returns>An implementation of <see cref="IStudentLoanThresholdSet"/> that provides the appropriate set of annual
    /// thresholds for the specified point.</returns>
    public IStudentLoanThresholdSet GetStudentLoanThresholdsForTaxYearAndPeriod(TaxYear taxYear, PayFrequency payFrequency, int taxPeriod)
    {
        var referenceDataSet = GetReferenceDataSetForTaxYear(taxYear);

        var thresholdsSet = FindApplicableEntry<StudentLoanReferenceDataEntry>(referenceDataSet.StudentLoans,
            taxYear, payFrequency, taxPeriod);

        return new StudentLoanThresholdSet()
        {
            Plan1PerPeriodThreshold = thresholdsSet.Plan1Thresholds.ThresholdValuePerYear,
            Plan2PerPeriodThreshold = thresholdsSet.Plan2Thresholds.ThresholdValuePerYear,
            Plan4PerPeriodThreshold = thresholdsSet.Plan4Thresholds.ThresholdValuePerYear,
            PostGradPerPeriodThreshold = thresholdsSet.PostGradThresholds.ThresholdValuePerYear
        };
    }

    /// <summary>
    /// Gets the student and post graduate deduction rates for the specified tax year and tax period, as denoted
    /// by the supplied pay frequency.
    /// and pay period.
    /// </summary>
    /// <param name="taxYear">Applicable tax year.</param>
    /// <param name="payFrequency">Applicable pay frequency.</param>
    /// <param name="taxPeriod">Applicable tax period.</param>
    /// <returns>An instance of <see cref="IStudentLoanRateSet"/> containing the rates for the specified point
    /// in time.</returns>
    public IStudentLoanRateSet GetStudentLoanRatesForTaxYearAndPeriod(TaxYear taxYear, PayFrequency payFrequency, int taxPeriod)
    {
        var referenceDataSet = GetReferenceDataSetForTaxYear(taxYear);

        return FindApplicableEntry<StudentLoanReferenceDataEntry>(referenceDataSet.StudentLoans,
            taxYear, payFrequency, taxPeriod).DeductionRates;
    }

    /// <summary>
    /// Gets reference information about Employment Allowance.
    /// </summary>
    /// <param name="date">Applicable date for this reference information request.</param>
    /// <returns>Reference data information on Employment Allowance for the specified date.</returns>
    public EmploymentAllowanceInfo GetEmploymentAllowanceInfoForDate(DateOnly date) =>
        throw new NotImplementedException();

    /// <summary>
    /// Gets reference information about reclaiming some or all of statutory payments (e.g., SMP, SPP).
    /// </summary>
    /// <param name="date">Applicable date for this reference information request.</param>
    /// <returns>Reference data information on reclaiming statutory payments for the specified date.</returns>
    public StatutoryPaymentReclaimInfo GetStatutoryPaymentReclaimInfoForDate(DateOnly date) =>
        throw new NotImplementedException();

    /// <summary>
    /// Gets reference information about the Apprentice Levy.
    /// </summary>
    /// <param name="date">Applicable date for this reference information request.</param>
    /// <returns>Reference data information on the Apprentice Levy for the specified date.</returns>
    public ApprenticeLevyInfo GetApprenticeLevyInfoForDate(DateOnly date) =>
        throw new NotImplementedException();

    internal bool TryAdd(HmrcTaxYearReferenceDataSet referenceDataSet, out string? errorMessage)
    {
        errorMessage = null;

        var (isValid, errorText) = ValidateReferenceData(referenceDataSet);

        if (!isValid)
        {
            errorMessage = errorText;

            return false;
        }

        return _referenceDataSets.TryAdd(referenceDataSet.ApplicableTaxYearEnding, referenceDataSet);
    }

    private HmrcTaxYearReferenceDataSet GetReferenceDataSetForTaxYear(TaxYear taxYear)
    {
        if (!_referenceDataSets.TryGetValue(taxYear.TaxYearEnding, out var referenceDataSet))
            throw new ArgumentOutOfRangeException(nameof(taxYear), $"No reference data found for tax year {taxYear}");

        if (referenceDataSet == null ||
            referenceDataSet?.IncomeTax == null ||
            referenceDataSet?.NationalInsurance == null ||
            referenceDataSet?.Pensions == null ||
            referenceDataSet?.NationalMinimumWage == null ||
            referenceDataSet?.StudentLoans == null)
            throw new InvalidReferenceDataException($"Reference data for tax year ending {taxYear.EndOfTaxYear} is invalid or incomplete");

        return referenceDataSet;
    }

    private static T FindApplicableEntry<T>(IReadOnlyList<IApplicableFromTill> entries, PayDate payDate) =>
        (T)(entries
                .FirstOrDefault(ite => ite.ApplicableFrom <= payDate.Date && payDate.Date <= ite.ApplicableTill) ??
                    throw new InvalidReferenceDataException($"Unable to find reference data entry for specified pay date {payDate} (Type: '{typeof(T)}')"));

    private static T FindApplicableEntry<T>(IReadOnlyList<IApplicableFromTill> entries, TaxYear taxYear, PayFrequency payFrequency, int taxPeriod)
    {
        var endDateOfPeriod = taxYear.GetLastDayOfTaxPeriod(payFrequency, taxPeriod);

        return (T)(entries
            .FirstOrDefault(ite => ite.ApplicableFrom <= endDateOfPeriod && endDateOfPeriod <= ite.ApplicableTill) ??
                throw new InvalidReferenceDataException($"Unable to find reference data entry for specified tax year, pay frequency and pay period (Type: '{typeof(T)}')"));
    }

    private static Dictionary<NiCategory, INiCategoryRatesEntry> MakeNiCategoryRatesEntries(
        ImmutableArray<NiEmployerRatesEntry> employerRateEntries,
        ImmutableArray<NiEmployeeRatesEntry> employeeRateEntries)
    {
        var rateEntries = new Dictionary<NiCategory, NiCategoryRatesEntry>();

        foreach (var erEntry in employerRateEntries)
        {
            foreach (var erCategory in erEntry.NiCategories)
            {
                if (!rateEntries.TryGetValue(erCategory, out NiCategoryRatesEntry? rateEntry))
                {
                    rateEntry = new NiCategoryRatesEntry(erCategory);
                    rateEntries.Add(erCategory, rateEntry);
                }

                rateEntry.EmployerRateLELtoST = erEntry.ForEarningsAtOrAboveLELUpToAndIncludingST;
                rateEntry.EmployerRateSTtoFUST = erEntry.ForEarningsAboveSTUpToAndIncludingFUST;
                rateEntry.EmployerRateFUSTtoUEL = erEntry.ForEarningsAboveFUSTUpToAndIncludingUELOrUST;
                rateEntry.EmployerRateAboveUEL = erEntry.ForEarningsAboveUELOrUST;
            }
        }

        foreach (var eeEntry in employeeRateEntries)
        {
            foreach (var erCategory in eeEntry.NiCategories)
            {
                if (!rateEntries.TryGetValue(erCategory, out NiCategoryRatesEntry? rateEntry))
                {
                    rateEntry = new NiCategoryRatesEntry(erCategory);
                    rateEntries.Add(erCategory, rateEntry);
                }

                rateEntry.EmployeeRateToPT = eeEntry.ForEarningsAtOrAboveLELUpTAndIncludingPT;
                rateEntry.EmployeeRatePTToUEL = eeEntry.ForEarningsAbovePTUpToAndIncludingUEL;
                rateEntry.EmployeeRateAboveUEL = eeEntry.ForEarningsAboveUEL;
            }
        }

        return rateEntries
            .Select(kv => new KeyValuePair<NiCategory, INiCategoryRatesEntry>(kv.Key, kv.Value))
            .ToDictionary(kv => kv.Key, kv => kv.Value);
    }

    private static (bool IsValid, string? ErrorMessage) ValidateReferenceData(HmrcTaxYearReferenceDataSet? referenceDataSet)
    {
        bool empty = referenceDataSet == null;
        bool noIncomeTax = !empty && referenceDataSet?.IncomeTax == null;
        bool noNi = !empty && referenceDataSet?.NationalInsurance == null;
        bool noPensions = !empty && referenceDataSet?.Pensions == null;
        bool noNmw = !empty && referenceDataSet?.NationalMinimumWage == null;
        bool noStudentLoan = !empty && referenceDataSet?.StudentLoans == null;

        bool isValid = !(empty || noIncomeTax || noNi || noPensions || noNmw || noStudentLoan);

        if (isValid)
            return (true, null);

        var errorMessage = new StringBuilder();

        return (false, errorMessage.ToString());
    }

    /// <summary>
    /// Gets the rate table for the specified countries, calculation type, and applicable date range.
    /// </summary>
    /// <param name="taxYear">Applicable tax year.</param>
    /// <param name="jurisdiction">Jurisdiction that this set of rates applies to.</param>
    /// <param name="calculationType">Attachment order calculation type to match.</param>
    /// <param name="applicabilityDate">Date that the attachment order applies, typically the issue
    /// date.</param>
    /// <returns>The rate table for the specified jurisdiction, calculation type and attachment
    /// order issue date.</returns>
    public ImmutableArray<AttachmentOrderRateTableEntry>? GetAttachmentOrderRateTable(
        TaxYear taxYear,
        CountriesForTaxPurposes jurisdiction,
        AttachmentOrderCalculationType calculationType,
        DateOnly applicabilityDate)
    {
        var referenceDataSet = GetReferenceDataSetForTaxYear(taxYear);

        var entry = referenceDataSet.AttachmentOrders.FirstOrDefault(ao =>
            (ao.ApplicableCountries & jurisdiction) == jurisdiction &&
            ao.ApplicableFrom <= applicabilityDate &&
            ao.ApplicableTill >= applicabilityDate &&
            ao.CalculationType == calculationType) ??
            throw new InvalidReferenceDataException($"Unable to find attachment order reference data for jurisdiction '{jurisdiction}' and calculation type '{calculationType}' with applicability date '{applicabilityDate}'");

        return entry.RateTable;
    }
}