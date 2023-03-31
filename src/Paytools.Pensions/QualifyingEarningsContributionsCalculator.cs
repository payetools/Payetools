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

using Paytools.Common.Model;

namespace Paytools.Pensions;

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
    protected override (decimal earningsForPensionCalculation, decimal employerContribution, decimal employeeContribution) CalculateContributions(
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