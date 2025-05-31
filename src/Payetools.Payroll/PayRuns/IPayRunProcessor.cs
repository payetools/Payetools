// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
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
    [Obsolete("Use Process(IEnumerable<IEmployeePayRunInputs>, bool, out IPayrollPayRunOutputs) instead.")]
    void Process(
        in IEmployer employer,
        in IEnumerable<IEmployeePayRunInputEntry> employeePayRunEntries,
        out IPayRunResult result);

    /// <summary>
    /// Processes the pay run for a set of employee pay run inputs and returns the results.
    /// </summary>
    /// <param name="employeePayRunInputs">Input pay run information for each employee in the payrun.</param>
    /// <param name="processInParallel">Set to true to process all employees in parallel, false to process
    /// each employee serially.</param>
    /// <param name="results">An instance of a class that implements <see cref="IPayrollPayRunOutputs"/> containing the
    /// results of this payrun.</param>
    void Process(
        in IEnumerable<IEmployeePayRunInputs> employeePayRunInputs,
        in bool processInParallel,
        out IPayrollPayRunOutputs results);
}