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

public class PensionablePaySetCalculator : PensionContributionCalculator
{
    public override EarningsBasis EarningsBasis { get; }

    public PensionablePaySetCalculator(EarningsBasis earningsBasis, PensionTaxTreatment taxTreatment, decimal? basicRateOfTax = null)
        : base(taxTreatment, basicRateOfTax)
    {
        EarningsBasis = earningsBasis;
    }

    protected override (decimal earningsForPensionCalculation, decimal employerContribution, decimal employeeContribution) CalculateContributions(decimal pensionableSalary,
        decimal employerContributionPercentage,
        decimal employeeContribution,
        bool employeeContributionIsFixedAmount = false)
    {
        return (pensionableSalary,
            decimal.Round(pensionableSalary * employerContributionPercentage / 100.0m, 2, MidpointRounding.AwayFromZero),
            employeeContributionIsFixedAmount ?
            employeeContribution :
            decimal.Round(pensionableSalary * employeeContribution / 100.0m, 2, MidpointRounding.AwayFromZero));
    }
}