// Copyright (c) 2023 Paytools Foundation.
//
// Paytools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Paytools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

using Paytools.Employment.Model;
using Paytools.IncomeTax.Model;
using Paytools.NationalInsurance.Model;
using Paytools.Pensions.Model;
using Paytools.StudentLoans.Model;

namespace Paytools.Payroll.Model;

/// <summary>
/// Interface that represents a payrun entry for one employee for a specific payrun.
/// </summary>
public interface IEmployeePayrunResult
{
    /// <summary>
    /// Gets information about this payrun.
    /// </summary>
    ref IPayrunInfo PayrunInfo { get; }

    /// <summary>
    /// Gets the employee's details.
    /// </summary>
    IEmployee Employee { get; }

    /// <summary>
    /// Gets a value indicating whether this employee is being recorded as left employment in this payrun.  Note that
    /// the employee's leaving date may be before the start of the pay period for this payrun.
    /// </summary>
    bool IsLeaverInThisPayrun { get; }

    /// <summary>
    /// Gets the results of this employee's income tax calculation for this payrun.
    /// </summary>
    ref ITaxCalculationResult TaxCalculationResult { get; }

    /// <summary>
    /// Gets the results of this employee's National Insurance calculation for this payrun.
    /// </summary>
    ref INiCalculationResult NiCalculationResult { get; }

    /// <summary>
    /// Gets the results of this employee's student loan calculation for this payrun, if applicable;
    /// null otherwise.
    /// </summary>
    ref IStudentLoanCalculationResult StudentLoanCalculationResult { get; }

    /// <summary>
    /// Gets the results of this employee's pension calculation for this payrun, if applicable.;
    /// null otherwise.
    /// </summary>
    ref IPensionContributionCalculationResult PensionContributionCalculationResult { get; }

    /// <summary>
    /// Gets the employee's total gross pay.
    /// </summary>
    decimal TotalGrossPay { get; }

    /// <summary>
    /// Gets the employee's total pay that is subject to National Insurance.
    /// </summary>
    decimal NicablePay { get; }

    /// <summary>
    /// Gets the employee's total taxable pay, before the application of any tax-free pay.
    /// </summary>
    decimal TaxablePay { get; }

    /// <summary>
    /// Gets the employee's final net pay.
    /// </summary>
    decimal NetPay { get; }

    /// <summary>
    /// Gets the historical set of information for an employee's payroll for the current tax year,
    /// not including the effect of this payrun.
    /// </summary>
    ref IEmployeePayrollHistoryYtd EmployeePayrollHistoryYtd { get; }
}
