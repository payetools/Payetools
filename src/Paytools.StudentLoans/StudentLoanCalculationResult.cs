// Copyright (c) 2023 Paytools Foundation.  All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License") ~
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace Paytools.StudentLoans;

/// <summary>
/// Represents the result of a student loan calculation.
/// </summary>
public readonly struct StudentLoanCalculationResult : IStudentLoanCalculationResult
{
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
