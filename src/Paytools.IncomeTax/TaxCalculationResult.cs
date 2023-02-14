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

using Paytools.IncomeTax.ReferenceData;

namespace Paytools.IncomeTax;

/// <summary>
/// Represents the result of an income tax calculation for an individual.  
/// </summary>
public readonly struct TaxCalculationResult : ITaxCalculationResult
{
    //public PayDate PayDate { get; init; }
    public decimal TaxableSalary { get; init; }
    public TaxCode TaxCode { get; init; }
    public decimal SalaryYearToDate { get; init; }
    public decimal TaxPaidYearToDate { get; init; }
    public int PayPeriodCount { get; init; }
    public decimal TaxUnpaidDueToRegulatoryLimit { get; init; }
    public decimal TaxDue { get; init; }
    public TaxAtBand[] TaxAtEachBand { get; init; }

    public TaxCalculationResult(TaxBandwidthEntry? applicableEntry,
        decimal incomeAtApplicableEntry,
        decimal taxAtApplicableEntry,
        decimal taxRateAtApplicableEntry,
        //PayDate payDate,
        decimal taxableSalary,
        TaxCode taxCode,
        decimal salaryYearToDate,
        decimal taxPaidYearToDate,
        int payPeriodCount,
        decimal taxUnpaidDueToRegulatoryLimit,
        decimal taxDue)
    {
        //PayDate = payDate;
        TaxableSalary = taxableSalary;
        TaxCode = taxCode;
        SalaryYearToDate = salaryYearToDate;
        TaxPaidYearToDate = taxPaidYearToDate;
        PayPeriodCount = payPeriodCount;
        TaxUnpaidDueToRegulatoryLimit = taxUnpaidDueToRegulatoryLimit;
        TaxDue = taxDue;

        TaxAtEachBand = GetTaxBandSplit(applicableEntry, incomeAtApplicableEntry, taxAtApplicableEntry, taxRateAtApplicableEntry);
    }

    private static TaxAtBand[] GetTaxBandSplit(TaxBandwidthEntry? applicableEntry, decimal incomeAtApplicableEntry, decimal taxAtApplicableEntry, decimal taxRateAtApplicableEntry)
    {
        TaxAtBand[] result;

        if (applicableEntry == null)
        {
            result = new TaxAtBand[1] { new TaxAtBand(taxRateAtApplicableEntry, incomeAtApplicableEntry, taxAtApplicableEntry) };
        }
        else
        {
            int bands = applicableEntry.GetApplicableBandCount();

            result = new TaxAtBand[bands];

            var bandwidthEntry = applicableEntry;

            for (int index = bands - 1; index >= 0; index--)
            {
                if (bandwidthEntry == null)
                    throw new InvalidOperationException("Unexpected null bandwidth entry detected when establishing tax band split");

                result[index] = index == bands - 1 ?
                    new TaxAtBand(taxRateAtApplicableEntry, incomeAtApplicableEntry, taxAtApplicableEntry) :
                    new TaxAtBand(bandwidthEntry.Rate, bandwidthEntry.Bandwidth, bandwidthEntry.TaxForBand);

                bandwidthEntry = bandwidthEntry.BandWidthEntryBelow;
            }
        }

        return result;
    }
}