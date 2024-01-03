// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Payetools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

using Payetools.Employment.Model;
using Payetools.Payroll.Model;

namespace Payetools.Payroll.Payruns;

/// <summary>
/// Represents a payrun, i.e., the running of payroll for a single pay reference period
/// on a single pay date for a predefined set of employees within one employer's employment.
/// </summary>
public class PayrunProcessor : IPayrunProcessor
{
    private readonly IPayrunEntryProcessor _payrunCalculator;
    private readonly IEmployer _employer;

    /// <summary>
    /// Initialises a new instance of <see cref="PayrunProcessor"/> with the supplied calculator.
    /// </summary>
    /// <param name="calculator">Calculator to be used to calculate earnings, deductions
    /// and net pay.</param>
    /// <param name="employer">Employer that this payrun processor relates to.</param>
    public PayrunProcessor(IPayrunEntryProcessor calculator, IEmployer employer)
    {
        _payrunCalculator = calculator;
        _employer = employer;
    }

    /// <summary>
    /// Processes this payrun.
    /// </summary>
    /// <param name="employeePayrunEntries">List of payrun information for each employee in the payrun.</param>
    /// <param name="result">An instance of a class that implements <see cref="IPayrunResult"/> containing the results
    /// of this payrun.</param>
    public void Process(List<IEmployeePayrunInputEntry> employeePayrunEntries, out IPayrunResult result)
    {
        var payrunOutputs = new List<IEmployeePayrunResult>();

        for (int i = 0; i < employeePayrunEntries.Count; i++)
        {
            _payrunCalculator.Process(employeePayrunEntries[i], out var employeeResult);

            payrunOutputs.Add(employeeResult);
        }

        result = new PayrunResult()
        {
            EmployeePayrunEntries = payrunOutputs,
            Employer = _employer,
            PayDate = _payrunCalculator.PayDate
        };
    }
}
