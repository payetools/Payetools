// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Pensions.Model;
using System.Collections.Immutable;

namespace Payetools.Payroll.Model;

/// <summary>
/// Represents an employee payrun entry, i.e., all the information needed to prepare the payroll
/// record for the employee for the pay period in question.
/// </summary>
public record EmployeePayRunInputEntry : IEmployeePayRunInputEntry
{
    /// <summary>
    /// Gets the employment details for the employee for this entry.  (Use the PayrollId of this field as a
    /// handle to get access to the employee and related data.)
    /// </summary>
    public IEmployment Employment { get; }

    /// <summary>
    /// Gets the list of deductions for this employee for a given payrun.  May be empty.
    /// </summary>
    public ImmutableArray<IDeductionEntry> Deductions { get; }

    /// <summary>
    /// Gets the list of pay components for this employee for a given payrun.  May be empty but usually not.
    /// </summary>
    public ImmutableArray<IEarningsEntry> Earnings { get; }

    /// <summary>
    /// Gets the list of payrolled benefits for this employee for a given payrun.  Empty if the employee has
    /// no payrolled benefits.
    /// </summary>
    public ImmutableArray<IPayrolledBenefitForPeriod> PayrolledBenefits { get; }

    /// <summary>
    /// Gets the pension contributions to apply for this pay period.
    /// </summary>
    public IPensionContributionLevels PensionContributionLevels { get; }

    /// <summary>
    /// Initialises a new instance of <see cref="EmployeePayRunInputEntry"/>.
    /// </summary>
    /// <param name="employment">Employment details.</param>
    /// <param name="earnings">List of applicable earnings, if any.  Empty list if none.</param>
    /// <param name="deductions">List of applicable deductions, if any.  Empty list if none.</param>
    /// <param name="payrolledBenefits">List of payrolled benefits, if any.  Empty list if none.</param>
    /// <param name="pensionContributionLevels">Pension contribtuion levels to be applied.</param>
    public EmployeePayRunInputEntry(
        IEmployment employment,
        ImmutableArray<IEarningsEntry> earnings,
        ImmutableArray<IDeductionEntry> deductions,
        ImmutableArray<IPayrolledBenefitForPeriod> payrolledBenefits,
        IPensionContributionLevels pensionContributionLevels)
    {
        Employment = employment;
        Earnings = earnings;
        Deductions = deductions;
        PayrolledBenefits = payrolledBenefits;
        PensionContributionLevels = pensionContributionLevels;
    }
}