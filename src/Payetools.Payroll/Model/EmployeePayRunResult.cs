// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.IncomeTax.Model;
using Payetools.NationalInsurance.Model;
using Payetools.Pensions.Model;
using Payetools.StudentLoans.Model;

namespace Payetools.Payroll.Model;

/// <summary>
/// Represents a payrun entry for one employee for a specific payrun.
/// </summary>
public record EmployeePayRunResult : IEmployeePayRunResult
{
    private ITaxCalculationResult _taxCalculationResult;
    private INiCalculationResult _niCalculationResult;
    private IStudentLoanCalculationResult _studentLoanCalculationResult;
    private IPensionContributionCalculationResult _pensionContributionCalculationResult;
    private IEmployeePayrollHistoryYtd _employeePayrollHistoryYtd;

    /// <summary>
    /// Gets information about this payrun.
    /// </summary>
    public ref IPayRunDetails PayRunDetails { get => throw new NotImplementedException(); }

    /// <summary>
    /// Gets the employee's employment details used in calculating this pay run result.  The PayrollId property of
    /// this field can be used as a hand to get access to the employee.
    /// </summary>
    public IEmployment Employment { get; }

    /// <summary>
    /// Gets a value indicating whether this employee is being recorded as left employment in this payrun.  Note that
    /// the employee's leaving date may be before the start of the pay period for this payrun.
    /// </summary>
    public bool IsLeaverInThisPayRun { get; }

    /// <summary>
    /// Gets a value indicating whether an ex-employee is being paid after the leaving date has been reported to
    /// HMRC in a previous submission.
    /// </summary>
    public bool IsPaymentAfterLeaving { get; }

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
    /// Initialises a new instance of <see cref="EmployeePayRunResult"/>.
    /// </summary>
    /// <param name="employment">Employee employment details.  Includes PayrollId as a handle to get access to the employee.</param>
    /// <param name="isLeaverInThisPayRun">True if the employee is being marked as left in this payrun.</param>
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
    public EmployeePayRunResult(
        IEmployment employment,
        bool isLeaverInThisPayRun,
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
        Employment = employment;
        IsLeaverInThisPayRun = isLeaverInThisPayRun;
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