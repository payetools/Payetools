// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;

namespace Payetools.Payroll.Model;

/// <summary>
/// Interface that represents a new starter for employment purposes.
/// </summary>
public interface INewStarterInfo
{
    /// <summary>
    /// Gets the employee's starter declaration; null if it was not possible to
    /// obtain a starter declaration from the employee.
    /// </summary>
    public StarterDeclaration? StarterDeclaration { get; }

    /// <summary>
    /// Gets a value indicating whether student loan deductions should continue.
    /// </summary>
    /// <remarks>As a P45 from a previous employer does not indicate the student loan type,
    /// it may be necessary to request the employee's student loan plan type separately.</remarks>
    public bool StudentLoanDeductionNeeded { get; }

    /// <summary>
    /// Gets any applicable student loan type, if known.  (See <see cref="StudentLoanDeductionNeeded"/>).
    /// </summary>
    public StudentLoanType? StudentLoanType { get; }

    /// <summary>
    /// Gets a value indicating whether postgraduate loan deductions should continue.
    /// </summary>
    public bool PostgraduateLoanDeductionNeeded { get; }
}
