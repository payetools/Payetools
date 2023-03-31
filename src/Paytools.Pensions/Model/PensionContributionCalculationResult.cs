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

namespace Paytools.Pensions.Model;

/// <summary>
/// Represents a pension contribution calculation result.
/// </summary>
public readonly struct PensionContributionCalculationResult : IPensionContributionCalculationResult
{
    /// <summary>
    /// Gets an empty <see cref="PensionContributionCalculationResult"/> that indicates that no pension is applicable.
    /// </summary>
    public static IPensionContributionCalculationResult NoPensionApplicable => default(PensionContributionCalculationResult);

    /// <summary>
    /// Gets the pensionable salary for the period.
    /// </summary>
    public decimal PensionableSalaryInPeriod { get; init; }

    /// <summary>
    /// Gets the earnings basis for the pension calculation (e.g., Qualifying Earnings).
    /// </summary>
    public PensionsEarningsBasis EarningsBasis { get; init; }

    /// <summary>
    /// Gets the employee contribution percentage, expressed in percentage points, i.e., 5% = 5.0m.
    /// </summary>
    public decimal? EmployeeContributionPercentage { get; init; }

    /// <summary>
    /// Gets the employee's fixed contribution amount, if applicable.  If supplied, overrides
    /// <see cref="EmployeeContributionPercentage"/>.
    /// </summary>
    public decimal? EmployeeContributionFixedAmount { get; init; }

    /// <summary>
    /// Gets the employer contribution percentage, expressed in percentage points, i.e., 3% = 3.0m.
    /// </summary>
    public decimal EmployerContributionPercentage { get; init; }

    /// <summary>
    /// Gets a value indicating whether salary exchange has been applied.
    /// </summary>
    public bool SalaryExchangeApplied { get; init; }

    /// <summary>
    /// Gets the percentage of employer's NI saving to be re-invested into the employee's pension as an employer-only
    /// contribution, expressed in percentage points, i.e., 50% = 50.0m.  Only applies under salary exchange.
    /// </summary>
    public decimal? EmployersNiReinvestmentPercentage { get; init; }

    /// <summary>
    /// Gets the amount of any employer NI savings that are to be re-invested in the employee's pension, adjusted
    /// by the value of <see cref="EmployersNiReinvestmentPercentage"/>.  Only applies under salary exchange.
    /// </summary>
    public decimal? EmployerNiSavingsToReinvest { get; init; }

    /// <summary>
    /// Gets the portion of the total employer-only contribution made under salary exchange that pertains to the
    /// employer's contribution.
    /// </summary>
    public decimal? EmployerContributionAmountBeforeSalaryExchange { get; init; }

    /// <summary>
    /// Gets the amount the employee's gross salary exchanged under a salary exchange arrangement.
    /// </summary>
    public decimal? SalaryExchangedAmount { get; init; }

    /// <summary>
    /// Gets any Additional Voluntary Contribution (AVC) made by the employee.
    /// </summary>
    public decimal? EmployeeAvcAmount { get; init; }

    /// <summary>
    /// Gets the portion of earnings used to calculate the employee and employer contributions under
    /// Qualifying Earnings.  Null for pensionable pay.
    /// </summary>
    public decimal? BandedEarnings { get; init; }

    /// <summary>
    /// Gets the pension tax treatment to be applied, i.e., relief at source vs net pay arrangement.
    /// </summary>
    public PensionTaxTreatment TaxTreatment { get; init; }

    /// <summary>
    /// Gets the employee contribution amount resulting from the calculation.  Will be zero if <see cref="SalaryExchangeApplied"/>
    /// is true.
    /// </summary>
    public decimal CalculatedEmployeeContributionAmount { get; init; }

    /// <summary>
    /// Gets the employer contribution amount resulting from the calculation.  If<see cref="SalaryExchangeApplied"/> is true,
    /// includes both calculated amounts for employer and employee contributions and any NI reinvestment to be
    /// applied (based on the value of <see cref="EmployersNiReinvestmentPercentage"/>).
    /// </summary>
    public decimal CalculatedEmployerContributionAmount { get; init; }
}