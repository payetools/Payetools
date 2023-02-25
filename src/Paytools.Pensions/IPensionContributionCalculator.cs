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

namespace Paytools.Pensions;

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
    /// <returns>An instance of a <see cref="IPensionContributionCalculationResult"/> implementation that contains
    /// the results of the calculation.</returns>
    IPensionContributionCalculationResult Calculate(decimal pensionableSalary,
        decimal employerContributionPercentage,
        decimal employeeContribution,
        bool employeeContributionIsFixedAmount = false,
        decimal avcForPeriod = 0.0m,
        decimal? salaryForMaternityPurposes = null);

    /// <summary>
    /// Calculates the appropriate employee and employer pension contributions based on the supplied employer
    /// and employee rates, but under salary exchange arrangements.  Here the output employee contribution will
    /// always be zero.  Also supports the ability to supply a fixed employee contribution as an
    /// alternative to an employee percentage rate.
    /// </summary>
    /// <param name="pensionableSalary">Pensionable salary to be used for calculation.</param>
    /// <param name="employerContributionPercentage">Employer contribution level, expressed in percentage points (i.e., 3% = 3.0m).</param>
    /// <param name="employersNiSavingsCalculator">Instance of <see cref="IEmployerNiSavingsCalculator"/> that knows how
    /// to calculate employer NI savings.</param>
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
    /// <returns>An instance of a <see cref="IPensionContributionCalculationResult"/> implementation that contains
    /// the results of the calculation.</returns>
    IPensionContributionCalculationResult CalculateUnderSalaryExchange(decimal pensionableSalary,
        decimal employerContributionPercentage,
        IEmployerNiSavingsCalculator employersNiSavingsCalculator,
        decimal employeeSalaryExchanged,
        bool employeeSalaryExchangedIsFixedAmount = false,
        decimal avcForPeriod = 0.0M,
        decimal? salaryForMaternityPurposes = null);
}