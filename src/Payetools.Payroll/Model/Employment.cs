// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;
using Payetools.NationalInsurance.Model;
using Payetools.Pensions.Model;
using System.Collections.Immutable;

namespace Payetools.Payroll.Model;

/// <summary>
/// Represents an employee's employment for payroll purposes.
/// </summary>
public record Employment : IEmployment
{
    private IEmployeePayrollHistoryYtd _payrollHistoryYtd;

    /// <summary>
    /// Gets the employee's official employment start date.
    /// </summary>
    public DateOnly EmploymentStartDate { get; init; }

    /// <summary>
    /// Gets the employee's official employment termination date, i.e., their last working
    /// day.  Null if the employee is still employed.
    /// </summary>
    public DateOnly? EmploymentEndDate { get; init; }

    /// <summary>
    /// Gets the employee's tax code.
    /// </summary>
    public TaxCode TaxCode { get; init; }

    /// <summary>
    /// Gets the normal hours worked by the employee in one of several bands established by HMRC.
    /// </summary>
    public NormalHoursWorkedBand NormalHoursWorkedBand { get; }

    /// <summary>
    /// Gets the employee's NI category.
    /// </summary>
    public NiCategory NiCategory { get; init; }

    /// <summary>
    /// Gets the employee's payroll ID, as reported to HMRC.  Sometimes known as "works number".
    /// </summary>
    public PayrollId PayrollId { get; init; } = default!;

    /// <summary>
    /// Gets a value indicating whether the employee is a company director.
    /// </summary>
    public bool IsDirector { get; init; }

    /// <summary>
    /// Gets the method for calculating National Insurance contributions.  Applicable only
    /// for directors; null otherwise.
    /// </summary>
    public DirectorsNiCalculationMethod? DirectorsNiCalculationMethod { get; init; }

    /// <summary>
    /// Gets the employee's current student loan status.
    /// </summary>
    public StudentLoanInfo? StudentLoanInfo { get; init; }

    /// <summary>
    /// Gets the employee's primary pay structure.
    /// </summary>
    public IEmployeePayStructure PrimaryPayStructure { get; init; } = default!;

    /// <summary>
    /// Gets the pension scheme that the employee is a member of.  Null if they are not a member of
    /// any scheme.
    /// </summary>
    public IPensionScheme? PensionScheme { get; init; }

    /// <summary>
    /// Gets the default pension contributions to apply in each pay period, unless overridden by employee
    /// or employer instruction for that pay period.
    /// </summary>
    public IPensionContributionLevels DefaultPensionContributionLevels { get; init; } = default!;

    /// <summary>
    /// Gets the list of payrolled benefits that apply to this employment.
    /// </summary>
    public ImmutableArray<IPayrolledBenefit> PayrolledBenefits { get; init; } = default!;

    /// <summary>
    /// Gets the list of recurring earnings elements for an employee.
    /// </summary>
    public ImmutableArray<IRecurringEarnings> RecurringEarnings { get; init; } = default!;

    /// <summary>
    /// Gets the list of recurring deductions for an employee.
    /// </summary>
    public ImmutableArray<IRecurringDeduction> RecurringDeductions { get; init; } = default!;

    /// <summary>
    /// Gets the key figures from the employee's payroll history for the tax year to date.
    /// </summary>
    public ref IEmployeePayrollHistoryYtd PayrollHistoryYtd => ref _payrollHistoryYtd;

    /// <summary>
    /// Initialises a new instance of <see cref="Employment"/>.
    /// </summary>
    /// <param name="payrollHistoryYtd">Employee's year-to-date payroll history.</param>
    public Employment(in IEmployeePayrollHistoryYtd payrollHistoryYtd)
    {
        _payrollHistoryYtd = payrollHistoryYtd;
    }
}