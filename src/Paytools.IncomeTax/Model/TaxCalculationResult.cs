// Copyright (c) 2023 Paytools Foundation.
//
// Licensed under the Apache License, Version 2.0 (the "License") ~
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

namespace Paytools.IncomeTax.Model;

/// <summary>
/// Represents the result of an income tax calculation for an individual, providing the tax due plus a rich set of information about
/// how the result was achieved.
/// </summary>
public readonly struct TaxCalculationResult : ITaxCalculationResult
{
    /// <summary>
    /// Gets the calculator (implementation of <see cref="ITaxCalculator"/>) used to perform this calculation.
    /// </summary>
    public ITaxCalculator Calculator { get; }

    /// <summary>
    /// Gets the <see cref="TaxCode"/> used in this calculation.
    /// </summary>
    public TaxCode TaxCode { get; }

    /// <summary>
    /// Gets the taxable salary used in this calculation.  This is the gross salary less any tax-free pay (or plus any additional
    /// notional pay in the case of K tax codes).
    /// </summary>
    public decimal TaxableSalaryAfterTaxFreePay { get; }

    /// <summary>
    /// Gets the tax-free pay applicable to the end of the period, as given by the specified tax code.  May be negative
    /// in the case of K tax codes.
    /// </summary>
    public decimal TaxFreePayToEndOfPeriod { get; }

    /// <summary>
    /// Gets the year to date taxable salary paid up to and including the end of the previous period.
    /// </summary>
    public decimal PreviousPeriodSalaryYearToDate { get; }

    /// <summary>
    /// Gets the year to date tax paid up to and including the end of the previous period.
    /// </summary>
    public decimal PreviousPeriodTaxPaidYearToDate { get; }

    /// <summary>
    /// Gets any unpaid tax due to the regulatory limit.
    /// TODO: implement this properly.
    /// </summary>
    public decimal TaxUnpaidDueToRegulatoryLimit { get; }

    /// <summary>
    /// Gets the tax due at the end of the period, based on the taxable earnings to the end of the period and
    /// accounting for any tax-free pay up to the end of the period.  This figure takes account of both the
    /// effect of the regulatory limit and the effect of any unpaid taxes due to the effect of the regulatory
    /// limit in previous periods.
    /// </summary>
    public decimal FinalTaxDue { get; }

    /// <summary>
    /// Gets the tax due at the end of the period, based on the taxable earnings to the end of the period and
    /// accounting for any tax-free pay up to the end of the period.  This is before considering the effect of
    /// regulatory limits.
    /// </summary>
    public decimal TaxDueBeforeApplicationOfRegulatoryLimit { get; }

    /// <summary>
    /// Gets the numberic index of the highest tax band used in the calculation.
    /// </summary>
    public int HighestApplicableTaxBandIndex { get; }

    /// <summary>
    /// Gets the total income to date that falls within the highest tax band used in the calculation.
    /// </summary>
    public decimal IncomeAtHighestApplicableBand { get; }

    /// <summary>
    /// Gets the total tax due for income to date that falls within the highest tax band used in the calculation.
    /// </summary>
    public decimal TaxAtHighestApplicableBand { get; }

    /// <summary>
    /// Initialises a new instance of <see cref="TaxCalculationResult"/> using the supplied parameters.
    /// </summary>
    /// <param name="calculator">Calculator (implementation of <see cref="ITaxCalculator"/>) used to perform this calculation.</param>
    /// <param name="taxCode"><see cref="TaxCode"/> used in this calculation.</param>
    /// <param name="taxFreePayToEndOfPeriod">Tax-free pay applicable to the end of the period, derived from the specified tax code.</param>
    /// <param name="taxableSalary">Taxable salary used in this calculation.</param>
    /// <param name="previousPeriodSalaryYearToDate">Year to date taxable salary paid up to and including the end of the previous period.</param>
    /// <param name="previousPeriodTaxPaidYearToDate">Year to date tax paid up to and including the end of the previous period.</param>
    /// <param name="highestApplicableTaxBandIndex">Numeric index of the highest tax band (<see cref="TaxPeriodBandwidthEntry"/>) used in the
    /// calculation.</param>
    /// <param name="incomeAtHighestApplicableBand">Total income to date that falls within the highest tax band used in the calculation.</param>
    /// <param name="taxAtHighestApplicableBand">Total tax due for income to date that falls within the highest tax band used in the calculation.</param>
    /// <param name="taxUnpaidDueToRegulatoryLimit">Previous period tax unpaid due to regulatory limit.</param>
    /// <param name="taxDueBeforeRegulatoryLimitEffects">Tax due before considering the effects of regulatory limits.</param>
    /// <param name="finalTaxDue">Tax due at the end of the period, based on the taxable earnings to the end of the period and
    /// accounting for any tax-free pay up to the end of the period, accounting for the effects of regulatory limits, both from this
    /// period and any prior periods.</param>
    public TaxCalculationResult(
        ITaxCalculator calculator,
        TaxCode taxCode,
        decimal taxFreePayToEndOfPeriod,
        decimal taxableSalary,
        decimal previousPeriodSalaryYearToDate,
        decimal previousPeriodTaxPaidYearToDate,
        int highestApplicableTaxBandIndex,
        decimal incomeAtHighestApplicableBand,
        decimal taxAtHighestApplicableBand,
        decimal taxUnpaidDueToRegulatoryLimit,
        decimal taxDueBeforeRegulatoryLimitEffects,
        decimal finalTaxDue)
    {
        Calculator = calculator;
        TaxCode = taxCode;
        TaxFreePayToEndOfPeriod = taxFreePayToEndOfPeriod;
        HighestApplicableTaxBandIndex = highestApplicableTaxBandIndex;
        IncomeAtHighestApplicableBand = incomeAtHighestApplicableBand;
        TaxAtHighestApplicableBand = taxAtHighestApplicableBand;
        TaxableSalaryAfterTaxFreePay = taxableSalary;
        PreviousPeriodSalaryYearToDate = previousPeriodSalaryYearToDate;
        PreviousPeriodTaxPaidYearToDate = previousPeriodTaxPaidYearToDate;
        TaxUnpaidDueToRegulatoryLimit = taxUnpaidDueToRegulatoryLimit;
        TaxDueBeforeApplicationOfRegulatoryLimit = taxDueBeforeRegulatoryLimitEffects;
        FinalTaxDue = finalTaxDue;
    }
}