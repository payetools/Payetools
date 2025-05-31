// Copyright (c) 2023-2025, Payetools Foundation.
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
    public PayRunProcessor(in IPayRunEntryProcessor calculator)
    {
        _payrunCalculator = calculator;
    }

    /// <summary>
    /// Processes this pay run.
    /// </summary>
    /// <param name="employer">Employer that this processing relates to.</param>
    /// <param name="employeePayRunEntries">Pay run information for each employee in the payrun.</param>
    /// <param name="result">An instance of a class that implements <see cref="IPayRunResult"/> containing the results
    /// of this payrun.</param>
    [Obsolete("Use Process(IEnumerable<IEmployeePayRunInputs>, bool, out IPayrollPayRunOutputs) instead.")]
    public void Process(
        in IEmployer employer,
        in IEnumerable<IEmployeePayRunInputEntry> employeePayRunEntries,
        out IPayRunResult result)
    {
        result = new PayRunResult(
            employer,
            new PayRunDetails(_payrunCalculator.PayDate, _payrunCalculator.PayPeriod),
            employeePayRunEntries,
            employeePayRunEntries.Select(entry =>
                {
                    _payrunCalculator.Process(entry, out var employeeResult);
                    return employeeResult;
                })
                .ToImmutableArray());
    }

    /// <summary>
    /// Processes the pay run for a set of employee pay run inputs and returns the results.
    /// </summary>
    /// <param name="employeePayRunInputs">Input pay run information for each employee in the payrun.</param>
    /// <param name="processInParallel">Set to true to process all employees in parallel, false to process
    /// each employee serially.</param>
    /// <param name="results">An instance of a class that implements <see cref="IPayrollPayRunOutputs"/> containing the
    /// results of this pay run.</param>
    public void Process(
        in IEnumerable<IEmployeePayRunInputs> employeePayRunInputs,
        in bool processInParallel,
        out IPayrollPayRunOutputs results)
    {
        IEmployeePayRunOutputs[] employeeOutputs;

        if (processInParallel)
        {
            employeeOutputs = new IEmployeePayRunOutputs[employeePayRunInputs.Count()];

            var indexedInputs = employeePayRunInputs.Select((item, index) => (Item: item, Index: index));

            Parallel.ForEach(indexedInputs, employeeInputs =>
            {
                _payrunCalculator.Process(employeeInputs.Item, out var employeeResult);

                employeeOutputs[employeeInputs.Index] = employeeResult;
            });
        }
        else
        {
            employeeOutputs = employeePayRunInputs.Select((employeeInputs, index) =>
            {
                _payrunCalculator.Process(employeeInputs, out var employeeResult);

                return employeeResult;
            }).ToArray();
        }

        results = new PayrollPayRunOutput
        {
            PayDate = _payrunCalculator.PayDate,
            PayPeriod = _payrunCalculator.PayPeriod,
            EmployeePayRunOutputs = employeeOutputs.ToImmutableArray()
        };
    }
}