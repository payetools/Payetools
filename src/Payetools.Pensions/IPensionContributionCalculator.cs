// Copyright (c) 2023-2024, Payetools Foundation.
//
// Paytools Foundation licenses this file to you under one of the following licenses:
//
//   * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//   * Paytools Commercial Use license [TBA]

using Payetools.Pensions.Model;

namespace Payetools.Pensions;

/// <summary>
/// Interface for types that provide calculation of pension contributions, both under normal arrangements
/// and under salary exchange (aka salary sacrifice).
/// </summary>
public interface IPensionContributionCalculator
{
    /// <summary>
    /// Calculates the appropriate employee and employer pension contributions based on the supplied employer
    /// and employee rates.  Also supports the ability to supply a fixed employee contribution as an
    /// alternative to an employee percentage rate.
    /// </summary>
    /// <param name="pensionableSalary">Pensionable salary to be used for calculation.</param>
    /// <param name="employerContributionPercentage">Employer contribution level, expressed in percentage points (i.e., 3% = 3.0m).</param>
    /// <param name="employeeContribution">Employee contribution level, either expressed in percentage points (i.e., 5% = 5.0m)
    /// or as a fixed amount (i.e. £500.00), as indicated by the following parameter.</param>
    /// <param name="employeeContributionIsFixedAmount">True if the previous parameter should be treated as a fixed amount; false if
    /// it should be treated as a percentage.</param>
    /// <param name="avcForPeriod">Any Additional Voluntary Contribution (AVC) on the part of the employee.</param>
    /// <param name="salaryForMaternityPurposes">Used to override the employer contribution when an individual is on
    /// maternity leave and should be paid employer contributions based on their contracted salary rather than their
    /// pensionable pay.</param>
    /// <param name="result">An instance of a <see cref="IPensionContributionCalculationResult"/> implementation that contains
    /// the results of the calculation.</param>
    void Calculate(decimal pensionableSalary,
        decimal employerContributionPercentage,
        decimal employeeContribution,
        bool employeeContributionIsFixedAmount,
        decimal? avcForPeriod,
        decimal? salaryForMaternityPurposes,
        out IPensionContributionCalculationResult result);

    /// <summary>
    /// Calculates the appropriate employee and employer pension contributions based on the supplied employer
    /// and employee rates, but under salary exchange arrangements.  Here the output employee contribution will
    /// always be zero.  Also supports the ability to supply a fixed employee contribution as an
    /// alternative to an employee percentage rate.
    /// </summary>
    /// <param name="pensionableSalary">Pensionable salary to be used for calculation.</param>
    /// <param name="employerContributionPercentage">Employer contribution level, expressed in percentage points (i.e., 3% = 3.0m).</param>
    /// <param name="employerNiSavings">Savings in employer's NI due to the salary exchanged.</param>
    /// <param name="employerNiSavingsReinvestmentPercentage">Percentage of employer NI savings to be reinvested in the employee's
    /// pension, expressed in percentage points (i.e., 100% = 100.0m).</param>
    /// <param name="employeeSalaryExchanged">The level of employee's salary forgone as set out in the salary
    /// exchange arrangements.  Expressed either as a percentage in percentage points (e.g., 5% = 5.0m), or as a fixed
    /// amount, as indicated by the following parameter.  NB If fixed amount is given, it relates to the pay period
    /// (as opposed to annually).</param>
    /// <param name="employeeSalaryExchangedIsFixedAmount">True if the previous parameter should be treated as a fixed amount; false if
    /// it should be treated as a percentage.</param>
    /// <param name="avcForPeriod">Any Additional Voluntary Contribution (AVC) on the part of the employee.</param>
    /// <param name="salaryForMaternityPurposes">Used to override the employer contribution when an individual is on
    /// maternity leave and should be paid employer contributions based on their contracted salary rather than their
    /// pensionable pay.</param>
    /// <param name="result">An instance of a <see cref="IPensionContributionCalculationResult"/> implementation that contains
    /// the results of the calculation.</param>
    void CalculateUnderSalaryExchange(decimal pensionableSalary,
        decimal employerContributionPercentage,
        decimal employerNiSavings,
        decimal employerNiSavingsReinvestmentPercentage,
        decimal employeeSalaryExchanged,
        bool employeeSalaryExchangedIsFixedAmount,
        decimal? avcForPeriod,
        decimal? salaryForMaternityPurposes,
        out IPensionContributionCalculationResult result);

    /// <summary>
    /// Gets the absolute amount of employee salary exchanged, either as a result of a fixed amount being passed in,
    /// or as a percentage of pensionable salary (banded in the case of Qualifying Earnings.
    /// </summary>
    /// <param name="pensionableSalary">Pensionable salary to be used for calculation.</param>
    /// <param name="employeeSalaryExchanged">The level of employee's salary forgone as set out in the salary
    /// exchange arrangements.  Expressed either as a percentage in percentage points (e.g., 5% = 5.0m), or as a fixed
    /// amount, as indicated by the following parameter.  NB If fixed amount is given, it relates to the pay period
    /// (as opposed to annually).</param>
    /// <param name="employeeSalaryExchangedIsFixedAmount">True if the previous parameter should be treated as a fixed amount; false if
    /// it should be treated as a percentage.</param>
    /// <returns>Value of employee salary being exchanged.</returns>
    decimal GetSalaryExchangedAmount(
        decimal pensionableSalary,
        decimal employeeSalaryExchanged,
        bool employeeSalaryExchangedIsFixedAmount);
}