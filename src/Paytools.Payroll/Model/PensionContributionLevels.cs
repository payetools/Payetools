// Copyright (c) 2023 Paytools Foundation.
//
// Paytools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Paytools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

using Paytools.Pensions.Model;

namespace Paytools.Payroll.Model;

/// <summary>
/// Defines the levels to be applied for contributions into an employee's pension.
/// </summary>
public record PensionContributionLevels : IPensionContributionLevels
{
    /// <summary>
    /// Gets the employee contribution level, either expressed in percentage points (i.e., 5% = 5.0m)
    /// or as a fixed amount (i.e. £500.00), as indicated by the following parameter.
    /// </summary>
    public decimal EmployeeContribution { get; init; }

    /// <summary>
    /// Gets a value indicating whether <see cref="EmployeeContribution"/> should be treated as a fixed amount.  True if the employee
    /// contribution figure sshould be treated as a fixed amount; false if it should be treated as a percentage.
    /// </summary>
    public bool EmployeeContributionIsFixedAmount { get; init; }

    /// <summary>
    /// Gets the employer contribution percentage, expressed in percentage points, i.e., 3% = 3.0m.
    /// </summary>
    public decimal EmployerContributionPercentage { get; init; }

    /// <summary>
    /// Gets a value indicating whether salary exchange should be applied.
    /// </summary>
    public bool SalaryExchangeApplied { get; init; }

    /// <summary>
    /// Gets the percentage of employer's NI saving to be re-invested into the employee's pension as an employer-only
    /// contribution, expressed in percentage points, i.e., 50% = 50.0m.  Only applies under salary exchange.
    /// </summary>
    public decimal? EmployersNiReinvestmentPercentage { get; init; }

    /// <summary>
    /// Gets any Additional Voluntary Contribution (AVC) on the part of the employee.
    /// </summary>
    public decimal? AvcForPeriod { get; init; }

    /// <summary>
    /// Gets the value used to override the employer contribution when an individual is on maternity leave
    /// and should be paid employer contributions based on their contracted salary rather than their
    /// pensionable pay.
    /// </summary>
    public decimal? SalaryForMaternityPurposes { get; init; }
}