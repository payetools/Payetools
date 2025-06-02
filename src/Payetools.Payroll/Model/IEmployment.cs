// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;
using Payetools.NationalInsurance.Model;
using Payetools.Payroll.PayRuns;
using Payetools.Pensions.Model;
using System.Collections.Immutable;

namespace Payetools.Payroll.Model;

/// <summary>
/// Interface that represents an employee's employment for payroll purposes. The PayrollId provides a handle
/// to the employee that this employment refers to.
/// </summary>
public interface IEmployment
{
    /// <summary>
    /// Gets the employee's official employment start date.
    /// </summary>
    DateOnly EmploymentStartDate { get; }

    /// <summary>
    /// Gets the employee's official employment termination date, i.e., their last working
    /// day.  Null if the employee is still employed.
    /// </summary>
    DateOnly? EmploymentEndDate { get; }

    /// <summary>
    /// Gets the employee's tax code.
    /// </summary>
    TaxCode TaxCode { get; }

    /// <summary>
    /// Gets the normal hours worked by the employee in one of several bands established by HMRC.
    /// </summary>
    NormalHoursWorkedBand NormalHoursWorkedBand { get; }

    /// <summary>
    /// Gets the employee's NI category.
    /// </summary>
    NiCategory NiCategory { get; }

    /// <summary>
    /// Gets the employee's payroll ID, as reported to HMRC.  Sometimes known as "works number".
    /// </summary>
    PayrollId PayrollId { get; }

    /// <summary>
    /// Gets a value indicating whether the employee is a company director.
    /// </summary>
    bool IsDirector { get; }

    /// <summary>
    /// Gets the method for calculating National Insurance contributions.  Applicable only
    /// for directors; null otherwise.
    /// </summary>
    DirectorsNiCalculationMethod? DirectorsNiCalculationMethod { get; }

    /// <summary>
    /// Gets the date the employee was appointed as a director, where appropriate; null otherwise.
    /// </summary>
    DateOnly? DirectorsAppointmentDate { get; }

    /// <summary>
    /// Gets the date the employee ceased to be a director, where appropriate; null otherwise.
    /// </summary>
    DateOnly? CeasedToBeDirectorDate { get; }

    /// <summary>
    /// Gets the employee's current student loan status.
    /// </summary>
    StudentLoanInfo? StudentLoanInfo { get; }

    /// <summary>
    /// Gets the employee's primary pay structure.
    /// </summary>
    IEmployeePayStructure PrimaryPayStructure { get; }

    /// <summary>
    /// Gets the pension scheme that the employee is a member of.  Null if they are not a member of
    /// any scheme.
    /// </summary>
    IPensionScheme? PensionScheme { get; }

    /// <summary>
    /// Gets a value indicating whether the employee is paid on an irregular basis.
    /// </summary>
    bool IsIrregularlyPaid { get;  }

    /// <summary>
    /// Gets a value indicating whether the employee is an off-payroll worker.
    /// </summary>
    bool IsOffPayrollWorker { get; init; }

    /// <summary>
    /// Gets the employee's workplace postcode, applicable when the employees' National Insurance
    /// category indicates they are working in a Freeport or Investment Zone.
    /// </summary>
    /// <remarks>Only applicable from April 2025.</remarks>
    string? EmployeeWorkplacePostcode { get; init; }

    /// <summary>
    /// Gets the default pension contributions to apply in each pay period, unless overridden by employee
    /// or employer instruction for that pay period.
    /// </summary>
    IPensionContributionLevels DefaultPensionContributionLevels { get; }

    /// <summary>
    /// Gets the list of payrolled benefits that apply to this employment.
    /// </summary>
    ImmutableArray<IPayrolledBenefit> PayrolledBenefits { get; }

    /// <summary>
    /// Gets the list of recurring earnings elements for an employee.
    /// </summary>
    ImmutableArray<IRecurringEarnings> RecurringEarnings { get; }

    /// <summary>
    /// Gets the list of recurring deductions for an employee.
    /// </summary>
    ImmutableArray<IRecurringDeduction> RecurringDeductions { get; }

    /// <summary>
    /// Gets the key figures from the employee's payroll history for the tax year to date.
    /// </summary>
    ref IEmployeePayrollHistoryYtd PayrollHistoryYtd { get; }

    /// <summary>
    /// Updates the payroll history for this employee with the supplied pay run information.
    /// </summary>
    /// <param name="payRunInput">Employee pay run input entry.</param>
    /// <param name="payrunResult">Results of a set of payroll calculations for the employee.</param>
    [Obsolete("Use UpdatePayrollHistory(IEmployeePayRunInputs, IEmployeePayRunOutputs) instead. This method will be removed in a future version.", false)]
    void UpdatePayrollHistory(in IEmployeePayRunInputEntry payRunInput, in IEmployeePayRunResult payrunResult);

    /// <summary>
    /// Updates the payroll history for this employee with the supplied pay run information.
    /// </summary>
    /// <param name="payDate">Pay date for the pay run.</param>
    /// <param name="employeePayRunInputs">Employee pay run inputs.</param>
    /// <param name="employeePayRunOutputs">Employee pay run outputs.</param>
    /// /// <typeparam name="TIdentifier">Identifier type for payrolls, pay runs, etc.</typeparam>
    void UpdatePayrollHistory<TIdentifier>(
        in PayDate payDate,
        in IEmployeePayRunInputs<TIdentifier> employeePayRunInputs, in IEmployeePayRunOutputs<TIdentifier> employeePayRunOutputs)
        where TIdentifier : notnull;
}