// Copyright (c) 2023 Paytools Foundation.
//
// Paytools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Paytools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

namespace Paytools.StudentLoans.ReferenceData;

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