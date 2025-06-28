// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;
using Payetools.StudentLoans.Model;

namespace Payetools.StudentLoans;

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