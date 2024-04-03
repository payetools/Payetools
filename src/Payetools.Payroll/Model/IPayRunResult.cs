// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;
using System.Collections.Immutable;

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
    /// Gets the pay date and pay period for this pay run.
    /// </summary>
    IPayRunDetails PayRunDetails { get; }

    /// <summary>
    /// Gets the list of employee pay run entries.
    /// </summary>
    ImmutableArray<IEmployeePayRunResult> EmployeePayRunEntries { get; }
}
