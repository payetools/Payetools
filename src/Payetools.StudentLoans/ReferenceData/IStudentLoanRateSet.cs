// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

namespace Payetools.StudentLoans.ReferenceData;

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