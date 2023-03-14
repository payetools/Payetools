// Copyright (c) 2023 Paytools Foundation.
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
using Paytools.IncomeTax.Model;
using Paytools.NationalInsurance.Model;
using Paytools.Pensions.Model;
using System.Collections.Immutable;

namespace Paytools.Employment.Model;

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
