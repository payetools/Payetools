// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.AttachmentOrders.Model;
using Payetools.IncomeTax.Model;
using Payetools.NationalInsurance.Model;
using Payetools.Payroll.PayRuns;
using Payetools.Pensions.Model;
using Payetools.StudentLoans.Model;

namespace Payetools.Payroll.Model;

/// <summary>
/// Entiy that represents the outputs of an employee's pay run, including all the individual
/// calulation results and values that are relevant for the pay run.
/// </summary>
/// <typeparam name="TIdentifier">Identifier type for payrolls, pay runs, etc.</typeparam>
public class EmployeePayRunOutputs<TIdentifier> : IEmployeePayRunOutputs<TIdentifier>
{
    private ITaxCalculationResult _taxCalculationResult;
    private INiCalculationResult _niCalculationResult;
    private IStudentLoanCalculationResult? _studentLoanCalculationResult;
    private IPensionContributionCalculationResult? _pensionContributionCalculationResult;
    private IAttachmentOrdersCalculationResult? _attachmentOfEarningsCalculationResult;

    /// <summary>
    /// Initializes a new instance of the <see cref="EmployeePayRunOutputs{TIdentifier}"/> class with all calculation results and values.
    /// </summary>
    /// <param name="employeeId">The unique identifier for the employee.</param>
    /// <param name="taxCalculationResult">The income tax calculation result.</param>
    /// <param name="niCalculationResult">The National Insurance calculation result.</param>
    /// <param name="studentLoanCalculationResult">The student loan calculation result, or null if not applicable.</param>
    /// <param name="pensionContributionCalculationResult">The pension contribution calculation result, or null if not applicable.</param>
    /// <param name="attachmentOfEarningsCalculationResult">The attachment of earnings calculation result, or null if not applicable.</param>
    /// <param name="totalGrossPay">The total gross pay, excluding payrolled taxable benefits.</param>
    /// <param name="workingGrossPay">The total gross pay less any deductions that reduce taxable pay.</param>
    /// <param name="taxablePay">The total taxable pay before any tax-free pay is applied.</param>
    /// <param name="nicablePay">The total pay subject to National Insurance.</param>
    /// <param name="payrollBenefitsInPeriod">The total payrolled benefits in the period, or null if none.</param>
    public EmployeePayRunOutputs(
        TIdentifier employeeId,
        ref ITaxCalculationResult taxCalculationResult,
        ref INiCalculationResult niCalculationResult,
        ref IStudentLoanCalculationResult? studentLoanCalculationResult,
        ref IPensionContributionCalculationResult? pensionContributionCalculationResult,
        ref IAttachmentOrdersCalculationResult? attachmentOfEarningsCalculationResult,
        decimal totalGrossPay,
        decimal workingGrossPay,
        decimal taxablePay,
        decimal nicablePay,
        decimal? payrollBenefitsInPeriod)
    {
        _taxCalculationResult = taxCalculationResult;
        _niCalculationResult = niCalculationResult;
        _studentLoanCalculationResult = studentLoanCalculationResult;
        _pensionContributionCalculationResult = pensionContributionCalculationResult;
        _attachmentOfEarningsCalculationResult = attachmentOfEarningsCalculationResult;

        EmployeeId = employeeId ?? throw new ArgumentNullException(nameof(employeeId));
        TotalGrossPay = totalGrossPay;
        WorkingGrossPay = workingGrossPay;
        NicablePay = nicablePay;
        TaxablePay = taxablePay;
        NetPay = CalculateNetPay(
            totalGrossPay,
            taxCalculationResult.FinalTaxDue,
            niCalculationResult.EmployeeContribution,
            GetEmployeePensionDeduction(pensionContributionCalculationResult),
            studentLoanCalculationResult?.TotalDeduction,
            attachmentOfEarningsCalculationResult?.TotalDeduction);
        PayrollBenefitsInPeriod = payrollBenefitsInPeriod;
    }

    /// <summary>
    /// Gets the unique identifier for the employee.
    /// </summary>
    public TIdentifier EmployeeId { get; init; }

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
    public ref IStudentLoanCalculationResult? StudentLoanCalculationResult => ref _studentLoanCalculationResult;

    /// <summary>
    /// Gets the results of this employee's pension calculation for this payrun, if applicable;
    /// null otherwise.
    /// </summary>
    public ref IPensionContributionCalculationResult? PensionContributionCalculationResult => ref _pensionContributionCalculationResult;

    /// <summary>
    /// Gets the results of any attachment of earnings order calculation for this employee for this
    /// payrun, if applicable.
    /// </summary>
    public ref IAttachmentOrdersCalculationResult? AttachmentOfEarningsCalculationResult => ref _attachmentOfEarningsCalculationResult;

    /// <summary>
    /// Gets the employee's total gross pay, excluding payrolled taxable benefits.
    /// </summary>
    public decimal TotalGrossPay { get; }

    /// <summary>
    /// Gets the employee's working gross pay, excluding payrolled taxable benefits and less
    /// any deductions that reduce taxable pay.
    /// </summary>
    public decimal WorkingGrossPay { get; }

    /// <summary>
    /// Gets the employee's total pay that is subject to National Insurance.
    /// </summary>
    public decimal NicablePay { get; }

    /// <summary>
    /// Gets the employee's total taxable pay, before the application of any tax-free pay.
    /// </summary>
    public decimal TaxablePay { get; }

    /// <summary>
    /// Gets the employee's final net pay.
    /// </summary>
    public decimal NetPay { get; }

    /// <summary>
    /// Gets the total amount of payrolled benefits in the period, where applicable.  Null if no
    /// payrolled benefits have been applied.
    /// </summary>
    public decimal? PayrollBenefitsInPeriod { get; }

    private static decimal CalculateNetPay(
        decimal totalGrossPay,
        decimal incomeTax,
        decimal nationalInsurance,
        decimal employeePension,
        decimal? studentLoan,
        decimal? attachmentOfEarningsDeductions,
        decimal? otherDeductions = null) =>
        totalGrossPay -
            incomeTax -
            nationalInsurance -
            employeePension -
            (studentLoan ?? 0.0m) -
            (attachmentOfEarningsDeductions ?? 0.0m) -
            (otherDeductions ?? 0.0m);

    private static decimal GetEmployeePensionDeduction(IPensionContributionCalculationResult? pensionContributionCalculation) =>
        pensionContributionCalculation != null ?
                (pensionContributionCalculation.SalaryExchangeApplied ?
                pensionContributionCalculation.SalaryExchangedAmount ?? 0.0m :
                pensionContributionCalculation.CalculatedEmployeeContributionAmount) :
            0.0m;
}