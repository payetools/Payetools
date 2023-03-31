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

using Paytools.Common.Model;
using Paytools.StudentLoans.Model;
using Paytools.StudentLoans.ReferenceData;

namespace Paytools.StudentLoans;

/// <summary>
/// Represents a student loan calculator that implements <see cref="IStudentLoanCalculator"/>.
/// </summary>
public class StudentLoanCalculator : IStudentLoanCalculator
{
    private readonly IStudentLoanThresholdSet _thresholds;
    private readonly IStudentLoanRateSet _rates;

    /// <summary>
    /// Initialises a new instance of a <see cref="StudentLoanCalculator"/> with the supplied thresholds and rates.  Note that
    /// supplied thresholds have been adjusted to correspond to the appropriate pay frequency for this calculator instance.
    /// </summary>
    /// <param name="thresholds">Thresholds adjusted to match pay frequency for this calculator.</param>
    /// <param name="rates">Student and post-grad loan deduction rates.</param>
    public StudentLoanCalculator(IStudentLoanThresholdSet thresholds, IStudentLoanRateSet rates)
    {
        _thresholds = thresholds;
        _rates = rates;
    }

    /// <summary>
    /// Calculates the necessary student loan deduction based on the input salary, the student loan type (if applicable) and
    /// indication of whether to apply post-graduate loan deductions.
    /// </summary>
    /// <param name="grossSalary">Gross salary.</param>
    /// <param name="studentLoanType">Optional student loan type.  Null if no student loan applies.</param>
    /// <param name="hasPostGradLoan">True if post-graduate loan deductions should be applied; false otherwise.</param>
    /// <param name="result">An implementation of <see cref="IStudentLoanCalculationResult"/> containing the results of the calculation.</param>
    public void Calculate(decimal grossSalary, StudentLoanType? studentLoanType, bool hasPostGradLoan, out IStudentLoanCalculationResult result)
    {
        if (studentLoanType == null && !hasPostGradLoan)
            throw new InvalidOperationException("Student loan calculations can only be performed when a student and/or post-graduate loan is in place");

        decimal? studentLoanThreshold = null;
        var studentLoanDeduction = 0.0m;
        var postGradLoanDeduction = 0.0m;

        if (studentLoanType != null)
        {
            studentLoanThreshold = studentLoanType switch
            {
                StudentLoanType.Plan1 => _thresholds.Plan1PerPeriodThreshold,
                StudentLoanType.Plan2 => _thresholds.Plan2PerPeriodThreshold,
                StudentLoanType.Plan4 => _thresholds.Plan4PerPeriodThreshold,
                _ => throw new ArgumentOutOfRangeException(nameof(studentLoanType), "Unrecognised value for student loan type")
            };

            if (grossSalary > studentLoanThreshold)
                studentLoanDeduction = decimal.Round((grossSalary - (decimal)studentLoanThreshold) * _rates.Student, 0, MidpointRounding.ToZero);
        }

        if (hasPostGradLoan && grossSalary > _thresholds.PostGradPerPeriodThreshold)
            postGradLoanDeduction = decimal.Round((grossSalary - _thresholds.PostGradPerPeriodThreshold) * _rates.PostGrad, 0, MidpointRounding.ToZero);

        result = new StudentLoanCalculationResult()
        {
            StudentLoanType = studentLoanType,
            HasPostGradLoan = hasPostGradLoan,
            StudentLoanThresholdUsed = studentLoanThreshold,
            PostGradLoanThresholdUsed = hasPostGradLoan ? _thresholds.PostGradPerPeriodThreshold : null,
            StudentLoanDeduction = studentLoanDeduction,
            PostGraduateLoanDeduction = postGradLoanDeduction,
            TotalDeduction = studentLoanDeduction + postGradLoanDeduction
        };
    }
}