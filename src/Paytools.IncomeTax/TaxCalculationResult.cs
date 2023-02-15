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

using Paytools.Common.Model;

namespace Paytools.IncomeTax;

/// <summary>
/// Represents the result of an income tax calculation for an individual.  
/// </summary>
public readonly struct TaxCalculationResult : ITaxCalculationResult
{
    public ITaxCalculator Calculator { get; init; }
    public decimal TaxableSalary { get; init; }
    public TaxCode TaxCode { get; init; }
    public decimal PreviousSalaryYearToDate { get; init; }
    public decimal PreviousTaxPaidYearToDate { get; init; }
    public decimal TaxUnpaidDueToRegulatoryLimit { get; init; }
    public decimal TaxDue { get; init; }
    public int HighestApplicableTaxBandIndex { get; init; }
    public decimal IncomeAtHighestApplicableBand { get; init; }
    public decimal TaxAtHighestApplicableBand { get; init; }

    public TaxCalculationResult(ITaxCalculator calculator,
        int highestApplicableTaxBandIndex,
        decimal incomeAtHighestApplicableBand,
        decimal taxAtHighestApplicableBand,
        decimal taxableSalary,
        TaxCode taxCode,
        decimal previousSalaryYearToDate,
        decimal previousTaxPaidYearToDate,
        decimal taxUnpaidDueToRegulatoryLimit,
        decimal taxDue)
    {
        Calculator = calculator;
        HighestApplicableTaxBandIndex = highestApplicableTaxBandIndex;
        IncomeAtHighestApplicableBand = incomeAtHighestApplicableBand;
        TaxAtHighestApplicableBand = taxAtHighestApplicableBand;
        TaxableSalary = taxableSalary;
        TaxCode = taxCode;
        PreviousSalaryYearToDate = previousSalaryYearToDate;
        PreviousTaxPaidYearToDate = previousTaxPaidYearToDate;
        TaxUnpaidDueToRegulatoryLimit = taxUnpaidDueToRegulatoryLimit;
        TaxDue = taxDue;
    }
}