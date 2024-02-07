// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Pensions.Model;

namespace Payetools.Payroll.Model;

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
    /// Gets the employer contribution percentage, either expressed in percentage points (i.e., 5% = 5.0m)
    /// or as a fixed amount (i.e. £500.00), as indicated by <see cref="EmployerContributionIsFixedAmount"/>.
    /// </summary>
    public decimal EmployerContribution { get; init; }

    /// <summary>
    /// Gets a value indicating whether <see cref="EmployerContribution"/> should be treated as a fixed amount.  True if the employer
    /// contribution figure sshould be treated as a fixed amount; false if it should be treated as a percentage.
    /// </summary>
    public bool EmployerContributionIsFixedAmount { get; init; }

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