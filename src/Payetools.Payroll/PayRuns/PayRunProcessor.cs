// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Payroll.Model;
using System.Collections.Immutable;

namespace Payetools.Payroll.PayRuns;

/// <summary>
/// Processor for processing a payrun for a specific pay reference period and pay date.
/// </summary>
public class PayRunProcessor : IPayRunProcessor
{
    private readonly IPayRunEntryProcessor _payrunCalculator;

    /// <summary>
    /// Initialises a new instance of <see cref="PayRunProcessor"/> with the supplied calculator.
    /// </summary>
    /// <param name="calculator">Calculator to be used to calculate earnings, deductions
    /// and net pay.</param>
    public PayRunProcessor(IPayRunEntryProcessor calculator)
    {
        _payrunCalculator = calculator;
    }

    /// <summary>
    /// Processes this pay run.
    /// </summary>
    /// <param name="employer">Employer that this processing relates to.</param>
    /// <param name="employeePayRunEntries">List of payrun information for each employee in the payrun.</param>
    /// <param name="result">An instance of a class that implements <see cref="IPayRunResult"/> containing the results
    /// of this payrun.</param>
    public void Process(IEmployer employer, List<IEmployeePayRunInputEntry> employeePayRunEntries, out IPayRunResult result)
    {
        result = new PayRunResult(
            employer,
            new PayRunDetails(_payrunCalculator.PayDate, _payrunCalculator.PayPeriod),
            employeePayRunEntries.Select(entry =>
                {
                    _payrunCalculator.Process(entry, out var employeeResult);
                    return employeeResult;
                })
                .ToImmutableArray());
    }
}