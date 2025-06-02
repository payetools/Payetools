// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

#pragma warning disable SA1402 // File may only contain a single type

using Payetools.Common.Model;
using Payetools.NationalInsurance.Model;
using Payetools.Payroll.PayRuns;

namespace Payetools.Payroll.Model;

/// <summary>
/// Interface for types that represent the historical set of information for an employee's payroll for the
/// current tax year.
/// </summary>
/// <remarks>Added Statutory Neonatal Care Pay applicable from April 2025.</remarks>
public interface IEmployeePayrollHistoryYtd : IEmployeeCoreYtdFigures
{
    /// <summary>
    /// Gets any statutory maternity pay paid to date this tax year.
    /// </summary>
    decimal StatutoryMaternityPayYtd { get; }

    /// <summary>
    /// Gets any statutory paternity pay paid to date this tax year.
    /// </summary>
    decimal StatutoryPaternityPayYtd { get; }

    /// <summary>
    /// Gets any statutory adoption pay paid to date this tax year.
    /// </summary>
    decimal StatutoryAdoptionPayYtd { get; }

    /// <summary>
    /// Gets any statutory parental pay paid to date this tax year.
    /// </summary>
    decimal StatutorySharedParentalPayYtd { get; }

    /// <summary>
    /// Gets any statutory parental bereavement pay paid to date this tax year.
    /// </summary>
    decimal StatutoryParentalBereavementPayYtd { get; }

    /// <summary>
    /// Gets any statutory neonatal care pay paid to date this tax year.
    /// </summary>
    decimal StatutoryNeonatalCarePayYtd { get; }

    /// <summary>
    /// Gets any statutory sickness pay paid to date this tax year.
    /// </summary>
    decimal StatutorySickPayYtd { get; }

    /// <summary>
    /// Gets the National Insurance payment history for the current tax year.  Employees may
    /// transition between NI categories during the tax year and each NI category's payment
    /// record must be retained.
    /// </summary>
    [Obsolete("Use NiHistory instead. Scheduled for removal in v3.0.0.", false)]
    NiYtdHistory EmployeeNiHistoryEntries { get; }

    /// <summary>
    /// Gets the gross pay paid to date this tax year.
    /// </summary>
    decimal GrossPayYtd { get; }

    /// <summary>
    /// Gets the student loan deductions made to date this tax year.
    /// </summary>
    decimal StudentLoanRepaymentsYtd { get; }

    /// <summary>
    /// Gets the postgraduate loan deductions made to date this tax year.
    /// </summary>
    decimal PostgraduateLoanRepaymentsYtd { get; }

    /// <summary>
    /// Gets the amount accrued against payrolled benefits to date this tax year.
    /// </summary>
    decimal PayrolledBenefitsYtd { get; }

    /// <summary>
    /// Gets the total employee pension contributions made under a net pay arrangement to date this tax year.
    /// </summary>
    decimal EmployeePensionContributionsUnderNpaYtd { get; }

    /// <summary>
    /// Gets the total employee pension contributions made under relief at source to date this tax year.
    /// </summary>
    decimal EmployeePensionContributionsUnderRasYtd { get; }

    /// <summary>
    /// Gets the total employer pension contributions made to date this tax year.
    /// </summary>
    decimal EmployerPensionContributionsYtd { get; }

    /// <summary>
    /// Gets the employee's earnings history for the tax year to date.
    /// </summary>
    IEarningsHistoryYtd EarningsHistoryYtd { get; }

    /// <summary>
    /// Gets the employee's deduction history for the tax year to date.
    /// </summary>
    IDeductionsHistoryYtd DeductionsHistoryYtd { get; }

    /// <summary>
    /// Adds the results of the pay run provided to the current instance and returns a new instance of
    /// <see cref="IEmployeePayrollHistoryYtd"/>.</summary>
    /// <param name="payRunInput">Employee pay run input entry.</param>
    /// <param name="payrunResult">Results of a set of payroll calculations for a given employee.</param>
    /// <returns>New instance of <see cref="IEmployeePayrollHistoryYtd"/> with the calculation results
    /// applied.</returns>
    [Obsolete("Use Add(IEmployeePayRunInputs, IEmployeePayRunOutputs) instead. Scheduled for removal in v3.0.0.", false)]
    IEmployeePayrollHistoryYtd Add(IEmployeePayRunInputEntry payRunInput, IEmployeePayRunResult payrunResult);
}

/// <summary>
/// Interface for types that represent the historical set of information for an employee's payroll for
/// the current tax year. <see cref="EmployeeId"/> is used to identify the employee.  Also adds the
/// tax year ending value, to identify which tax year this payroll history applies to.
/// </summary>
/// <typeparam name="TIdentifier">Type of the employee identifier.</typeparam>
/// <remarks>Use this interface in preference to the non-generic type if it is necessary to identify
/// payroll history by employee.</remarks>
public interface IEmployeePayrollHistoryYtd<TIdentifier> : IEmployeePayrollHistoryYtd
    where TIdentifier : notnull
{
    /// <summary>
    /// Gets the unique identifier for the employee that this payroll history applies to.
    /// </summary>
    TIdentifier EmployeeId { get; init; }

    /// <summary>
    /// Gets the tax year ending value for this payroll history.
    /// </summary>
    TaxYearEnding TaxYearEnding { get; init; }

    /// <summary>
    /// Adds the results of the pay run provided to the current instance and returns a new instance of
    /// <see cref="IEmployeePayrollHistoryYtd"/>.
    /// </summary>
    /// <param name="payDate">Pay date for the pay run.</param>
    /// <param name="employeePayRunInputs">Employee pay run inputs.</param>
    /// <param name="employeePayRunOutputs">Employee pay run outputs.</param>
    /// <returns>New instance of <see cref="IEmployeePayrollHistoryYtd"/> with the calculation results
    /// applied.</returns>
    IEmployeePayrollHistoryYtd<TIdentifier> Add(
        PayDate payDate,
        IEmployeePayRunInputs<TIdentifier> employeePayRunInputs,
        IEmployeePayRunOutputs<TIdentifier> employeePayRunOutputs);
}