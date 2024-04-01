// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;

namespace Payetools.StudentLoans.Model;

/// <summary>
/// Interface that types implement to provide access to the results of a student loan calculation.
/// </summary>
public interface IStudentLoanCalculationResult
{
    /// <summary>
    /// Gets the optional student loan type.  Null if no student loan applied.  (Post-graduate loans
    /// are treated separately via <see cref="HasPostGradLoan"/>.
    /// </summary>
    StudentLoanType? StudentLoanType { get; }

    /// <summary>
    /// Gets a value indicating whether post-graduate loan deductions were applied.
    /// </summary>
    bool HasPostGradLoan { get; }

    /// <summary>
    /// Gets the student loan threshold for the period used in calculating student loan
    /// (but not post-graduate loan) deductions.
    /// </summary>
    decimal? StudentLoanThresholdUsed { get; }

    /// <summary>
    /// Gets the post-graduate loan threshold for the period used in calculating student loan
    /// (but not student loan) deductions.
    /// </summary>
    decimal? PostGradLoanThresholdUsed { get; }

    /// <summary>
    /// Gets the student loan deduction applied (excluding post-grad loan deductions).
    /// amounts.
    /// </summary>
    decimal StudentLoanDeduction { get; }

    /// <summary>
    /// Gets the post-graduate loan deduction applied.
    /// amounts.
    /// </summary>
    decimal PostgraduateLoanDeduction { get; }

    /// <summary>
    /// Gets the total deduction to be made, the sum of any student and post-graduate loan deduction
    /// amounts.
    /// </summary>
    decimal TotalDeduction { get; }
}
