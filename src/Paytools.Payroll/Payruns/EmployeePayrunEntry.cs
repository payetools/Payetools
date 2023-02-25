// Copyright (c) 2023 Paytools Foundation.  All rights reserved.
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

using Paytools.Employment.Model;
using Paytools.IncomeTax;
using Paytools.NationalInsurance;
using Paytools.Pensions;
using Paytools.StudentLoans;

namespace Paytools.Payroll.Payruns;

/// <summary>
/// Represents a payrun entry for one employee for a specific payrun.
/// </summary>
public record EmployeePayrunEntry : IEmployeePayrunEntry
{
    /// <summary>
    /// Gets information about this payrun.
    /// </summary>
    public IPayrunInfo PayrunInfo { get => throw new NotImplementedException(); }

    /// <summary>
    /// Gets the employee's details.
    /// </summary>
    public IEmployee Employee { get; }

    /// <summary>
    /// Gets a value indicating whether this employee is being recorded as left employment in this payrun.  Note that
    /// the employee's leaving date may be before the start of the pay period for this payrun.
    /// </summary>
    public bool IsLeaverInThisPayrun { get;  }

    /// <summary>
    /// Gets the results of this employee's income tax calculation for this payrun.
    /// </summary>
    public ITaxCalculationResult TaxCalculationResult { get; }

    /// <summary>
    /// Gets the results of this employee's National Insurance calculation for this payrun.
    /// </summary>
    public INiCalculationResult NiCalculationResult { get; }

    /// <summary>
    /// Gets the results of this employee's student loan calculation for this payrun, if applicable;
    /// null otherwise.
    /// </summary>
    public IStudentLoanCalculationResult? StudentLoanCalculationResult { get; }

    /// <summary>
    /// Gets the results of this employee's pension calculation for this payrun, if applicable.;
    /// null otherwise.
    /// </summary>
    public IPensionContributionCalculationResult? PensionContributionCalculationResult { get; }

    /// <summary>
    /// Gets the historical set of information for an employee's payroll for the current tax year,
    /// not including the effect of this payrun.
    /// </summary>
    public IEmployeePayrollHistoryYtd EmployeePayrollHistoryYtd { get; }

    /// <summary>
    /// Initialises a new instance of <see cref="EmployeePayrunEntry"/>.
    /// </summary>
    /// <param name="employee">Employee details.</param>
    /// <param name="isLeaverInThisPayrun">True if the employee is being marked as left in this payrun.</param>
    /// <param name="taxCalculationResult">Result of income tax calculation.</param>
    /// <param name="niCalculationResult">Result of National Insurance calculation.</param>
    /// <param name="studentLoanCalculationResult">Optional result of student loan calculation.  Null if the
    /// employee does not have any outstanding student or post-graduate loans.</param>
    /// <param name="pensionContributionCalculation">Optional result of pension calculation.  Null if the
    /// employee is not a member of one of the company's schemes.</param>
    /// <param name="employeePayrollHistoryYtd">Historical set of information for an employee's payroll for the
    /// current tax year, not including the effect of this payrun.</param>
    public EmployeePayrunEntry(
        IEmployee employee,
        bool isLeaverInThisPayrun,
        ITaxCalculationResult taxCalculationResult,
        INiCalculationResult niCalculationResult,
        IStudentLoanCalculationResult? studentLoanCalculationResult,
        IPensionContributionCalculationResult? pensionContributionCalculation,
        EmployeePayrollHistoryYtd employeePayrollHistoryYtd)
    {
        Employee = employee;
        IsLeaverInThisPayrun = isLeaverInThisPayrun;
        TaxCalculationResult = taxCalculationResult;
        NiCalculationResult = niCalculationResult;
        StudentLoanCalculationResult = studentLoanCalculationResult;
        PensionContributionCalculationResult = pensionContributionCalculation;
        EmployeePayrollHistoryYtd = employeePayrollHistoryYtd;
    }
}