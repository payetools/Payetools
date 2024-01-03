// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Payetools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

using Payetools.NationalInsurance.Model;

namespace Payetools.Employment.Model;

/// <summary>
/// Interface for types that represent the historical set of information for an employee's payroll for the
/// current tax year.
/// </summary>
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
    decimal SharedParentalPayYtd { get; }

    /// <summary>
    /// Gets any statutory parental bereavement pay paid to date this tax year.
    /// </summary>
    decimal StatutoryParentalBereavementPayYtd { get; }

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
    /// Gets the graduate loan deductions made to date this tax year.
    /// </summary>
    decimal GraduateLoanRepaymentsYtd { get; }

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
    IDeductionHistoryYtd DeductionHistoryYtd { get; }
}