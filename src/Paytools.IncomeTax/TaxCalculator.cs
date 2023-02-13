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

using Paytools.Common;
using Paytools.Common.Diagnostics;
using Paytools.Common.Model;
using Paytools.IncomeTax.ReferenceData;
using System.Diagnostics;

namespace Paytools.IncomeTax;

public class TaxCalculator : ITaxCalculator
{
    private readonly TaxPeriodBandwidthSet _taxBandwidths;
    // TODO: Sort this mess out
    private readonly PayFrequency _payFrequency;
    private readonly int _taxPeriod;
    private readonly int _taxPeriodCount;

    public TaxCalculator(TaxBandwidthSet annualTaxBandwidths, PayFrequency payFrequency, int taxPeriod)
    {
        _payFrequency = payFrequency;
        _taxBandwidths = new TaxPeriodBandwidthSet(annualTaxBandwidths, payFrequency, taxPeriod);
        _taxPeriod = taxPeriod;
        _taxPeriodCount = payFrequency.GetStandardTaxPeriodCount(); ;
    }

    public TaxCalculationResult Calculate(decimal totalTaxableSalaryInPeriod,
        decimal benefitsInKind,
        TaxCode taxCode,
        decimal taxableSalaryYearToDate,
        decimal taxPaidYearToDate,
        decimal taxUnpaidDueToRegulatoryLimit = 0.0m)
    {
        // Although '0T' is a fixed code, it is just a special case of the regular calculation, where the allowance is zero
        if (taxCode.IsFixedCode && taxCode.TaxTreatment != TaxTreatment._0T)
            return CalculateFixedTaxCode(totalTaxableSalaryInPeriod, taxCode, taxableSalaryYearToDate,
                taxPaidYearToDate, taxUnpaidDueToRegulatoryLimit);

        var totalTaxableSalaryYtd = totalTaxableSalaryInPeriod + taxableSalaryYearToDate;

        var taxFreePayToEndOfPeriod = taxCode.GetTaxFreePayForPeriod(_taxPeriod, _taxPeriodCount);

        decimal taxPayable = taxCode.IsNonCumulative ?
            CalculateTaxDueNonCumulative(totalTaxableSalaryInPeriod, benefitsInKind, taxFreePayToEndOfPeriod) :
            CalculateTaxDueCumulative(totalTaxableSalaryInPeriod, benefitsInKind, taxableSalaryYearToDate, taxPaidYearToDate, taxFreePayToEndOfPeriod);

        var finalTaxPayable = taxPayable > 0 ? Math.Min(taxPayable, 0.5m * totalTaxableSalaryInPeriod) : taxPayable;

        return new TaxCalculationResult(null, totalTaxableSalaryInPeriod, taxCode, totalTaxableSalaryYtd,
            taxPaidYearToDate, _taxPeriodCount, taxUnpaidDueToRegulatoryLimit, finalTaxPayable);
    }

    public decimal CalculateTaxDueCumulative(decimal totalTaxableSalaryInPeriod,
    decimal benefitsInKind,
    //TaxCode taxCode,
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

        TaxPeriodBandwidthEntry applicableEntry = _taxBandwidths.TaxBandwidthEntries
            .FirstOrDefault(entry =>
                taxableSalaryAfterAllowance <= decimal.Round(entry.CumulativeBandwidth, 0, MidpointRounding.ToPositiveInfinity) || entry.IsTopBand) ??
                    throw new InvalidReferenceDataException($"No applicable tax bandwidth found for taxable pay of {taxableSalaryAfterAllowance:N2}");

        var cumulativeBandwidthBelow = applicableEntry?.BandWidthEntryBelow?.CumulativeBandwidth ?? 0.0m;
        var cumulativeTaxBelow = applicableEntry?.BandWidthEntryBelow?.CumulativeTax ?? 0.0m;

        var applicableRate = applicableEntry?.Rate ?? 0.0m;

        Debug.WriteLine("Cumlative tax calculation: totalTaxableSalaryYtd = {0}, taxFreePayToEndOfPeriod = {1}, taxableSalaryAfterAllowance = {2}, applicableRate = {3}",
            totalTaxableSalaryYtd, taxFreePayToEndOfPeriod, taxableSalaryAfterAllowance, applicableRate);

        var taxableSalaryAfterAllowanceRounded = Math.Floor(taxableSalaryAfterAllowance);

        //var taxDueToEndOfPeriod = decimal.Round(cumulativeTaxBelow + (taxableSalaryAfterAllowance - cumulativeBandwidthBelow) * applicableRate, 10);
        var taxDueToEndOfPeriod = cumulativeTaxBelow + (taxableSalaryAfterAllowanceRounded - cumulativeBandwidthBelow) * applicableRate;
        var taxDue = decimal.Round(taxDueToEndOfPeriod, 2, MidpointRounding.ToZero);

        var taxPayable = taxDue - taxPaidYearToDate;

        //return decimal.Round(taxPayable, 8);
        return taxPayable;
    }

    public decimal CalculateTaxDueNonCumulative(decimal totalTaxableSalaryInPeriod,
        decimal benefitsInKind,
        decimal taxFreePayToEndOfPeriod,
        decimal taxUnpaidDueToRegulatoryLimit = 0.0m)
    {
        // TODO: Process possible refund
        //if (totalTaxableSalaryYtd <= taxFreePayToEndOfPeriod)
        //    return new TaxCalculation(null, totalTaxableSalaryInPeriod, taxCode, totalTaxableSalaryYtd,
        //        taxPaidYearToDate, _taxPeriodCount, taxUnpaidDueToRegulatoryLimit, 0.0m);

        var taxableSalaryAfterAllowance = totalTaxableSalaryInPeriod - taxFreePayToEndOfPeriod;
        //var taxableSalaryAfterAllowance = Math.Floor(totalTaxableSalaryInPeriod - taxFreePayToEndOfPeriod);

        TaxPeriodBandwidthEntry applicableEntry = _taxBandwidths.TaxBandwidthEntries
            .FirstOrDefault(entry =>
                taxableSalaryAfterAllowance <= decimal.Round(entry.Period1CumulativeBandwidth, 0, MidpointRounding.ToPositiveInfinity) || entry.IsTopBand) ??
                    throw new InvalidReferenceDataException($"No applicable tax bandwidth found for taxable pay of {taxableSalaryAfterAllowance:N2}");

        var cumulativeBandwidthBelow = applicableEntry?.BandWidthEntryBelow?.Period1CumulativeBandwidth ?? 0.0m;
        var cumulativeTaxBelow = applicableEntry?.BandWidthEntryBelow?.Period1CumulativeTax ?? 0.0m;

        var applicableRate = applicableEntry?.Rate ?? 0.0m;

        var taxableSalaryAfterAllowanceRounded = Math.Floor(taxableSalaryAfterAllowance);

        var taxDue = cumulativeTaxBelow + (taxableSalaryAfterAllowanceRounded - cumulativeBandwidthBelow) * applicableRate;
        //var taxDue = decimal.Round(cumulativeTaxBelow + (taxableSalaryAfterAllowance - cumulativeBandwidthBelow) * applicableRate, 10);

        var taxPayable = decimal.Round(taxDue, 2, MidpointRounding.ToZero);//Rounding.Truncate(taxDue, 2);

        Debug.WriteLine("Non-cumulative tax calculation: totalTaxableSalaryInPeriod = {0}, taxFreePayToEndOfPeriod = {1}, taxableSalaryAfterAllowance = {2}, applicableRate = {3}, taxPayable = {4}",
            totalTaxableSalaryInPeriod, taxFreePayToEndOfPeriod, taxableSalaryAfterAllowance, applicableRate, taxPayable);

        return taxPayable;
        //return decimal.Round(taxPayable, 8);
    }


    private TaxCalculationResult CalculateFixedTaxCode(decimal taxableSalary,
        TaxCode taxCode,
        decimal taxableSalaryYearToDate,
        decimal taxPaidYearToDate,
        decimal taxUnpaidDueToRegulatoryLimit = 0.0m)
    {
        var taxDue = 0.0m;

        switch (taxCode.TaxTreatment)
        {
            case TaxTreatment.NT:
                taxDue = taxCode.IsNonCumulative ? 0.0m : -taxPaidYearToDate;
                break;

            case TaxTreatment.BR:
            case TaxTreatment.D0:
            case TaxTreatment.D1:
            case TaxTreatment.D2:
                var index = taxCode.TaxTreatment.GetBandIndex();
                if (index >= _taxBandwidths.TaxBandwidthEntries.Length)
                    throw new InconsistentDataException($"Tax code {taxCode.TaxTreatment} invalid for tax year/countries combination");
                var taxRate = _taxBandwidths.TaxBandwidthEntries[index].Rate;
                taxDue = taxCode.IsNonCumulative ?
                    Math.Round(taxableSalary, 0, MidpointRounding.ToZero) * taxRate :
                    Math.Round(taxableSalaryYearToDate + taxableSalary, 0, MidpointRounding.ToZero) * taxRate - taxPaidYearToDate;
                break;
        }

        var taxPayable = taxDue;// Rounding.Truncate(taxDue - taxPaidYearToDate, 2);

        return new(null, taxableSalary, taxCode, taxableSalaryYearToDate, taxPaidYearToDate, _taxPeriodCount, taxUnpaidDueToRegulatoryLimit, taxPayable);
    }
}
