// Copyright (c) 2023 Paytools Foundation.
//
// Paytools Foundation licenses this file to you under one of the following licenses:
//
//   * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//   * Paytools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

using Paytools.Common.Model;
using Paytools.Payroll.Model;

namespace Paytools.Payroll.Payruns;

/// <summary>
/// Interface that represent types that can process an employee's set of input payroll data and
/// provide the results of the calculations in the form of an <see cref="IEmployeePayrunResult"/>.
/// </summary>
public interface IPayrunEntryProcessor
{
    /// <summary>
    /// Gets the pay date for this payrun calculator.
    /// </summary>
    PayDate PayDate { get; }

    /// <summary>
    /// Gets the pay period for this payrun calculator.
    /// </summary>
    PayReferencePeriod PayPeriod { get; }

    /// <summary>
    /// Processes the supplied payrun entry calculating all the earnings and deductions, income tax, national insurance and
    /// other statutory deductions, and generating a result structure which includes the final net pay.
    /// </summary>
    /// <param name="entry">Instance of <see cref="IEmployeePayrunInputEntry"/> containing all the necessary input data for the
    /// payroll calculation.</param>
    /// <param name="result">An instance of <see cref="IEmployeePayrunResult"/> containing the results of the payroll calculations.</param>
    void Process(IEmployeePayrunInputEntry entry, out IEmployeePayrunResult result);
}
