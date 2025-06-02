// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;
using Payetools.Payroll.Model;
using System.Collections.Immutable;

namespace Payetools.Payroll.PayRuns;

/// <summary>
/// Interface that represents the output of a payroll pay run, which includes the results for each employee.
/// </summary>
/// <typeparam name="TIdentifier">Identifier type for payrolls, pay runs, etc.</typeparam>
public interface IPayrollPayRunOutputs<TIdentifier>
    where TIdentifier : notnull
{
    /// <summary>
    /// Gets the unique identifier for this pay run.
    /// </summary>
    TIdentifier PayRunId { get; init; }

    /// <summary>
    /// Gets the <see cref="PayDate"/> for this payrun, which provides access to the pay date and the pay frequency.
    /// </summary>
    PayDate PayDate { get; init; }

    /// <summary>
    /// Gets the start and end dates of the pay period that pertains to this payrun, in the form of a <see cref="DateRange"/>.
    /// </summary>
    DateRange PayPeriod { get; init; }

    /// <summary>
    /// Gets the set of employee pay run outputs for this payroll pay run.
    /// </summary>
    ImmutableArray<IEmployeePayRunOutputs<TIdentifier>> EmployeePayRunOutputs { get; init; }

    /// <summary>
    /// Gets a summary of this pay run, providing totals for all statutory payments.
    /// </summary>
    /// <param name="payRunInputs">Instance of <see cref="IPayrollPayRunInputs{TIdentifier}"/> that provides the input data for
    /// the pay run.</param>
    /// <returns><see cref="IPayRunSummary"/> instance that provides summary figures.</returns>
    IPayRunSummary GetPayRunSummary(in IPayrollPayRunInputs<TIdentifier> payRunInputs);
}