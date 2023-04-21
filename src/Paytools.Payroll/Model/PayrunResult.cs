// Copyright (c) 2023 Paytools Foundation.
//
// Paytools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Paytools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

using Paytools.Common.Model;
using Paytools.Employment.Model;
using System.Collections.Concurrent;

namespace Paytools.Payroll.Model;

/// <summary>
/// Represents the output of a payrun.
/// </summary>
public record PayrunResult : IPayrunResult
{
    /// <summary>
    /// Gets the employer that this payrun refers to.
    /// </summary>
    public IEmployer Employer { get; init; } = null!;

    /// <summary>
    /// Gets the pay date for this payrun.
    /// </summary>
    public PayDate PayDate { get; init; }

    /// <summary>
    /// Gets the list of employee payrun entries.
    /// </summary>
    public List<IEmployeePayrunResult> EmployeePayrunEntries { get; init; } = null!;
}