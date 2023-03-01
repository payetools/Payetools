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

namespace Paytools.Pensions.Model;

/// <summary>
/// Interface that defines the levels to be applied for contributions into an employee's pension.
/// </summary>
public interface IPensionContributionLevels
{
    /// <summary>
    /// Gets the employee contribution level, either expressed in percentage points (i.e., 5% = 5.0m)
    /// or as a fixed amount (i.e. £500.00), as indicated by the following parameter.
    /// </summary>
    decimal EmployeeContribution { get; }

    /// <summary>
    /// Gets a value indicating whether <see cref="EmployeeContribution"/> should be treated as a fixed amount.  True if the employee
    /// contribution figure sshould be treated as a fixed amount; false if it should be treated as a percentage.
    /// </summary>
    bool EmployeeContributionIsFixedAmount { get; }

    /// <summary>
    /// Gets the employer contribution percentage, expressed in percentage points, i.e., 3% = 3.0m.
    /// </summary>
    decimal EmployerContributionPercentage { get; }

    /// <summary>
    /// Gets a value indicating whether salary exchange should be applied.
    /// </summary>
    bool SalaryExchangeApplied { get; }

    /// <summary>
    /// Gets the percentage of employer's NI saving to be re-invested into the employee's pension as an employer-only
    /// contribution, expressed in percentage points, i.e., 50% = 50.0m.  Only applies under salary exchange.
    /// </summary>
    decimal? EmployersNiReinvestmentPercentage { get; }

    /// <summary>
    /// Gets any Additional Voluntary Contribution (AVC) on the part of the employee.
    /// </summary>
    decimal? AvcForPeriod { get; }

    /// <summary>
    /// Gets the value used to override the employer contribution when an individual is on maternity leave
    /// and should be paid employer contributions based on their contracted salary rather than their
    /// pensionable pay.
    /// </summary>
    decimal? SalaryForMaternityPurposes { get; }
}