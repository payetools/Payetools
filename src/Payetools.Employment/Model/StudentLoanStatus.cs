// Copyright (c) 2023 Payetools Foundation.
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
/// Struct that holds student loan status information for an employee.
/// </summary>
public readonly struct StudentLoanStatus
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
