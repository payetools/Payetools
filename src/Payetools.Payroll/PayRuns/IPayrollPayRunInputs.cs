// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Payroll.Model;

namespace Payetools.Payroll.PayRuns;

/// <summary>
/// Interface that provides access to the inputs needed to prepare a pay run for a given payroll.
/// </summary>
public interface IPayrollPayRunInputs
{
    /// <summary>
    /// Gets the unique identifier for this payrun.
    /// </summary>
    object PayRunId { get; }

    /// <summary>
    /// Gets the <see cref="IPayRunDetails"/> for this payrun, which provides access to the pay date, the pay period
    /// and the pay frequency.
    /// </summary>
    IPayRunDetails PayRunDetails { get; }

    /// <summary>
    /// Gets the set of employee pay run inputs for this payrun.
    /// </summary>
    IEnumerable<IEmployeePayRunInputs> EmployeePayRunInputs { get; }
}