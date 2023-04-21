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
/// Interface for types that detail the various HMRC-published student loan deduction rates.
/// </summary>
public interface IStudentLoanRateSet
{
    /// <summary>
    /// Gets the deduction rate for Plan 1, 2 and 4 student loan deductions.
    /// </summary>
    decimal Student { get; }

    /// <summary>
    /// Gets the deduction rate for post-graduate student loan deductions.
    /// </summary>
    decimal PostGrad { get; }
}