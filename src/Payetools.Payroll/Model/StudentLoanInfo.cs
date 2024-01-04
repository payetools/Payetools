// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;

namespace Payetools.Payroll.Model;

/// <summary>
/// Struct that holds student loan status information for an employee.
/// </summary>
public readonly struct StudentLoanInfo
{
    /// <summary>
    /// Gets the student loan applicable for an employee.  Null if the employee does not have an
    /// outstanding student loan.
    /// </summary>
    public StudentLoanType? StudentLoanType { get; init; }

    /// <summary>
    /// Gets a value indicating whether the employee has an outstanding post-graduate loan.
    /// </summary>
    public bool HasPostGradLoan { get; init; }
}