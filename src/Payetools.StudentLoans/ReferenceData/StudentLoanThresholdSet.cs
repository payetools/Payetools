// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Payetools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

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