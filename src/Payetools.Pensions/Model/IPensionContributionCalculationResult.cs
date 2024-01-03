// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;

namespace Payetools.Pensions.Model;

/// <summary>
/// Interface for types that model pension contribution calculation results.
/// </summary>
public interface IPensionContributionCalculationResult
{
    /// <summary>
    /// Gets the pensionable salary for the period.
    /// </summary>
    decimal PensionableSalaryInPeriod { get; }

    /// <summary>
    /// Gets the earnings basis for the pension calculation (e.g., Qualifying Earnings).
    /// </summary>
    PensionsEarningsBasis EarningsBasis { get; }

    /// <summary>
    /// Gets the employee contribution percentage, expressed in percentage points, i.e., 5% = 5.0m.
    /// </summary>
    decimal? EmployeeContributionPercentage { get; }

    /// <summary>
    /// Gets the employee's fixed contribution amount, if applicable.  If supplied, overrides
    /// <see cref="EmployeeContributionPercentage"/>.
    /// </summary>
    decimal? EmployeeContributionFixedAmount { get; }

    /// <summary>
    /// Gets the employer contribution percentage, expressed in percentage points, i.e., 3% = 3.0m.
    /// </summary>
    decimal EmployerContributionPercentage { get; }

    /// <summary>
    /// Gets a value indicating whether salary exchange has been applied.
    /// </summary>
    bool SalaryExchangeApplied { get; }

    /// <summary>
    /// Gets the percentage of employer's NI saving to be re-invested into the employee's pension as an employer-only
    /// contribution, expressed in percentage points, i.e., 50% = 50.0m.  Only applies under salary exchange.
    /// </summary>
    decimal? EmployersNiReinvestmentPercentage { get; }

    /// <summary>
    /// Gets the amount of any employer NI savings that are to be re-invested in the employee's pension, adjusted
    /// by the value of <see cref="EmployersNiReinvestmentPercentage"/>.  Only applies under salary exchange.
    /// </summary>
    decimal? EmployerNiSavingsToReinvest { get; }

    /// <summary>
    /// Gets the portion of the total employer-only contribution made under salary exchange that pertains to the
    /// employer's contribution.
    /// </summary>
    decimal? EmployerContributionAmountBeforeSalaryExchange { get; }

    /// <summary>
    /// Gets the amount the employee's gross salary exchanged under a salary exchange arrangement.
    /// </summary>
    decimal? SalaryExchangedAmount { get; }

    /// <summary>
    /// Gets any Additional Voluntary Contribution (AVC) made by the employee.
    /// </summary>
    decimal? EmployeeAvcAmount { get; }

    /// <summary>
    /// Gets the portion of earnings used to calculate the employee and employer contributions under
    /// Qualifying Earnings.  Null for pensionable pay.
    /// </summary>
    decimal? BandedEarnings { get; }

    /// <summary>
    /// Gets the pension tax treatment to be applied, i.e., relief at source vs net pay arrangement.
    /// </summary>
    PensionTaxTreatment TaxTreatment { get; }

    /// <summary>
    /// Gets the employee contribution amount resulting from the calculation.  Will be zero if <see cref="SalaryExchangeApplied"/>
    /// is true.
    /// </summary>
    decimal CalculatedEmployeeContributionAmount { get; }

    /// <summary>
    /// Gets the employer contribution amount resulting from the calculation.  If<see cref="SalaryExchangeApplied"/> is true,
    /// includes both calculated amounts for employer and employee contributions and any NI reinvestment to be
    /// applied (based on the value of <see cref="EmployersNiReinvestmentPercentage"/>).
    /// </summary>
    decimal CalculatedEmployerContributionAmount { get; }
}