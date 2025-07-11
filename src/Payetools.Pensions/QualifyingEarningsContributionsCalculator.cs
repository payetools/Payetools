﻿// Copyright (c) 2023-2025, Payetools Ltd.
//
// Payetools Ltd licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;

namespace Payetools.Pensions;

/// <summary>
/// Represents a pension contribution calculator for Qualifying Earnings.
/// </summary>
public class QualifyingEarningsContributionsCalculator : PensionContributionCalculator
{
    private readonly decimal _lowerLevelForQualifyingEarnings;
    private readonly decimal _upperLevelForQualifyingEarnings;

    /// <summary>
    /// Gets the earnings basis for this calculator.  Always returns <see cref="PensionsEarningsBasis.QualifyingEarnings"/>.
    /// </summary>
    public override PensionsEarningsBasis EarningsBasis => PensionsEarningsBasis.QualifyingEarnings;

    /// <summary>
    /// Initialises a new instance of <see cref="QualifyingEarningsContributionsCalculator"/> with the specified tax treatment,
    /// using the lower and upper thresholds supplied.
    /// </summary>
    /// <param name="taxTreatment">Tax treatment for the target pension, i.e., net pay arrangement vs relief at source.</param>
    /// <param name="lowerLevelForQualifyingEarnings">HMRC/TPR-supplied lower level for qualifying earnings.</param>
    /// <param name="upperLevelForQualifyingEarnings">HMRC/TPR-supplied upper level for qualifying earnings.</param>
    /// <param name="basicRateOfTax">Basic rate of tax to use for relief at source pensions.</param>
    public QualifyingEarningsContributionsCalculator(PensionTaxTreatment taxTreatment,
        decimal lowerLevelForQualifyingEarnings,
        decimal upperLevelForQualifyingEarnings,
        decimal? basicRateOfTax = null)
        : base(taxTreatment, basicRateOfTax)
    {
        _lowerLevelForQualifyingEarnings = lowerLevelForQualifyingEarnings;
        _upperLevelForQualifyingEarnings = upperLevelForQualifyingEarnings;
    }

    /// <inheritdoc/>
    protected override (decimal EarningsForPensionCalculation, decimal EmployerContribution, decimal EmployeeContribution) CalculateContributions(
        decimal pensionableSalary,
        decimal employerContributionPercentage,
        decimal employeeContribution,
        bool employeeContributionIsFixedAmount = false,
        decimal? salaryForMaternityPurposes = null)
    {
        decimal bandedEarnings = pensionableSalary <= _lowerLevelForQualifyingEarnings ?
            0.0m : Math.Min(pensionableSalary, _upperLevelForQualifyingEarnings) - _lowerLevelForQualifyingEarnings;

        var employerBandedEarnings = GetEarningsForPensionCalculation(salaryForMaternityPurposes ?? pensionableSalary);
        var employerContribution = employerBandedEarnings * employerContributionPercentage / 100.0m;

        return (GetEarningsForPensionCalculation(pensionableSalary),
            decimal.Round(employerContribution, 2, MidpointRounding.AwayFromZero),
            employeeContributionIsFixedAmount ?
                employeeContribution :
                decimal.Round(bandedEarnings * employeeContribution / 100.0m, 2, MidpointRounding.AwayFromZero));
    }

    /// <inheritdoc/>
    protected override decimal GetEarningsForPensionCalculation(decimal pensionableSalary) =>
        pensionableSalary <= _lowerLevelForQualifyingEarnings ?
            0.0m :
            Math.Min(pensionableSalary, _upperLevelForQualifyingEarnings) - _lowerLevelForQualifyingEarnings;
}