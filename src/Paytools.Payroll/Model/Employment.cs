﻿// Copyright (c) 2023 Paytools Foundation.
//
// Licensed under the Apache License, Version 2.0 (the "License") ~
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using Paytools.Common.Model;
using Paytools.Employment;
using Paytools.Employment.Model;
using Paytools.IncomeTax;
using Paytools.NationalInsurance;
using Paytools.Pensions.Model;
using System.Collections.Immutable;

namespace Paytools.Payroll.Model;

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
    public StudentLoanStatus? StudentLoanStatus { get; init; }

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
    public ImmutableList<IPayrolledBenefit> PayrolledBenefits { get; init; } = default!;

    /// <summary>
    /// Gets the list of recurring earnings elements for an employee.
    /// </summary>
    public ImmutableList<IRecurringEarnings> RecurringEarnings { get; init; } = default!;

    /// <summary>
    /// Gets the list of recurring deductions for an employee.
    /// </summary>
    public ImmutableList<IRecurringDeduction> RecurringDeductions { get; init; } = default!;

    /// <summary>
    /// Gets the key figures from the employee's payroll history for the tax year to date.
    /// </summary>
    public ref IEmployeePayrollHistoryYtd PayrollHistoryYtd => ref _payrollHistoryYtd;

    /// <summary>
    /// Initialises a new instance of <see cref="Employment"/>.
    /// </summary>
    /// <param name="payrollHistoryYtd">Employee's year-to-date payroll history.</param>
    public Employment(ref IEmployeePayrollHistoryYtd payrollHistoryYtd)
    {
        _payrollHistoryYtd = payrollHistoryYtd;
    }
}