// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

namespace Payetools.StudentLoans.ReferenceData;

/// <summary>
/// Type that provides access to a specific set of student (and post-grad) loan
/// thresholds for a given plan type and specific period.
/// </summary>
public readonly struct StudentLoanThresholdSet : IStudentLoanThresholdSet
{
    /// <summary>
    /// Gets the period threshold for Plan 1 student loan deductions.
    /// </summary>
    public decimal Plan1PerPeriodThreshold { get; init; }

    /// <summary>
    /// Gets the period threshold for Plan 2 student loan deductions.
    /// </summary>
    public decimal Plan2PerPeriodThreshold { get; init; }

    /// <summary>
    /// Gets the period threshold for Plan 4 student loan deductions.
    /// </summary>
    public decimal Plan4PerPeriodThreshold { get; init; }

    /// <summary>
    /// Gets the period threshold for post-graduate student loan deductions.
    /// </summary>
    public decimal PostGradPerPeriodThreshold { get; init; }
}