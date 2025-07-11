﻿// Copyright (c) 2023-2025, Payetools Ltd.
//
// Payetools Ltd licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Payroll.Model;

namespace Payetools.Payroll.PayRuns;

/// <summary>
/// Interface that represents a payrun, i.e., the running of payroll for a single pay reference period
/// on a single pay date for a predefined set of employees within one employer's employment.
/// </summary>
public interface IPayRunProcessor
{
    /// <summary>
    /// Processes this payrun.
    /// </summary>
    /// <param name="employer">Employer that this processing relates to.</param>
    /// <param name="employeePayRunEntries">Payrun information for each employee in the payrun.</param>
    /// <param name="result">An instance of a class that implements <see cref="IPayRunResult"/> containing the results
    /// of this payrun.</param>
    [Obsolete("Use Process(IEnumerable<IEmployeePayRunInputs>, bool, out IPayrollPayRunOutputs) instead. Scheduled for removal in v3.0.0.", false)]
    void Process(
        in IEmployer employer,
        in IEnumerable<IEmployeePayRunInputEntry> employeePayRunEntries,
        out IPayRunResult result);

    /// <summary>
    /// Processes the pay run for a set of employee pay run inputs and returns the results.
    /// </summary>
    /// <param name="payRunInputs">Input pay run information with record for each employee in the payrun.</param>
    /// <param name="processInParallel">Set to true to process all employees in parallel, false to process
    /// each employee serially.</param>
    /// <typeparam name="TIdentifier">Identifier type for payrolls, pay runs, etc.</typeparam>
    /// <param name="results">An instance of a class that implements <see cref="IPayrollPayRunOutputs{TIdentifier}"/> containing the
    /// results of this payrun.</param>
    void Process<TIdentifier>(
        in IPayrollPayRunInputs<TIdentifier> payRunInputs,
        in bool processInParallel,
        out IPayrollPayRunOutputs<TIdentifier> results)
        where TIdentifier : notnull;
}