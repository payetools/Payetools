// Copyright (c) 2023-2025, Payetools Ltd.
//
// Payetools Ltd licenses this file to you under the following license(s):
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
    [Obsolete("Use Process(IEnumerable<IEmployeePayRunInputs>, bool, out IPayrollPayRunOutputs) instead. Scheduled for removal in v3.0.0.", false)]
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
    /// <param name="payRunInputs">Input pay run information for each employee in the payrun.</param>
    /// <param name="processInParallel">Set to true to process all employees in parallel, false to process
    /// each employee serially.</param>
    /// <typeparam name="TIdentifier">Identifier type for payrolls, pay runs, etc.</typeparam>
    /// <param name="results">An instance of a class that implements <see cref="IPayrollPayRunOutputs{TIdentifier}"/> containing the
    /// results of this pay run.</param>
    public void Process<TIdentifier>(
        in IPayrollPayRunInputs<TIdentifier> payRunInputs,
        in bool processInParallel,
        out IPayrollPayRunOutputs<TIdentifier> results)
        where TIdentifier : notnull
    {
        IEmployeePayRunOutputs<TIdentifier>[] employeeOutputs;

        if (processInParallel)
        {
            employeeOutputs = new IEmployeePayRunOutputs<TIdentifier>[payRunInputs.EmployeePayRunInputs.Count()];

            var employeePayRunInputs = payRunInputs.EmployeePayRunInputs;

            var indexedInputs = employeePayRunInputs.Select((item, index) => (Item: item, Index: index));

            Parallel.ForEach(indexedInputs, employeeInputs =>
            {
                _payrunCalculator.Process<TIdentifier>(employeeInputs.Item, out var employeeResult);

                employeeOutputs[employeeInputs.Index] = employeeResult;
            });
        }
        else
        {
            employeeOutputs = payRunInputs.EmployeePayRunInputs.Select((employeeInputs, index) =>
            {
                _payrunCalculator.Process(employeeInputs, out var employeeResult);

                return employeeResult;
            }).ToArray();
        }

        results = new PayrollPayRunOutputs<TIdentifier>
        {
            PayRunId = payRunInputs.PayRunId,
            PayDate = _payrunCalculator.PayDate,
            PayPeriod = _payrunCalculator.PayPeriod,
            EmployeePayRunOutputs = employeeOutputs.ToImmutableArray()
        };
    }
}