// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Payetools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

using Payetools.Pensions.Model;
using System.Collections.Immutable;

namespace Payetools.Payroll.Model;

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
    public ImmutableList<IDeductionEntry> Deductions { get; }

    /// <summary>
    /// Gets the list of pay components for this employee for a given payrun.  May be empty but usually not.
    /// </summary>
    public ImmutableList<IEarningsEntry> Earnings { get; }

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
        ImmutableList<IEarningsEntry> earnings,
        ImmutableList<IDeductionEntry> deductions,
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