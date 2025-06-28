// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Payroll.PayRuns;

namespace Payetools.Payroll.Model;

/// <summary>
/// Interface that represents a payrun result for an employee for a specific payrun.
/// </summary>
public interface IEmployeePayRunResult : IEmployeePayRunOutputs<object>
{
    /// <summary>
    /// Gets the employee's employment details for this pay run.
    /// </summary>
    IEmployment Employment { get; }

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

    /// <summary>
    /// Gets a value indicating whether the employee has shared parental pay in this pay run.
    /// </summary>
    bool HasSharedParentalPayInPeriod { get; }
}