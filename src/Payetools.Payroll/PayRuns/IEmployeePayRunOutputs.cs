// Copyright (c) 2023-2025, Payetools Ltd.
//
// Payetools Ltd licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.AttachmentOrders.Model;
using Payetools.IncomeTax.Model;
using Payetools.NationalInsurance.Model;
using Payetools.Pensions.Model;
using Payetools.StudentLoans.Model;

namespace Payetools.Payroll.PayRuns;

/// <summary>
/// Provides access to the outputs of a pay run for an employee.
/// </summary>
/// <typeparam name="TIdentifier">Identifier type for payrolls, pay runs, etc.</typeparam>
public interface IEmployeePayRunOutputs<TIdentifier>
{
    /// <summary>
    /// Gets the unique identifier for the employee.
    /// </summary>
    TIdentifier EmployeeId { get; init; }

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
    ref IAttachmentOrdersCalculationResult? AttachmentOfEarningsCalculationResult { get; }

    /// <summary>
    /// Gets the employee's total gross pay, excluding payrolled taxable benefits.
    /// </summary>
    decimal TotalGrossPay { get; }

    /// <summary>
    /// Gets the employee's working gross pay, excluding payrolled taxable benefits and less
    /// any deductions that reduce taxable pay.
    /// </summary>
    decimal WorkingGrossPay { get; }

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
}