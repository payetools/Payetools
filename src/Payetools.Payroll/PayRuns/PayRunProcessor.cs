// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Payroll.Model;
using Payetools.Payroll.PayRuns;

namespace Payetools.Payroll.PayRuns;

/// <summary>
/// Represents a payrun, i.e., the running of payroll for a single pay reference period
/// on a single pay date for a predefined set of employees within one employer's employment.
/// </summary>
public class PayRunProcessor : IPayRunProcessor
{
    private readonly IPayRunEntryProcessor _payrunCalculator;
    private readonly IEmployer _employer;

    /// <summary>
    /// Initialises a new instance of <see cref="PayRunProcessor"/> with the supplied calculator.
    /// </summary>
    /// <param name="calculator">Calculator to be used to calculate earnings, deductions
    /// and net pay.</param>
    /// <param name="employer">Employer that this payrun processor relates to.</param>
    public PayRunProcessor(IPayRunEntryProcessor calculator, IEmployer employer)
    {
        _payrunCalculator = calculator;
        _employer = employer;
    }

    /// <summary>
    /// Processes this payrun.
    /// </summary>
    /// <param name="employeePayRunEntries">List of payrun information for each employee in the payrun.</param>
    /// <param name="result">An instance of a class that implements <see cref="IPayRunResult"/> containing the results
    /// of this payrun.</param>
    public void Process(List<IEmployeePayRunInputEntry> employeePayRunEntries, out IPayRunResult result)
    {
        var payrunOutputs = new List<IEmployeePayRunResult>();

        for (int i = 0; i < employeePayRunEntries.Count; i++)
        {
            _payrunCalculator.Process(employeePayRunEntries[i], out var employeeResult);

            payrunOutputs.Add(employeeResult);
        }

        result = new PayRunResult()
        {
            EmployeePayRunEntries = payrunOutputs,
            Employer = _employer,
            PayDate = _payrunCalculator.PayDate
        };
    }
}
