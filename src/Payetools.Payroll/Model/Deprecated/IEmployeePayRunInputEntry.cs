// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.AttachmentOrders.Model;
using Payetools.Pensions.Model;
using System.Collections.Immutable;

namespace Payetools.Payroll.Model;

/// <summary>
/// Represents an employee payrun entry, i.e., all the information needed to prepare the payroll
/// record for the employee for the pay period in question.
/// </summary>
[Obsolete("Use IEmployeePayRunInputs instead. Scheduled for removal in v3.0.0.", false)]
public interface IEmployeePayRunInputEntry
{
    /// <summary>
    /// Gets the employment details for the employee for this entry.  (Use the EmployeeId of the Employment to
    /// get access to the employee and related data.)
    /// </summary>
    IEmployment Employment { get; }

    /// <summary>
    /// Gets the list of deductions for this employee for a given payrun.  May be empty.
    /// </summary>
    ImmutableArray<IDeductionEntry> Deductions { get; }

    /// <summary>
    /// Gets the list of pay components for this employee for a given payrun.  May be empty but usually not.
    /// </summary>
    ImmutableArray<IEarningsEntry> Earnings { get; }

    /// <summary>
    /// Gets the list of payrolled benefits for this employee for a given payrun.  Empty if the employee has
    /// no payrolled benefits.
    /// </summary>
    ImmutableArray<IPayrolledBenefitForPeriod> PayrolledBenefits { get; }

    /// <summary>
    /// Gets the list of attachment of earnings orders for this employee for a given payrun.  Null (or empty) if the
    /// employee does not have any attachment of earnings orders.
    /// </summary>
    ImmutableArray<IAttachmentOrder>? AttachmentOfEarningsOrders { get; }

    /// <summary>
    /// Gets the pension contributions to apply for this pay period.. Null if no pension payments are being made.
    /// </summary>
    IPensionContributionLevels? PensionContributionLevels { get; }

    /// <summary>
    /// Gets a value indicating whether this employee is being recorded as left employment in this pay run.
    /// Note that the employee's leaving date may be before the start of the pay period for this pay run.
    /// </summary>
    bool IsLeaverInThisPayRun { get; }

    /// <summary>
    /// Gets a value indicating whether an ex-employee is being paid after the leaving date has been reported to
    /// HMRC in a previous submission.
    /// </summary>
    bool IsPaymentAfterLeaving { get; }
}