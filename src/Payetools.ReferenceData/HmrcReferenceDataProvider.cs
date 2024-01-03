﻿// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Diagnostics;
using Payetools.Common.Model;
using Payetools.IncomeTax.ReferenceData;
using Payetools.NationalInsurance.ReferenceData;
using Payetools.NationalMinimumWage.ReferenceData;
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

internal class HmrcReferenceDataProvider : IHmrcReferenceDataProvider
{
    private readonly Dictionary<TaxYearEnding, HmrcTaxYearReferenceDataSet> _referenceDataSets;

    /// <summary>
    /// Gets or sets (internal only) the health of this reference data provider as human-readable string.
    /// </summary>
    public string Health { get; internal set; }

    /// <summary>
    /// Initialises a new intance of <see cref="IHmrcReferenceDataProvider"/>.
    /// </summary>
    internal HmrcReferenceDataProvider()
    {
        _referenceDataSets = new Dictionary<TaxYearEnding, HmrcTaxYearReferenceDataSet>();
        Health = "No tax years added";
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
        }).ToImmutableList<INiThresholdEntry>();

        return new NiThresholdSet(thresholds);
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

    internal bool TryAdd(HmrcTaxYearReferenceDataSet referenceDataSet)
    {
        var (isValid, errorMessage) = ValidateReferenceData(referenceDataSet);

        if (!isValid)
        {
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
        ImmutableList<NiEmployerRatesEntry> employerRateEntries,
        ImmutableList<NiEmployeeRatesEntry> employeeRateEntries)
    {
        var rateEntries = new Dictionary<NiCategory, NiCategoryRatesEntry>();

        employerRateEntries.ForEach(erEntry =>
        {
            erEntry.NiCategories.ForEach(erCategory =>
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
            });
        });

        employeeRateEntries.ForEach(eeEntry =>
        {
            eeEntry.NiCategories.ForEach(erCategory =>
            {
                if (!rateEntries.TryGetValue(erCategory, out NiCategoryRatesEntry? rateEntry))
                {
                    rateEntry = new NiCategoryRatesEntry(erCategory);
                    rateEntries.Add(erCategory, rateEntry);
                }

                rateEntry.EmployeeRateToPT = eeEntry.ForEarningsAtOrAboveLELUpTAndIncludingPT;
                rateEntry.EmployeeRatePTToUEL = eeEntry.ForEarningsAbovePTUpToAndIncludingUEL;
                rateEntry.EmployeeRateAboveUEL = eeEntry.ForEarningsAboveUEL;
            });
        });

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
}