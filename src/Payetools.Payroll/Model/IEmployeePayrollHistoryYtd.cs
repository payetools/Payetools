﻿// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.NationalInsurance.Model;

namespace Payetools.Payroll.Model;

/// <summary>
/// Interface for types that represent the historical set of information for an employee's payroll for the
/// current tax year.
/// </summary>
/// <remarks>Added Statutory Neonatal Care Pay applicable from April 2025.</remarks>
public interface IEmployeePayrollHistoryYtd
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
    NiYtdHistory EmployeeNiHistoryEntries { get; }

    /// <summary>
    /// Gets the gross pay paid to date this tax year.
    /// </summary>
    decimal GrossPayYtd { get; }

    /// <summary>
    /// Gets the taxable pay paid to date this tax year.
    /// </summary>
    decimal TaxablePayYtd { get; }

    /// <summary>
    /// Gets the NI-able pay paid to date this tax year.
    /// </summary>
    decimal NicablePayYtd { get; }

    /// <summary>
    /// Gets the income tax paid to date this tax year.
    /// </summary>
    decimal TaxPaidYtd { get; }

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
    /// Gets the tax that it has not been possible to collect so far this tax year due to the
    /// regulatory limit on income tax deductions.
    /// </summary>
    decimal TaxUnpaidDueToRegulatoryLimit { get; }

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
    IEmployeePayrollHistoryYtd Add(IEmployeePayRunInputEntry payRunInput, IEmployeePayRunResult payrunResult);
}