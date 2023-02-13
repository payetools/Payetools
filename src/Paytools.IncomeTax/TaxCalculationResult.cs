// Copyright (c) 2022-2023 Paytools Ltd
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

public readonly struct TaxCalculationResult : ITaxCalculationResult
{
    public decimal TaxableSalary { get; init; }
    public TaxCode TaxCode { get; init; }
    public decimal SalaryYearToDate { get; init; }
    public decimal TaxPaidYearToDate { get; init; }
    public int PayPeriodCount { get; init; }
    public decimal TaxUnpaidDueToRegulatoryLimit { get; init; }
    public decimal TaxDue { get; init; }

    public readonly struct TaxAtBand
    {
        public readonly decimal TaxDue;
        public readonly decimal ApplicableIncome;

        public TaxAtBand(decimal applicableIncome, decimal taxDue)
        {
            ApplicableIncome = applicableIncome;
            TaxDue = taxDue;
        }
    }

    public TaxCalculationResult(TaxBandwidthEntry? applicableEntry,
        decimal taxableSalary,
         TaxCode taxCode,
         decimal salaryYearToDate,
         decimal taxPaidYearToDate,
         int payPeriodCount,
         decimal taxUnpaidDueToRegulatoryLimit,
         decimal taxDue)
    {
        TaxableSalary = taxableSalary;
        TaxCode = taxCode;
        SalaryYearToDate = salaryYearToDate;
        TaxPaidYearToDate = taxPaidYearToDate;
        PayPeriodCount = payPeriodCount;
        TaxUnpaidDueToRegulatoryLimit = taxUnpaidDueToRegulatoryLimit;
        TaxDue = taxDue;
    }

    private TaxAtBand[] GetTaxBandSplit(TaxBandwidthEntry? applicableEntry, decimal incomeAtApplicableEntry, decimal taxAtApplicableEntry)
    {
        TaxAtBand[] result = new TaxAtBand[1];

        return result;

        //if (applicableEntry != null)
        //{
        //    var taxSplitCount = GetBandwidthCount(applicableEntry);

        //    result = new TaxAtBand[taxSplitCount];

        //}
        //else
        //    result = new TaxAtBand[1];

        //result[result.Length-1] = { new TaxAtBand(incomeAtApplicableEntry, taxAtApplicableEntry)};



        //var taxBandSplit = new TaxBand[taxSplitCount];

        //var entry = applicableEntry;
        //for (var index = taxSplitCount - 1; index >= 0; index--)
        //{
        //    taxBandSplit[index] = new TaxBand() { }
        //    entry = entry?.BandWidthEntryBelow;

        //}
        //while (index )
        //{

        //    index++;
        //}




        //return bandSplit;
    }

    private int GetBandwidthCount(TaxBandwidthEntry applicableEntry)
    {
        int index = 1;
        var entry = applicableEntry;
        while (entry?.BandWidthEntryBelow != null)
        {
            entry = entry?.BandWidthEntryBelow;
            index++;
        }

        return index;
    }
}