// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

namespace Payetools.Payroll.PayRuns;

/// <summary>
/// Interface that represent types that can process an employee's set of input payroll data and
/// provide the results of the calculations in the form of an <see cref="IEmployeePayRunOutputs{TIdentifier}"/>.
/// </summary>
public interface IEmployeePayRunProcessor
{
    /// <summary>
    /// Processes the supplied payrun entry calculating all the earnings and deductions, income tax, national insurance and
    /// other statutory deductions, and generating a result structure which includes the final net pay.
    /// </summary>
    /// <param name="payRunInputs">Instance of <see cref="IEmployeePayRunInputs{TIdentifier}"/> containing all the necessary input data for the
    /// payroll calculation.</param>
    /// <param name="result">An instance of <see cref="IEmployeePayRunOutputs{TIdentifier}"/> containing the results of the payroll calculations.</param>
    /// <typeparam name="TIdentifier">Identifier type for payrolls, pay runs, etc.</typeparam>
    void Process<TIdentifier>(in IEmployeePayRunInputs<TIdentifier> payRunInputs, out IEmployeePayRunOutputs<TIdentifier> result)
        where TIdentifier : notnull;
}