// Copyright (c) 2023 Paytools Foundation.  All rights reserved.
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
/// Represents a pension contribution calculator for Pensionable Pay Set 1, 2 and 3.
/// </summary>
public class PensionablePaySetCalculator : PensionContributionCalculator
{
    /// <inheritdoc/>
    public override EarningsBasis EarningsBasis { get; }

    /// <summary>
    /// Initialises a new instance of <see cref="PensionablePaySetCalculator"/> for the specified earnings basic and tax treatment.
    /// </summary>
    /// <param name="earningsBasis">Sepcifies the earnings basis for this calculator.  Must be one of Pensionable Pay Set 1,
    /// Pensionable Pay Set 2 or Pensionable Pay Set 3.</param>
    /// <param name="taxTreatment">Tax treatment for the target pension, i.e., net pay arrangement vs relief at source.</param>
    /// <param name="basicRateOfTax">Basic rate of tax to use for relief at source pensions.</param>
    /// <exception cref="ArgumentException">Thrown if an invalid earnings basis is supplied.</exception>
    public PensionablePaySetCalculator(EarningsBasis earningsBasis, PensionTaxTreatment taxTreatment, decimal? basicRateOfTax = null)
        : base(taxTreatment, basicRateOfTax)
    {
        if (earningsBasis != EarningsBasis.PensionablePaySet1 &&
            earningsBasis != EarningsBasis.PensionablePaySet2 &&
            earningsBasis != EarningsBasis.PensionablePaySet3)
            throw new ArgumentException("Earnings basis must be one of PensionablePaySet1, PensionablePaySet2 or PensionablePaySet3", nameof(earningsBasis));

        EarningsBasis = earningsBasis;
    }

    /// <inheritdoc/>
    protected override (decimal earningsForPensionCalculation, decimal employerContribution, decimal employeeContribution) CalculateContributions(decimal pensionableSalary,
        decimal employerContributionPercentage,
        decimal employeeContribution,
        bool employeeContributionIsFixedAmount = false,
        decimal? salaryForMaternityPurposes = null)
    {
        return (pensionableSalary,
            decimal.Round(pensionableSalary * employerContributionPercentage / 100.0m, 2, MidpointRounding.AwayFromZero),
            employeeContributionIsFixedAmount ?
            employeeContribution :
            decimal.Round(pensionableSalary * employeeContribution / 100.0m, 2, MidpointRounding.AwayFromZero));
    }
}