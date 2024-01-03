// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Payetools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

using Payetools.Common.Model;
using Payetools.NationalInsurance.Model;
using Payetools.Pensions.Model;
using System.Collections.Immutable;

namespace Payetools.Employment.Model;

/// <summary>
/// Interface that represents an employee's employment for payroll purposes.
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
    /// Gets the employee's current student loan status.
    /// </summary>
    StudentLoanStatus? StudentLoanStatus { get; }

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
    /// Gets the default pension contributions to apply in each pay period, unless overridden by employee
    /// or employer instruction for that pay period.
    /// </summary>
    IPensionContributionLevels DefaultPensionContributionLevels { get; }

    /// <summary>
    /// Gets the list of payrolled benefits that apply to this employment.
    /// </summary>
    ImmutableList<IPayrolledBenefit> PayrolledBenefits { get; }

    /// <summary>
    /// Gets the list of recurring earnings elements for an employee.
    /// </summary>
    ImmutableList<IRecurringEarnings> RecurringEarnings { get; }

    /// <summary>
    /// Gets the list of recurring deductions for an employee.
    /// </summary>
    ImmutableList<IRecurringDeduction> RecurringDeductions { get; }

    /// <summary>
    /// Gets the key figures from the employee's payroll history for the tax year to date.
    /// </summary>
    ref IEmployeePayrollHistoryYtd PayrollHistoryYtd { get; }
}
