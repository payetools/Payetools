// Copyright (c) 2023-2025, Payetools Ltd.
//
// Payetools Ltd licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

namespace Payetools.StudentLoans.ReferenceData;

/// <summary>
/// Interface for types that provide access to a specific set of student (and post-grad) loan
/// thresholds for a given plan type and specific period.
/// </summary>
public interface IStudentLoanThresholdSet
{
    /// <summary>
    /// Gets the period threshold for Plan 1 student loan deductions.
    /// </summary>
    decimal Plan1PerPeriodThreshold { get; }

    /// <summary>
    /// Gets the period threshold for Plan 2 student loan deductions.
    /// </summary>
    decimal Plan2PerPeriodThreshold { get; }

    /// <summary>
    /// Gets the period threshold for Plan 4 student loan deductions.
    /// </summary>
    decimal Plan4PerPeriodThreshold { get; }

    /// <summary>
    /// Gets the period threshold for post-graduate student loan deductions.
    /// </summary>
    decimal PostGradPerPeriodThreshold { get; }
}