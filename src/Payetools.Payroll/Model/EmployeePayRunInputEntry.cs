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
public class EmployeePayRunInputEntry : IEmployeePayRunInputEntry
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
    /// Gets a value indicating whether this employee is being recorded as left employment in this pay run.
    /// Note that the employee's leaving date may be before the start of the pay period for this pay run.
    /// </summary>
    public bool IsLeaverInThisPayRun { get; }

    /// <summary>
    /// Gets a value indicating whether an ex-employee is being paid after the leaving date has been reported to
    /// HMRC in a previous submission.
    /// </summary>
    public bool IsPaymentAfterLeaving { get; }

    /// <summary>
    /// Initialises a new instance of <see cref="EmployeePayRunInputEntry"/>.
    /// </summary>
    /// <param name="employment">Employment details.</param>
    /// <param name="earnings">List of applicable earnings, if any.  Empty list if none.</param>
    /// <param name="deductions">List of applicable deductions, if any.  Empty list if none.</param>
    /// <param name="payrolledBenefits">List of payrolled benefits, if any.  Empty list if none.</param>
    /// <param name="pensionContributionLevels">Pension contribtuion levels to be applied.</param>
    /// <param name="isLeaverInThisPayRun">Should be set to true if the employee needs to be
    /// reported as leaving during this pay run. Defaults to false.</param>
    /// <param name="isPaymentAfterLeaving">Should be set to true if the employee has already been
    /// reported as left but a further payment is being made to them. Defaults to false.</param>
    public EmployeePayRunInputEntry(
        IEmployment employment,
        ImmutableArray<IEarningsEntry> earnings,
        ImmutableArray<IDeductionEntry> deductions,
        ImmutableArray<IPayrolledBenefitForPeriod> payrolledBenefits,
        IPensionContributionLevels pensionContributionLevels,
        bool isLeaverInThisPayRun = false,
        bool isPaymentAfterLeaving = false)
    {
        Employment = employment;
        Earnings = earnings;
        Deductions = deductions;
        PayrolledBenefits = payrolledBenefits;
        PensionContributionLevels = pensionContributionLevels;
        IsLeaverInThisPayRun = isLeaverInThisPayRun;
        IsPaymentAfterLeaving = isPaymentAfterLeaving;
    }
}