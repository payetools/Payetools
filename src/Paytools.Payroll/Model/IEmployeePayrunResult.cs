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
