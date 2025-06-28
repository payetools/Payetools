// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;

namespace Payetools.Payroll.Model;

/// <summary>
/// Provides access to the pension contributions for an employee for a given pay run.
/// </summary>
public class PensionContributions : IPensionContributions
{
    /// <summary>
    /// Gets the earnings basis for this pension scheme.
    /// </summary>
    public PensionsEarningsBasis EarningsBasis { get; init; }

    /// <summary>
    /// Gets the tax treatment for this pension scheme.
    /// </summary>
    public PensionTaxTreatment TaxTreatment { get; init; }

    /// <summary>
    /// Gets the employee contribution level, either expressed in percentage points (i.e., 5% = 5.0m)
    /// or as a fixed amount (i.e. £500.00), as indicated by <see cref="EmployeeContributionIsFixedAmount"/>.
    /// </summary>
    public decimal EmployeeContribution { get; init; }

    /// <summary>
    /// Gets a value indicating whether <see cref="EmployeeContribution"/> should be treated as a fixed amount.
    /// True if the employee contribution figure should be treated as a fixed amount; false if it should be treated as a percentage.
    /// </summary>
    public bool EmployeeContributionIsFixedAmount { get; init; }

    /// <summary>
    /// Gets the employer contribution percentage, either expressed in percentage points (i.e., 5% = 5.0m)
    /// or as a fixed amount (i.e. £500.00), as indicated by <see cref="EmployerContributionIsFixedAmount"/>.
    /// </summary>
    public decimal EmployerContribution { get; init; }

    /// <summary>
    /// Gets a value indicating whether <see cref="EmployerContribution"/> should be treated as a fixed amount.
    /// True if the employer contribution figure should be treated as a fixed amount; false if it should be treated as a percentage.
    /// </summary>
    public bool EmployerContributionIsFixedAmount { get; init; }

    /// <summary>
    /// Gets a value indicating whether salary exchange should be applied.
    /// </summary>
    public bool SalaryExchangeApplied { get; init; }

    /// <summary>
    /// Gets the percentage of employer's NI saving to be re-invested into the employee's pension as an employer-only
    /// contribution, expressed in percentage points, i.e., 50% = 50.0m. Only applies under salary exchange.
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

    /// <summary>
    /// Initializes a new instance of the <see cref="PensionContributions"/> class with the specified values.
    /// </summary>
    /// <param name="earningsBasis">The earnings basis for the pension scheme.</param>
    /// <param name="taxTreatment">The tax treatment for the pension scheme.</param>
    /// <param name="employeeContribution">The employee contribution amount or percentage.</param>
    /// <param name="employeeContributionIsFixedAmount">Whether the employee contribution is a fixed amount.</param>
    /// <param name="employerContribution">The employer contribution amount or percentage.</param>
    /// <param name="employerContributionIsFixedAmount">Whether the employer contribution is a fixed amount.</param>
    /// <param name="salaryExchangeApplied">Indicates whether salary exchange is applied.</param>
    /// <param name="employersNiReinvestmentPercentage">The percentage of NI savings to reinvest under salary exchange.</param>
    /// <param name="avcForPeriod">The employee's additional voluntary contribution (AVC), if any.</param>
    /// <param name="salaryForMaternityPurposes">The overridden salary used for employer contributions during maternity leave, if applicable.</param>
    public PensionContributions(
        PensionsEarningsBasis earningsBasis,
        PensionTaxTreatment taxTreatment,
        decimal employeeContribution,
        bool employeeContributionIsFixedAmount,
        decimal employerContribution,
        bool employerContributionIsFixedAmount,
        bool salaryExchangeApplied = false,
        decimal? employersNiReinvestmentPercentage = null,
        decimal? avcForPeriod = null,
        decimal? salaryForMaternityPurposes = null)
    {
        EarningsBasis = earningsBasis;
        TaxTreatment = taxTreatment;
        EmployeeContribution = employeeContribution;
        EmployeeContributionIsFixedAmount = employeeContributionIsFixedAmount;
        EmployerContribution = employerContribution;
        EmployerContributionIsFixedAmount = employerContributionIsFixedAmount;
        SalaryExchangeApplied = salaryExchangeApplied;
        EmployersNiReinvestmentPercentage = employersNiReinvestmentPercentage;
        AvcForPeriod = avcForPeriod;
        SalaryForMaternityPurposes = salaryForMaternityPurposes;
    }
}