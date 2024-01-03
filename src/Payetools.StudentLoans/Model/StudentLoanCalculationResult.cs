// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;

namespace Payetools.StudentLoans.Model;

/// <summary>
/// Represents the result of a student loan calculation.
/// </summary>
public readonly struct StudentLoanCalculationResult : IStudentLoanCalculationResult
{
    /// <summary>
    /// Gets an empty <see cref="StudentLoanCalculationResult"/> that indicates that no pension is applicable.
    /// </summary>
    public static StudentLoanCalculationResult NoStudentLoanApplicable => default;

    /// <summary>
    /// Gets the optional student loan type.  Null if no student loan applied.  (Post-graduate loans
    /// are treated separately via <see cref="HasPostGradLoan"/>.
    /// </summary>
    public StudentLoanType? StudentLoanType { get; init; }

    /// <summary>
    /// Gets a value indicating whether post-graduate loan deductions were applied.
    /// </summary>
    public bool HasPostGradLoan { get; init; }

    /// <summary>
    /// Gets the student loan threshold for the period used in calculating student loan
    /// (but not post-graduate loan) deductions.
    /// </summary>
    public decimal? StudentLoanThresholdUsed { get; init; }

    /// <summary>
    /// Gets the post-graduate loan threshold for the period used in calculating student loan
    /// (but not student loan) deductions.
    /// </summary>
    public decimal? PostGradLoanThresholdUsed { get; init; }

    /// <summary>
    /// Gets the student loan deduction applied (excluding post-grad loan deductions).
    /// amounts.
    /// </summary>
    public decimal StudentLoanDeduction { get; init; }

    /// <summary>
    /// Gets the post-graduate loan deduction applied.
    /// amounts.
    /// </summary>
    public decimal PostGraduateLoanDeduction { get; init; }

    /// <summary>
    /// Gets the total deduction to be made, the sum of any student and post-graduate loan deduction
    /// amounts.
    /// </summary>
    public decimal TotalDeduction { get; init; }
}
