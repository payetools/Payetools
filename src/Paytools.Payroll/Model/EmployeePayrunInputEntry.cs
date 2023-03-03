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

using Paytools.Employment.Model;
using Paytools.Pensions.Model;
using System.Collections.Immutable;

namespace Paytools.Payroll.Model;

/// <summary>
/// Represents an employee payrun entry, i.e., all the information needed to prepare the payroll
/// record for the employee for the pay period in question.
/// </summary>
public record EmployeePayrunInputEntry : IEmployeePayrunInputEntry
{
    /// <summary>
    /// Gets the employee details for this entry.
    /// </summary>
    public IEmployee Employee { get; }

    /// <summary>
    /// Gets the employment details for the employee for this entry.
    /// </summary>
    public IEmployment Employment { get; }

    /// <summary>
    /// Gets the list of deductions for this employee for a given payrun.  May be empty.
    /// </summary>
    public ImmutableList<DeductionEntry> Deductions { get; }

    /// <summary>
    /// Gets the list of pay components for this employee for a given payrun.  May be empty but usually not.
    /// </summary>
    public ImmutableList<EarningsEntry> Earnings { get; }

    /// <summary>
    /// Gets the list of payrolled benefits for this employee for a given payrun.  Empty if the employee has
    /// no payrolled benefits.
    /// </summary>
    public ImmutableList<IPayrolledBenefitForPeriod> PayrolledBenefits { get; }

    /// <summary>
    /// Gets the pension contributions to apply for this pay period.
    /// </summary>
    public IPensionContributionLevels PensionContributionLevels { get; }

    /// <summary>
    /// Initialises a new instance of <see cref="EmployeePayrunInputEntry"/>.
    /// </summary>
    /// <param name="employee">Employee details.</param>
    /// <param name="employment">Employment details.</param>
    /// <param name="earnings">List of applicable earnings, if any.  Empty list if none.</param>
    /// <param name="deductions">List of applicable deductions, if any.  Empty list if none.</param>
    /// <param name="payrolledBenefits">List of payrolled benefits, if any.  Empty list if none.</param>
    /// <param name="pensionContributionLevels">Pension contribtuion levels to be applied.</param>
    public EmployeePayrunInputEntry(
        IEmployee employee,
        IEmployment employment,
        ImmutableList<EarningsEntry> earnings,
        ImmutableList<DeductionEntry> deductions,
        ImmutableList<IPayrolledBenefitForPeriod> payrolledBenefits,
        IPensionContributionLevels pensionContributionLevels)
    {
        Employee = employee;
        Employment = employment;
        Earnings = earnings;
        Deductions = deductions;
        PayrolledBenefits = payrolledBenefits;
        PensionContributionLevels = pensionContributionLevels;
    }
}