// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Payetools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

using Payetools.StudentLoans.ReferenceData;

namespace Payetools.ReferenceData.StudentLoans;

/// <summary>
/// Represents the set of deduction rates to be applied for student and post-graduate loans.
/// </summary>
public readonly struct StudentLoanRatesSet : IStudentLoanRateSet
{
    /// <summary>
    /// Gets the deduction rate for Plan 1, 2 and 4 student loan deductions.
    /// </summary>
    public decimal Student { get; init; }

    /// <summary>
    /// Gets the deduction rate for post-graduate student loan deductions.
    /// </summary>
    public decimal PostGrad { get; init; }
}
