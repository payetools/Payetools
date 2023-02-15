// Copyright (c) 2023 Paytools Foundation
//
// Licensed under the Apache License, Version 2.0 (the "License")~
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using Paytools.Common.Diagnostics;
using Paytools.Common.Model;
using Paytools.IncomeTax.ReferenceData;
using System.Diagnostics;

namespace Paytools.IncomeTax;

/// <summary>
/// Represents a calculator for calculating income tax based on tax code, taxable income and tax paid to date.  <see cref="TaxCalculator"/>
/// implements <see cref="ITaxCalculator"/>; access to tax calculators is through the <see cref="TaxCalculatorFactory"/>; in normal
/// use, TaxCalculators are not be created directly.  A TaxCalculator is specific to a given pay frequency and tax period, which
/// corresponds to an instance of a given <see cref="PayDate"/>.
/// </summary>
public class TaxCalculator : ITaxCalculator
{
    internal struct InternalTaxCalculationResult
    {
        public decimal TaxableSalary { get; init;  }
        public decimal TaxDueBeforeRegulatoryLimit { get; init; }
        public decimal TaxDue { get; set; }
        public int HighestApplicableTaxBandIndex { get; init; }
        public decimal IncomeAtHighestApplicableBand { get; init; }
        public decimal TaxAtHighestApplicableBand { get; init; }
    }

    public TaxPeriodBandwidthSet TaxBandwidths { get; init; }
    public PayFrequency PayFrequency{ get; init; }
    public int TaxPeriod{ get; init; }
    public int TaxPeriodCount{ get; init; }

    internal TaxCalculator(TaxBandwidthSet annualTaxBandwidths, PayFrequency payFrequency, int taxPeriod)
    {
        PayFrequency = payFrequency;
        TaxBandwidths = new TaxPeriodBandwidthSet(annualTaxBandwidths, payFrequency, taxPeriod);
        TaxPeriod = taxPeriod;
        TaxPeriodCount = payFrequency.GetStandardTaxPeriodCount(); ;
    }

    public ITaxCalculationResult Calculate(decimal totalTaxableSalaryInPeriod,
        decimal benefitsInKind,
        TaxCode taxCode,
        decimal taxableSalaryYearToDate,
        decimal taxPaidYearToDate,
        decimal taxUnpaidDueToRegulatoryLimit = 0.0m)
    {
        InternalTaxCalculationResult internalResult = taxCode switch
        {
            // NB Although '0T' is a fixed code, it is just a special case of the regular calculation,
            // where the allowance is zero
            { IsFixedCode: true } and not { TaxTreatment: TaxTreatment._0T } =>
                CalculateTaxDueOnFixedTaxCode(totalTaxableSalaryInPeriod, taxCode, taxableSalaryYearToDate,
                    taxPaidYearToDate, benefitsInKind, taxUnpaidDueToRegulatoryLimit),

            _ => CalculateTaxDueOnNonFixedTaxCode(totalTaxableSalaryInPeriod, taxCode, taxableSalaryYearToDate,
                    benefitsInKind, taxPaidYearToDate, taxUnpaidDueToRegulatoryLimit)
        };

        return new TaxCalculationResult(this,
            internalResult.HighestApplicableTaxBandIndex,
            internalResult.IncomeAtHighestApplicableBand,
            internalResult.TaxAtHighestApplicableBand,
            internalResult.TaxableSalary,
            taxCode,
            taxableSalaryYearToDate,
            taxPaidYearToDate,
            taxUnpaidDueToRegulatoryLimit,
            internalResult.TaxDue);
    }

    private InternalTaxCalculationResult CalculateTaxDueOnNonFixedTaxCode(decimal totalTaxableSalaryInPeriod,
        TaxCode taxCode, 
        decimal taxableSalaryYearToDate,
        decimal benefitsInKind,
        decimal taxPaidYearToDate,
        decimal taxUnpaidDueToRegulatoryLimit)
    {
        var totalTaxableSalaryYtd = totalTaxableSalaryInPeriod + taxableSalaryYearToDate;

        var taxFreePayToEndOfPeriod = taxCode.GetTaxFreePayForPeriod(TaxPeriod, TaxPeriodCount);

        InternalTaxCalculationResult internalResult = taxCode.IsNonCumulative ?
                    CalculateTaxDueNonCumulative(totalTaxableSalaryInPeriod, benefitsInKind, taxFreePayToEndOfPeriod) :
                    CalculateTaxDueCumulative(totalTaxableSalaryInPeriod, benefitsInKind, taxableSalaryYearToDate, taxPaidYearToDate, taxFreePayToEndOfPeriod);

        internalResult.TaxDue = internalResult.TaxDueBeforeRegulatoryLimit > 0 ? 
            Math.Min(internalResult.TaxDueBeforeRegulatoryLimit, 0.5m * (totalTaxableSalaryInPeriod - benefitsInKind)) : 
            internalResult.TaxDueBeforeRegulatoryLimit;

        return internalResult;
    }

    private InternalTaxCalculationResult CalculateTaxDueCumulative(decimal totalTaxableSalaryInPeriod,
            decimal benefitsInKind,
            decimal taxableSalaryYearToDate,
            decimal taxPaidYearToDate,
            decimal taxFreePayToEndOfPeriod,
            decimal taxUnpaidDueToRegulatoryLimit = 0.0m)
    {
        var totalTaxableSalaryYtd = totalTaxableSalaryInPeriod + taxableSalaryYearToDate;

        // TODO: Process possible refund
        //if (totalTaxableSalaryYtd <= taxFreePayToEndOfPeriod)
        //    return new TaxCalculation(null, totalTaxableSalaryInPeriod, taxCode, totalTaxableSalaryYtd,
        //        taxPaidYearToDate, _taxPeriodCount, taxUnpaidDueToRegulatoryLimit, 0.0m);

        var taxableSalaryAfterAllowance = Math.Floor(totalTaxableSalaryYtd - taxFreePayToEndOfPeriod);

        // This is the "magic" - we search for the highest applicable tax band (using FirstOrDefault with criteria) and
        // calculate the portion of tax that is applicable to that band.  For all other bands below, we are applying 
        // 100% of the band, and all bands above are irrelevant.
        TaxPeriodBandwidthEntry applicableEntry = TaxBandwidths.TaxBandwidthEntries
            .FirstOrDefault(entry =>
                taxableSalaryAfterAllowance <= decimal.Round(entry.CumulativeBandwidth, 0, MidpointRounding.ToPositiveInfinity) || entry.IsTopBand) ??
                    throw new InvalidReferenceDataException($"No applicable tax bandwidth found for taxable pay of {taxableSalaryAfterAllowance:N2}");

        var cumulativeBandwidthBelow = applicableEntry?.BandWidthEntryBelow?.CumulativeBandwidth ?? 0.0m;
        var cumulativeTaxBelow = applicableEntry?.BandWidthEntryBelow?.CumulativeTax ?? 0.0m;

        var applicableRate = applicableEntry?.Rate ?? 0.0m;

        Debug.WriteLine("Cumlative tax calculation: totalTaxableSalaryYtd = {0}, taxFreePayToEndOfPeriod = {1}, taxableSalaryAfterAllowance = {2}, applicableRate = {3}",
            totalTaxableSalaryYtd, taxFreePayToEndOfPeriod, taxableSalaryAfterAllowance, applicableRate);

        var taxableSalaryAfterAllowanceRounded = Math.Floor(taxableSalaryAfterAllowance);
        var taxableSalaryAtThisBand = taxableSalaryAfterAllowanceRounded - cumulativeBandwidthBelow;
        var taxAtThisBand = taxableSalaryAtThisBand * applicableRate;
        var taxDueToEndOfPeriod = cumulativeTaxBelow + taxAtThisBand;

        var taxDue = decimal.Round(taxDueToEndOfPeriod, 2, MidpointRounding.ToZero);

        var taxPayable = taxDue - taxPaidYearToDate;

        return new InternalTaxCalculationResult()
        {
            TaxDueBeforeRegulatoryLimit = taxPayable,
            HighestApplicableTaxBandIndex = applicableEntry?.EntryIndex ?? 0,
            IncomeAtHighestApplicableBand = taxableSalaryAtThisBand,
            TaxAtHighestApplicableBand = taxAtThisBand
        };
    }

    private InternalTaxCalculationResult CalculateTaxDueNonCumulative(decimal totalTaxableSalaryInPeriod,
        decimal benefitsInKind,
        decimal taxFreePayToEndOfPeriod,
        decimal taxUnpaidDueToRegulatoryLimit = 0.0m)
    {
        // TODO: Process possible refund
        //if (totalTaxableSalaryYtd <= taxFreePayToEndOfPeriod)
        //    return new TaxCalculation(null, totalTaxableSalaryInPeriod, taxCode, totalTaxableSalaryYtd,
        //        taxPaidYearToDate, _taxPeriodCount, taxUnpaidDueToRegulatoryLimit, 0.0m);

        var taxableSalaryAfterAllowance = totalTaxableSalaryInPeriod - taxFreePayToEndOfPeriod;

        // As per above, this is the "magic" - we search for the highest applicable tax band (using FirstOrDefault with
        // criteria) and calculate the portion of tax that is applicable to that band.  For all other bands below, we are
        // applying 100% of the band, and all bands above are irrelevant.
        TaxPeriodBandwidthEntry applicableEntry = TaxBandwidths.TaxBandwidthEntries
            .FirstOrDefault(entry =>
                taxableSalaryAfterAllowance <= decimal.Round(entry.Period1CumulativeBandwidth, 0, MidpointRounding.ToPositiveInfinity) || entry.IsTopBand) ??
                    throw new InvalidReferenceDataException($"No applicable tax bandwidth found for taxable pay of {taxableSalaryAfterAllowance:N2}");

        var cumulativeBandwidthBelow = applicableEntry?.BandWidthEntryBelow?.Period1CumulativeBandwidth ?? 0.0m;
        var cumulativeTaxBelow = applicableEntry?.BandWidthEntryBelow?.Period1CumulativeTax ?? 0.0m;

        var applicableRate = applicableEntry?.Rate ?? 0.0m;

        var taxableSalaryAfterAllowanceRounded = Math.Floor(taxableSalaryAfterAllowance);

        var taxableSalaryAtThisBand = taxableSalaryAfterAllowanceRounded - cumulativeBandwidthBelow;
        var taxAtThisBand = taxableSalaryAtThisBand * applicableRate;
        var taxDue = cumulativeTaxBelow + taxAtThisBand;

        var taxPayable = decimal.Round(taxDue, 2, MidpointRounding.ToZero);

        Debug.WriteLine("Non-cumulative tax calculation: totalTaxableSalaryInPeriod = {0}, taxFreePayToEndOfPeriod = {1}, taxableSalaryAfterAllowance = {2}, applicableRate = {3}, taxPayable = {4}",
            totalTaxableSalaryInPeriod, taxFreePayToEndOfPeriod, taxableSalaryAfterAllowance, applicableRate, taxPayable);

        return new InternalTaxCalculationResult()
        {
            TaxDueBeforeRegulatoryLimit = taxPayable,
            HighestApplicableTaxBandIndex = applicableEntry?.EntryIndex ?? 0,
            IncomeAtHighestApplicableBand = taxableSalaryAtThisBand,
            TaxAtHighestApplicableBand = taxAtThisBand
        };
    }

    // Used for all fixed tax codes _except_ 0T, which is treated like a normal code with zero allowance.
    private InternalTaxCalculationResult CalculateTaxDueOnFixedTaxCode(decimal taxableSalary,
        TaxCode taxCode,
        decimal taxableSalaryYearToDate,
        decimal taxPaidYearToDate,
        decimal benefitsInKind,
        decimal taxUnpaidDueToRegulatoryLimit = 0.0m)
    {
        var effectiveTaxableSalary = taxableSalary;
        var bandIndex = 0;
        var taxDue = 0.0m;
        var taxRate = 0.0m;

        switch (taxCode.TaxTreatment)
        {
            case TaxTreatment.NT:
                effectiveTaxableSalary = 0.0m;
                taxDue = taxCode.IsNonCumulative ? 0.0m : -taxPaidYearToDate;
                break;

            case TaxTreatment.BR:
            case TaxTreatment.D0:
            case TaxTreatment.D1:
            case TaxTreatment.D2:
                bandIndex = taxCode.TaxTreatment.GetBandIndex();
                if (bandIndex >= TaxBandwidths.TaxBandwidthEntries.Length)
                    throw new InconsistentDataException($"Tax code {taxCode.TaxTreatment} invalid for tax year/countries combination");
                taxRate = TaxBandwidths.TaxBandwidthEntries[bandIndex].Rate;
                taxDue = taxCode.IsNonCumulative ?
                    Math.Round(taxableSalary, 0, MidpointRounding.ToZero) * taxRate :
                    Math.Round(taxableSalaryYearToDate + taxableSalary, 0, MidpointRounding.ToZero) * taxRate - taxPaidYearToDate;
                break;
        }

        var internalResult = new InternalTaxCalculationResult()
        {
            TaxDueBeforeRegulatoryLimit = taxDue,
            HighestApplicableTaxBandIndex = bandIndex,
            IncomeAtHighestApplicableBand = effectiveTaxableSalary,
            TaxAtHighestApplicableBand = taxRate
        };

        internalResult.TaxDue = internalResult.TaxDueBeforeRegulatoryLimit > 0 ?
            Math.Min(internalResult.TaxDueBeforeRegulatoryLimit, 0.5m * (taxableSalary - benefitsInKind)) :
            internalResult.TaxDueBeforeRegulatoryLimit;

        return internalResult;
    }
}