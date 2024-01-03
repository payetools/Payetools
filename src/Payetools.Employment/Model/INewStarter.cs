// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Payetools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

using Payetools.Common.Model;

namespace Payetools.Employment.Model;

/// <summary>
/// Interface that represents a new starter for employment purposes.
/// </summary>
public interface INewStarter : IEmployee
{
    /// <summary>
    /// Gets or sets the employee's starter declaration; null if it was not possible to
    /// obtain a starter declaration from the employee.
    /// </summary>
    public StarterDeclaration? StarterDeclaration { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether student loan deductions should continue.
    /// </summary>
    /// <remarks>As a P45 from a previous employer does not indicate the student loan type,
    /// it may be necessary to request the employee's student loan plan type separately.</remarks>
    public bool StudentLoanDeductionNeeded { get; set; }

    /// <summary>
    /// Gets or sets any applicable student loan type, if known.  (See <see cref="StudentLoanDeductionNeeded"/>).
    /// </summary>
    public StudentLoanType? StudentLoanType { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether post-graduate loan deductions should continue.
    /// </summary>
    public bool GraduateLoanDeductionNeeded { get; set; }
}
