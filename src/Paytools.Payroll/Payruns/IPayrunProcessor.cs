// Copyright (c) 2023 Paytools Foundation.
//
// Paytools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Paytools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

using Paytools.Payroll.Model;

namespace Paytools.Payroll.Payruns;

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