// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Payroll.Model;

namespace Payetools.Payroll.Payruns;

/// <summary>
/// Interface that represents a payrun, i.e., the running of payroll for a single pay reference period
/// on a single pay date for a predefined set of employees within one employer's employment.
/// </summary>
public interface IPayrunProcessor
{
    /// <summary>
    /// Processes this payrun.
    /// </summary>
    /// <param name="employeePayrunEntries">List of payrun information for each employee in the payrun.</param>
    /// <param name="result">An instance of a class that implements <see cref="IPayrunResult"/> containing the results
    /// of this payrun.</param>
    void Process(List<IEmployeePayrunInputEntry> employeePayrunEntries, out IPayrunResult result);
}