// Copyright (c) 2023-2025, Payetools Ltd.
//
// Payetools Ltd licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;

namespace Payetools.Payroll.Model;

/// <summary>
/// Provides information about an employee's student and postgraduate loan
/// commitments, if any.
/// </summary>
public interface IStudentLoanInfo
{
    /// <summary>
    /// Gets a value indicating whether the employee has a student loan
    /// that is being repaid through payroll.
    /// </summary>
    bool HasStudentLoan { get; }

    /// <summary>
    /// Gets the type of student loan the employee has, if any.
    /// </summary>
    StudentLoanType? StudentLoanType { get; init; }

    /// <summary>
    /// Gets a value indicating whether the individual has a postgraduate loan
    /// that is being repaid through payroll.
    /// </summary>
    bool HasPostgraduateLoan { get; init; }
}