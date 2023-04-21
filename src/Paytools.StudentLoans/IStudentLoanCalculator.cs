// Copyright (c) 2023 Paytools Foundation.
//
// Paytools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Paytools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

using Paytools.Common.Model;
using Paytools.StudentLoans.Model;

namespace Paytools.StudentLoans;

/// <summary>
/// Interface that student loan calculators must implement.
/// </summary>
public interface IStudentLoanCalculator
{
    /// <summary>
    /// Calculates the necessary student loan deduction based on the input salary, the student loan type (if applicable) and
    /// indication of whether to apply post-graduate loan deductions.
    /// </summary>
    /// <param name="grossSalary">Gross salary.</param>
    /// <param name="studentLoanType">Optional student loan type.  Null if no student loan applies.</param>
    /// <param name="hasPostGradLoan">True if post-graduate loan deductions should be applied; false otherwise.</param>
    /// <param name="result">An implementation of <see cref="IStudentLoanCalculationResult"/> containing the results of the calculation.</param>
    void Calculate(decimal grossSalary, StudentLoanType? studentLoanType, bool hasPostGradLoan, out IStudentLoanCalculationResult result);
}
