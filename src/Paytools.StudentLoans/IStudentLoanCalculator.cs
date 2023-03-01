// Copyright (c) 2023 Paytools Foundation.
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
