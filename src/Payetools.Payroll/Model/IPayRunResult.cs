// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;

namespace Payetools.Payroll.Model;

/// <summary>
/// Interface that represents the output of a given pay run.
/// </summary>
public interface IPayRunResult
{
    /// <summary>
    /// Gets the employer that this pay run result pertains to.
    /// </summary>
    IEmployer Employer { get; }

    /// <summary>
    /// Gets the pay date for this pay run.
    /// </summary>
    PayDate PayDate { get; }

    /// <summary>
    /// Gets the list of employee pay run entries.
    /// </summary>
    List<IEmployeePayRunResult> EmployeePayRunEntries { get; }
}
