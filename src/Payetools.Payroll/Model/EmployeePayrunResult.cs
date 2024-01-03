// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Payetools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

using Payetools.Employment.Model;
using Payetools.IncomeTax.Model;
using Payetools.NationalInsurance.Model;
using Payetools.Pensions.Model;
using Payetools.StudentLoans.Model;

namespace Payetools.Payroll.Model;

/// <summary>
/// Represents a payrun entry for one employee for a specific payrun.
/// </summary>
public record EmployeePayrunResult : IEmployeePayrunResult
{
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
    public IEmployee Employee { get; }

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
    /// Gets the employee's working gross pay, the figure used as the starting point for calculating take-home
    /// pay.
    /// </summary>
    public decimal WorkingGrossPay { get; }

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
    /// <param name="workingGrossPay">Total gross pay less any deductions that reduce taxable/Nicable pay
    /// and should be deducted before calculating net pay.</param>
    /// <param name="taxablePay">Pay subject to income tax.</param>
    /// <param name="nicablePay">Pay subject to National Insurance.</param>
    /// <param name="employeePayrollHistoryYtd">Historical set of information for an employee's payroll for the
    /// current tax year, not including the effect of this payrun.</param>
    public EmployeePayrunResult(
        IEmployee employee,
        bool isLeaverInThisPayrun,
        ref ITaxCalculationResult taxCalculationResult,
        ref INiCalculationResult niCalculationResult,
        ref IStudentLoanCalculationResult studentLoanCalculationResult,
        ref IPensionContributionCalculationResult pensionContributionCalculation,
        decimal totalGrossPay,
        decimal workingGrossPay,
        decimal taxablePay,
        decimal nicablePay,
        ref IEmployeePayrollHistoryYtd employeePayrollHistoryYtd)
    {
        Employee = employee;
        IsLeaverInThisPayrun = isLeaverInThisPayrun;
        _taxCalculationResult = taxCalculationResult;
        _niCalculationResult = niCalculationResult;
        _studentLoanCalculationResult = studentLoanCalculationResult;
        _pensionContributionCalculationResult = pensionContributionCalculation;
        TotalGrossPay = totalGrossPay;
        WorkingGrossPay = workingGrossPay;
        TaxablePay = taxablePay;
        NicablePay = nicablePay;
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