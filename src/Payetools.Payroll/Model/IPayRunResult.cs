// Copyright (c) 2023-2025, Payetools Ltd.
//
// Payetools Ltd licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using System.Collections.Immutable;

namespace Payetools.Payroll.Model;

/// <summary>
/// Interface that represents the output of a given pay run.
/// </summary>
[Obsolete("User IPayrollPayRunOutputs instead. Scheduled for removal in v3.0.0.", false)]
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
    ImmutableArray<IEmployeePayRunResult> EmployeePayRunResults { get; }

    /// <summary>
    /// Gets a summary of this pay run, providing totals for all statutory payments.
    /// </summary>
    /// <param name="payRunSummary"><see cref="IPayRunSummary"/> instance that provides summary figures.</param>
    void GetPayRunSummary(out IPayRunSummary payRunSummary);
}