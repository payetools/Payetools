// Copyright (c) 2022-2023 Paytools Ltd
//
// Licensed under the Apache License, Version 2.0 (the "License")~
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

public interface IPensionContributionCalculationResult
{
    decimal PensionableSalaryInPeriod { get; }
    EarningsBasis EarningsBasis { get; }
    decimal? EmployeeContributionPercentage { get; }
    decimal? EmployeeContributionFixedAmount { get; }
    decimal EmployerContributionPercentage { get; }
    bool SalaryExchangeApplied { get; }
    decimal? EmployersNiReinvestmentPercentage { get; }
    decimal? EmployerNiSavings { get; }
    decimal? EmployerContributionAmountBeforeSalaryExchange { get; }
    decimal? SalaryExchangedAmount { get; }
    decimal? EmployeeAvcAmount { get; }
    decimal? BandedEarnings { get; }
    PensionTaxTreatment TaxTreatment { get; }

    /// <summary>
    /// Gets the employee contribution amount resulting from the calculation.  Will be zero if SalaryExchangeApplied
    /// is true.
    /// </summary>
    decimal EmployeeContributionAmount { get; }

    /// <summary>
    /// Gets the employer contribution amount resulting from the calculation.  If SalaryExchangeApplied is true,
    /// includes both calculated amounts for employer and employee contributions and any NI reinvestment to be
    /// applied (based on the value of EmployersNiReinvestmentPercentage).
    /// </summary>
    decimal EmployerContributionAmount { get; }
}
