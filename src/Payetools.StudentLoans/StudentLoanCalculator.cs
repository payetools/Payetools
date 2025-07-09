// Copyright (c) 2023-2025, Payetools Ltd.
//
// Payetools Ltd licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;
using Payetools.StudentLoans.Model;
using Payetools.StudentLoans.ReferenceData;

namespace Payetools.StudentLoans;

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
            PostgraduateLoanDeduction = postGradLoanDeduction,
            TotalDeduction = studentLoanDeduction + postGradLoanDeduction
        };
    }
}