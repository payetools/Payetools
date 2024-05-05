// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.IncomeTax.Model;
using Payetools.NationalInsurance.Model;
using Payetools.Pensions.Model;
using Payetools.Statutory.AttachmentOfEarnings;
using Payetools.StudentLoans.Model;

namespace Payetools.Payroll.Model;

/// <summary>
/// Interface that represents a payrun result for an employee for a specific payrun.
/// </summary>
public interface IEmployeePayRunResult
{
    /// <summary>
    /// Gets the employee's employment details used in calculating this pay run result.  The PayrollId property of
    /// this field can be used as a handle to get access to the employee.
    /// </summary>
    public IEmployment Employment { get; }

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
    ref IStudentLoanCalculationResult? StudentLoanCalculationResult { get; }

    /// <summary>
    /// Gets the results of this employee's pension calculation for this payrun, if applicable;
    /// null otherwise.
    /// </summary>
    ref IPensionContributionCalculationResult? PensionContributionCalculationResult { get; }

    /// <summary>
    /// Gets the results of any attachment of earnings order calculation for this employee for this
    /// payrun, if applicable.
    /// </summary>
    ref IAttachmentOfEarningsCalculationResult? AttachmentOfEarningsCalculationResult { get; }

    /// <summary>
    /// Gets the employee's total gross pay, excluding payrolled taxable benefits.
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
    /// Gets the total amount of payrolled benefits in the period, where applicable.  Null if no
    /// payrolled benefits have been applied.
    /// </summary>
    decimal? PayrollBenefitsInPeriod { get; }

    /// <summary>
    /// Gets the historical set of information for an employee's payroll for the current tax year,
    /// including the effect of this payrun.
    /// </summary>
    ref IEmployeePayrollHistoryYtd EmployeePayrollHistoryYtd { get; }

    /// <summary>
    /// Gets a value indicating whether this employee is being recorded as left employment in this pay run.
    /// Note that the employee's leaving date may be before the start of the pay period for this pay run.
    /// </summary>
    bool IsLeaverInThisPayRun { get; }

    /// <summary>
    /// Gets a value indicating whether an ex-employee is being paid after the leaving date has been reported to
    /// HMRC in a previous submission.
    /// </summary>
    bool IsPaymentAfterLeaving { get; }

    /// <summary>
    /// Gets a value indicating whether the employee has shared parental pay in this pay run.
    /// </summary>
    bool HasSharedParentalPayInPeriod { get; }
}
