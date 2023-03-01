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
using Paytools.IncomeTax;
using Paytools.NationalInsurance;
using Paytools.Pensions;
using Paytools.StudentLoans;

namespace Paytools.Payroll.Model;

/// <summary>
/// Represents a payrun entry for one employee for a specific payrun.
/// </summary>
public record EmployeePayrunResult : IEmployeePayrunResult
{
    private IEmployee _employee;
    private ITaxCalculationResult _taxCalculationResult;
    private INiCalculationResult _niCalculationResult;
    private IStudentLoanCalculationResult _studentLoanCalculationResult;
    private IPensionContributionCalculationResult _pensionContributionCalculationResult;
    private IEmployeePayrollHistoryYtd _employeePayrollHistoryYtd;

    /// <summary>
    /// Gets information about this payrun.
    /// </summary>
    public ref IPayrunInfo PayrunInfo { get => throw new NotImplementedException(); }

    /// <summary>
    /// Gets the employee's details.
    /// </summary>
    public ref IEmployee Employee => ref _employee;

    /// <summary>
    /// Gets a value indicating whether this employee is being recorded as left employment in this payrun.  Note that
    /// the employee's leaving date may be before the start of the pay period for this payrun.
    /// </summary>
    public bool IsLeaverInThisPayrun { get; }

    /// <summary>
    /// Gets the results of this employee's income tax calculation for this payrun.
    /// </summary>
    public ref ITaxCalculationResult TaxCalculationResult => ref _taxCalculationResult;

    /// <summary>
    /// Gets the results of this employee's National Insurance calculation for this payrun.
    /// </summary>
    public ref INiCalculationResult NiCalculationResult => ref _niCalculationResult;

    /// <summary>
    /// Gets the results of this employee's student loan calculation for this payrun, if applicable;
    /// null otherwise.
    /// </summary>
    public ref IStudentLoanCalculationResult StudentLoanCalculationResult => ref _studentLoanCalculationResult;

    /// <summary>
    /// Gets the results of this employee's pension calculation for this payrun, if applicable.;
    /// null otherwise.
    /// </summary>
    public ref IPensionContributionCalculationResult PensionContributionCalculationResult => ref _pensionContributionCalculationResult;

    /// <summary>
    /// Gets the historical set of information for an employee's payroll for the current tax year,
    /// not including the effect of this payrun.
    /// </summary>
    public ref IEmployeePayrollHistoryYtd EmployeePayrollHistoryYtd => ref _employeePayrollHistoryYtd;

    /// <summary>
    /// Gets the employee's total gross pay.
    /// </summary>
    public decimal TotalGrossPay { get; }

    /// <summary>
    /// Gets the employee's final net pay.
    /// </summary>
    public decimal NetPay { get; }

    /// <summary>
    /// Gets the employee's total pay that is subject to National Insurance.
    /// </summary>
    public decimal NicablePay { get; }

    /// <summary>
    /// Gets the employee's total taxable pay, before the application of any tax-free pay.
    /// </summary>
    public decimal TaxablePay { get; }

    /// <summary>
    /// Initialises a new instance of <see cref="EmployeePayrunResult"/>.
    /// </summary>
    /// <param name="employee">Employee details.</param>
    /// <param name="isLeaverInThisPayrun">True if the employee is being marked as left in this payrun.</param>
    /// <param name="taxCalculationResult">Result of income tax calculation.</param>
    /// <param name="niCalculationResult">Result of National Insurance calculation.</param>
    /// <param name="studentLoanCalculationResult">Optional result of student loan calculation.  Null if the
    /// employee does not have any outstanding student or post-graduate loans.</param>
    /// <param name="pensionContributionCalculation">Optional result of pension calculation.  Null if the
    /// employee is not a member of one of the company's schemes.</param>
    /// <param name="totalGrossPay">Total gross pay.</param>
    /// <param name="employeePayrollHistoryYtd">Historical set of information for an employee's payroll for the
    /// current tax year, not including the effect of this payrun.</param>
    public EmployeePayrunResult(
        ref IEmployee employee,
        bool isLeaverInThisPayrun,
        ref ITaxCalculationResult taxCalculationResult,
        ref INiCalculationResult niCalculationResult,
        ref IStudentLoanCalculationResult studentLoanCalculationResult,
        ref IPensionContributionCalculationResult pensionContributionCalculation,
        decimal totalGrossPay,
        ref IEmployeePayrollHistoryYtd employeePayrollHistoryYtd)
    {
        _employee = employee;
        IsLeaverInThisPayrun = isLeaverInThisPayrun;
        _taxCalculationResult = taxCalculationResult;
        _niCalculationResult = niCalculationResult;
        _studentLoanCalculationResult = studentLoanCalculationResult;
        _pensionContributionCalculationResult = pensionContributionCalculation;
        TotalGrossPay = totalGrossPay;
        NetPay = CalculateNetPay(totalGrossPay, taxCalculationResult.FinalTaxDue, niCalculationResult.EmployeeContribution,
            GetEmployeePensionDeduction(pensionContributionCalculation), studentLoanCalculationResult.TotalDeduction);
        _employeePayrollHistoryYtd = employeePayrollHistoryYtd;
    }

    private static decimal CalculateNetPay(decimal totalGrossPay, decimal incomeTax, decimal nationalInsurance, decimal employeePension, decimal? studentLoan) =>
        totalGrossPay - incomeTax - nationalInsurance - employeePension - (studentLoan ?? 0.0m);

    private static decimal GetEmployeePensionDeduction(IPensionContributionCalculationResult pensionContributionCalculation)
    {
        return pensionContributionCalculation.SalaryExchangeApplied ?
            pensionContributionCalculation.SalaryExchangedAmount ?? 0.0m :
            pensionContributionCalculation.CalculatedEmployeeContributionAmount;
    }
}