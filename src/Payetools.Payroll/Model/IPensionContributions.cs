// Copyright (c) 2023-2025, Payetools Ltd.
//
// Payetools Ltd licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;

namespace Payetools.Payroll.Model;

/// <summary>
/// Provides access to the pension contributions for an employee for a given pay run.
/// </summary>
public interface IPensionContributions
{
    /// <summary>
    /// Gets the earnings basis for this pension scheme.
    /// </summary>
    PensionsEarningsBasis EarningsBasis { get; init; }

    /// <summary>
    /// Gets the tax treatment for this pension scheme.
    /// </summary>
    PensionTaxTreatment TaxTreatment { get; init; }

    /// <summary>
    /// Gets the employee contribution level, either expressed in percentage points (i.e., 5% = 5.0m)
    /// or as a fixed amount (i.e. £500.00), as indicated by <see cref="EmployeeContributionIsFixedAmount"/>.
    /// </summary>
    decimal EmployeeContribution { get; init; }

    /// <summary>
    /// Gets a value indicating whether <see cref="EmployeeContribution"/> should be treated as a fixed amount.  True if the employee
    /// contribution figure sshould be treated as a fixed amount; false if it should be treated as a percentage.
    /// </summary>
    bool EmployeeContributionIsFixedAmount { get; init; }

    /// <summary>
    /// Gets the employer contribution percentage, either expressed in percentage points (i.e., 5% = 5.0m)
    /// or as a fixed amount (i.e. £500.00), as indicated by <see cref="EmployerContributionIsFixedAmount"/>.
    /// </summary>
    decimal EmployerContribution { get; init; }

    /// <summary>
    /// Gets a value indicating whether <see cref="EmployerContribution"/> should be treated as a fixed amount.  True if the employer
    /// contribution figure sshould be treated as a fixed amount; false if it should be treated as a percentage.
    /// </summary>
    bool EmployerContributionIsFixedAmount { get; init; }

    /// <summary>
    /// Gets a value indicating whether salary exchange should be applied.
    /// </summary>
    bool SalaryExchangeApplied { get; init; }

    /// <summary>
    /// Gets the percentage of employer's NI saving to be re-invested into the employee's pension as an employer-only
    /// contribution, expressed in percentage points, i.e., 50% = 50.0m.  Only applies under salary exchange.
    /// </summary>
    decimal? EmployersNiReinvestmentPercentage { get; init; }

    /// <summary>
    /// Gets any Additional Voluntary Contribution (AVC) on the part of the employee.
    /// </summary>
    decimal? AvcForPeriod { get; init; }

    /// <summary>
    /// Gets the value used to override the employer contribution when an individual is on maternity leave
    /// and should be paid employer contributions based on their contracted salary rather than their
    /// pensionable pay.
    /// </summary>
    decimal? SalaryForMaternityPurposes { get; init; }
}