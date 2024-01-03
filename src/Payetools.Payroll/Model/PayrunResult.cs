// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;

namespace Payetools.Payroll.Model;

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