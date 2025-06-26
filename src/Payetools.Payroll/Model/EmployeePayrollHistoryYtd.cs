// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

#pragma warning disable SA1402 // File may only contain a single type

using Payetools.Common.Model;
using Payetools.NationalInsurance.Model;
using Payetools.Payroll.PayRuns;
using Payetools.Pensions.Model;
using System.Text.Json.Serialization;

namespace Payetools.Payroll.Model;

/// <summary>
/// Represents the historical set of information for an employee's payroll for the
/// current tax year.
/// </summary>
/// <remarks>Added Statutory Neonatal Care Pay applicable from April 2025.</remarks>
public class EmployeePayrollHistoryYtd : IEmployeePayrollHistoryYtd
{
    /// <summary>
    /// Gets any statutory maternity pay paid to date this tax year.
    /// </summary>
    public decimal StatutoryMaternityPayYtd { get; init; }

    /// <summary>
    /// Gets any statutory paternity pay paid to date this tax year.
    /// </summary>
    public decimal StatutoryPaternityPayYtd { get; init; }

    /// <summary>
    /// Gets any statutory adoption pay paid to date this tax year.
    /// </summary>
    public decimal StatutoryAdoptionPayYtd { get; init; }

    /// <summary>
    /// Gets any statutory shared parental pay paid to date this tax year.
    /// </summary>
    public decimal StatutorySharedParentalPayYtd { get; init; }

    /// <summary>
    /// Gets any statutory parental bereavement pay paid to date this tax year.
    /// </summary>
    public decimal StatutoryParentalBereavementPayYtd { get; init; }

    /// <summary>
    /// Gets any statutory neonatal care pay paid to date this tax year.
    /// </summary>
    public decimal StatutoryNeonatalCarePayYtd { get; init; }

    /// <summary>
    /// Gets any statutory sickness pay paid to date this tax year.
    /// </summary>
    public decimal StatutorySickPayYtd { get; init; }

    /// <summary>
    /// Gets the National Insurance payment history for the current tax year.  Employees may
    /// transition between NI categories during the tax year and each NI category's payment
    /// record must be retained.
    /// </summary>
    public NiYtdHistory EmployeeNiHistoryEntries { get; init; } = default!;

    /// <summary>
    /// Gets the gross pay paid to date this tax year.
    /// </summary>
    public decimal GrossPayYtd { get; init; }

    /// <summary>
    /// Gets the taxable pay paid to date this tax year.
    /// </summary>
    public decimal TaxablePayYtd { get; init; }

    /// <summary>
    /// Gets the NI-able pay paid to date this tax year.
    /// </summary>
    public decimal NicablePayYtd { get; init; }

    /// <summary>
    /// Gets the income tax paid to date this tax year.
    /// </summary>
    public decimal TaxPaidYtd { get; init; }

    /// <summary>
    /// Gets the student loan deductions made to date this tax year.
    /// </summary>
    public decimal StudentLoanRepaymentsYtd { get; init; }

    /// <summary>
    /// Gets the postgraduate loan deductions made to date this tax year.
    /// </summary>
    public decimal PostgraduateLoanRepaymentsYtd { get; init; }

    /// <summary>
    /// Gets the amount accrued against payrolled benefits to date this tax year.
    /// </summary>
    public decimal PayrolledBenefitsYtd { get; init; }

    /// <summary>
    /// Gets the total employee pension contributions made under a net pay arrangement to date this tax year.
    /// </summary>
    public decimal EmployeePensionContributionsUnderNpaYtd { get; init; }

    /// <summary>
    /// Gets the total employee pension contributions made under relief at source to date this tax year.
    /// </summary>
    public decimal EmployeePensionContributionsUnderRasYtd { get; init; }

    /// <summary>
    /// Gets the total employer pension contributions made to date this tax year.
    /// </summary>
    public decimal EmployerPensionContributionsYtd { get; init; }

    /// <summary>
    /// Gets the tax that it has not been possible to collect so far this tax year due to the
    /// regulatory limit on income tax deductions.
    /// </summary>
    public decimal TaxUnpaidDueToRegulatoryLimit { get; init; }

    /// <summary>
    /// Gets the employee's earnings history for the tax year to date.
    /// </summary>
    public IEarningsHistoryYtd EarningsHistoryYtd { get; init; } = default!;

    /// <summary>
    /// Gets the employee's deduction history for the tax year to date.
    /// </summary>
    public IDeductionsHistoryYtd DeductionsHistoryYtd { get; init; } = default!;

    /// <summary>
    /// Gets the National Insurance paid to date this tax year, by NI category.
    /// </summary>
    /// <remarks>Overlay property as planning to change the name of the original property in due course.</remarks>
    [JsonIgnore]
    public INiYtdHistory NiHistory { get => EmployeeNiHistoryEntries; init => EmployeeNiHistoryEntries = (NiYtdHistory)value; }

    /// <summary>
    /// Initialises a new empty instance of <see cref="EmployeePayrollHistoryYtd"/>.
    /// </summary>
    public EmployeePayrollHistoryYtd()
    {
        EarningsHistoryYtd = new EarningsHistoryYtd();
        DeductionsHistoryYtd = new DeductionsHistoryYtd();
        EmployeeNiHistoryEntries = new NiYtdHistory();
    }

    /// <summary>
    /// Initialises a new instance of <see cref="EmployeePayrollHistoryYtd"/> with the
    /// supplied earnings and deductions history.
    /// </summary>
    /// <param name="earningsHistoryYtd">Year-to-date earnings history.</param>
    /// <param name="deductionsHistoryYtd">Year-to-date deductions.</param>
    protected EmployeePayrollHistoryYtd(IEarningsHistoryYtd earningsHistoryYtd, IDeductionsHistoryYtd deductionsHistoryYtd)
    {
        EarningsHistoryYtd = earningsHistoryYtd;
        DeductionsHistoryYtd = deductionsHistoryYtd;
    }

    /// <summary>
    /// Adds the results of the payrun provided to the current instance and returns a new instance of
    /// <see cref="IEmployeePayrollHistoryYtd"/>.</summary>
    /// <param name="payRunInput">Employee pay run input entry.</param>
    /// <param name="payrunResult">Results of a set of payroll calculations for a given employee.</param>
    /// <returns>New instance of <see cref="IEmployeePayrollHistoryYtd"/> with the calculation results applied.</returns>
    [Obsolete("Use Add(IEmployeePayRunInputs, IEmployeePayRunOutputs) instead. Scheduled for removal in v3.0.0.", false)]
    public IEmployeePayrollHistoryYtd Add(IEmployeePayRunInputEntry payRunInput, IEmployeePayRunResult payrunResult)
    {
        var hasPension = payrunResult.PensionContributionCalculationResult != null;

        IEmployeePayrollHistoryYtd newHistory = new EmployeePayrollHistoryYtd(EarningsHistoryYtd.Apply(payRunInput.Earnings), DeductionsHistoryYtd.Apply(payRunInput.Deductions))
        {
            StatutoryMaternityPayYtd = StatutoryMaternityPayYtd +
                payRunInput.Earnings
                    .Where(e => e.EarningsDetails.PaymentType == EarningsType.StatutoryMaternityPay)
                    .Select(e => e.TotalEarnings)
                    .Sum(),

            StatutoryPaternityPayYtd = StatutoryPaternityPayYtd +
                payRunInput.Earnings
                    .Where(e => e.EarningsDetails.PaymentType == EarningsType.StatutoryPaternityPay)
                    .Select(e => e.TotalEarnings)
                    .Sum(),

            StatutoryAdoptionPayYtd = StatutoryAdoptionPayYtd +
                payRunInput.Earnings
                    .Where(e => e.EarningsDetails.PaymentType == EarningsType.StatutoryAdoptionPay)
                    .Select(e => e.TotalEarnings)
                    .Sum(),

            StatutorySharedParentalPayYtd = StatutorySharedParentalPayYtd +
                payRunInput.Earnings
                    .Where(e => e.EarningsDetails.PaymentType == EarningsType.StatutorySharedParentalPay)
                    .Select(e => e.TotalEarnings)
                    .Sum(),

            StatutoryParentalBereavementPayYtd = StatutoryParentalBereavementPayYtd +
                payRunInput.Earnings
                    .Where(e => e.EarningsDetails.PaymentType == EarningsType.StatutoryParentalBereavementPay)
                    .Select(e => e.TotalEarnings)
                    .Sum(),

            StatutoryNeonatalCarePayYtd = StatutoryNeonatalCarePayYtd +
                payRunInput.Earnings
                    .Where(e => e.EarningsDetails.PaymentType == EarningsType.StatutoryNeonatalCarePay)
                    .Select(e => e.TotalEarnings)
                    .Sum(),

            EmployeeNiHistoryEntries = EmployeeNiHistoryEntries.Add(payrunResult.NiCalculationResult),

            GrossPayYtd = GrossPayYtd + payrunResult.TotalGrossPay,
            TaxablePayYtd = TaxablePayYtd + payrunResult.TaxablePay,
            NicablePayYtd = NicablePayYtd + payrunResult.NicablePay,
            TaxPaidYtd = TaxPaidYtd + payrunResult.TaxCalculationResult.FinalTaxDue,
            StudentLoanRepaymentsYtd = StudentLoanRepaymentsYtd + payrunResult.StudentLoanCalculationResult?.StudentLoanDeduction ?? 0.0m,
            PostgraduateLoanRepaymentsYtd = PostgraduateLoanRepaymentsYtd + payrunResult.StudentLoanCalculationResult?.PostgraduateLoanDeduction ?? 0.0m,

            PayrolledBenefitsYtd = PayrolledBenefitsYtd + payRunInput.PayrolledBenefits.Select<IPayrolledBenefitForPeriod, decimal>(pb => pb.AmountForPeriod).Sum(),

            EmployeePensionContributionsUnderNpaYtd = EmployeePensionContributionsUnderNpaYtd +
                (hasPension && PensionIsUnderNpa(payrunResult.PensionContributionCalculationResult) ?
                    payrunResult.PensionContributionCalculationResult!.CalculatedEmployeeContributionAmount : 0.0m),

            EmployeePensionContributionsUnderRasYtd = EmployeePensionContributionsUnderRasYtd +
                (hasPension && !PensionIsUnderNpa(payrunResult.PensionContributionCalculationResult) ?
                    payrunResult.PensionContributionCalculationResult!.CalculatedEmployeeContributionAmount : 0.0m),

            EmployerPensionContributionsYtd = EmployerPensionContributionsYtd +
            payrunResult.PensionContributionCalculationResult?.CalculatedEmployerContributionAmount ?? 0.0m,

            TaxUnpaidDueToRegulatoryLimit = TaxUnpaidDueToRegulatoryLimit + payrunResult.TaxCalculationResult.TaxUnpaidDueToRegulatoryLimit
        };

        return newHistory;
    }

    /// <summary>
    /// Returns true if the supplied payment type is one that can be reclaimed from HMRC, as these are treated differently within this
    /// entity and the associated earnings history.
    /// </summary>
    /// <param name="paymentType">Payment type.</param>
    /// <returns>True if the supplied payment type is one that can be reclaimed from HMRC; otherwise false.</returns>
    public static bool IsReclaimableStatutoryPayment(EarningsType paymentType) =>
        paymentType == EarningsType.StatutoryMaternityPay ||
        paymentType == EarningsType.StatutoryAdoptionPay ||
        paymentType == EarningsType.StatutoryPaternityPay ||
        paymentType == EarningsType.StatutorySharedParentalPay ||
        paymentType == EarningsType.StatutoryParentalBereavementPay ||
        paymentType == EarningsType.StatutoryNeonatalCarePay;

    /// <summary>
    /// Gets whether the pension contribution calculation result is under a net pay arrangement (NPA).
    /// </summary>
    /// <param name="pensionCalculationResult">Pension calculation result.</param>
    /// <returns>true if the pension is under NPA; false otherwise.</returns>
    protected static bool PensionIsUnderNpa(IPensionContributionCalculationResult? pensionCalculationResult) =>
        pensionCalculationResult?.TaxTreatment == PensionTaxTreatment.NetPayArrangement;
}

/// <summary>
/// Represents the historical set of information for an employee's payroll for the current tax year.
/// <see cref="EmployeeId"/> is used to identify the employee.  Also adds the tax year ending value,
/// to identify which tax year this payroll history applies to.
/// </summary>
/// <typeparam name="TIdentifier">Type of the employee identifier.</typeparam>
/// <remarks>Use this type in preference to the non-generic type if it is necessary to identify
/// payroll history by employee.</remarks>
public class EmployeePayrollHistoryYtd<TIdentifier> : EmployeePayrollHistoryYtd, IEmployeePayrollHistoryYtd<TIdentifier>
    where TIdentifier : IEarningsHistoryYtd, IDeductionsHistoryYtd
{
    /// <summary>
    /// Gets the unique identifier for the employee that this payroll history applies to.
    /// </summary>
    public required TIdentifier EmployeeId { get; init; }

    /// <summary>
    /// Gets the tax year ending value for this payroll history.
    /// </summary>
    public TaxYearEnding TaxYearEnding { get; init; }

    /// <summary>
    /// Initializes a new instance of the <see cref="EmployeePayrollHistoryYtd{TIdentifier}"/> class.
    /// </summary>
    /// <param name="earningsHistoryYtd">Year-to-date earnings history.</param>
    /// <param name="deductionsHistoryYtd">Year-to-date deductions.</param>
    public EmployeePayrollHistoryYtd(IEarningsHistoryYtd earningsHistoryYtd, IDeductionsHistoryYtd deductionsHistoryYtd)
        : base(earningsHistoryYtd, deductionsHistoryYtd)
    {
    }

    /// <summary>
    /// Adds the results of the pay run provided to the current instance and returns a new instance of
    /// <see cref="IEmployeePayrollHistoryYtd"/>.
    /// </summary>
    /// <param name="payDate">Pay date for the pay run.</param>
    /// <param name="employeePayRunInputs">Employee pay run inputs.</param>
    /// <param name="employeePayRunOutputs">Employee pay run outputs.</param>
    /// <returns>New instance of <see cref="IEmployeePayrollHistoryYtd"/> with the calculation results
    /// applied.</returns>
    public IEmployeePayrollHistoryYtd<TIdentifier> Add(
        PayDate payDate,
        IEmployeePayRunInputs<TIdentifier> employeePayRunInputs,
        IEmployeePayRunOutputs<TIdentifier> employeePayRunOutputs)
    {
        var hasPension = employeePayRunOutputs.PensionContributionCalculationResult != null;

        IEmployeePayrollHistoryYtd<TIdentifier> newHistory = new EmployeePayrollHistoryYtd<TIdentifier>(
            EarningsHistoryYtd.Apply(employeePayRunInputs.Earnings),
            DeductionsHistoryYtd.Apply(employeePayRunInputs.Deductions))
        {
            EmployeeId = employeePayRunInputs.EmployeeId,

            TaxYearEnding = payDate.TaxYear.TaxYearEnding,

            StatutoryMaternityPayYtd = StatutoryMaternityPayYtd +
                employeePayRunInputs.Earnings
                    .Where(e => e.EarningsDetails.PaymentType == EarningsType.StatutoryMaternityPay)
                    .Select(e => e.TotalEarnings)
                    .Sum(),

            StatutoryPaternityPayYtd = StatutoryPaternityPayYtd +
                employeePayRunInputs.Earnings
                    .Where(e => e.EarningsDetails.PaymentType == EarningsType.StatutoryPaternityPay)
                    .Select(e => e.TotalEarnings)
                    .Sum(),

            StatutoryAdoptionPayYtd = StatutoryAdoptionPayYtd +
                employeePayRunInputs.Earnings
                    .Where(e => e.EarningsDetails.PaymentType == EarningsType.StatutoryAdoptionPay)
                    .Select(e => e.TotalEarnings)
                    .Sum(),

            StatutorySharedParentalPayYtd = StatutorySharedParentalPayYtd +
                employeePayRunInputs.Earnings
                    .Where(e => e.EarningsDetails.PaymentType == EarningsType.StatutorySharedParentalPay)
                    .Select(e => e.TotalEarnings)
                    .Sum(),

            StatutoryParentalBereavementPayYtd = StatutoryParentalBereavementPayYtd +
                employeePayRunInputs.Earnings
                    .Where(e => e.EarningsDetails.PaymentType == EarningsType.StatutoryParentalBereavementPay)
                    .Select(e => e.TotalEarnings)
                    .Sum(),

            StatutoryNeonatalCarePayYtd = StatutoryNeonatalCarePayYtd +
                employeePayRunInputs.Earnings
                    .Where(e => e.EarningsDetails.PaymentType == EarningsType.StatutoryNeonatalCarePay)
                    .Select(e => e.TotalEarnings)
                    .Sum(),

            EmployeeNiHistoryEntries = EmployeeNiHistoryEntries.Add(employeePayRunOutputs.NiCalculationResult),

            GrossPayYtd = GrossPayYtd + employeePayRunOutputs.TotalGrossPay,
            TaxablePayYtd = TaxablePayYtd + employeePayRunOutputs.TaxablePay,
            NicablePayYtd = NicablePayYtd + employeePayRunOutputs.NicablePay,
            TaxPaidYtd = TaxPaidYtd + employeePayRunOutputs.TaxCalculationResult.FinalTaxDue,
            StudentLoanRepaymentsYtd = StudentLoanRepaymentsYtd + employeePayRunOutputs.StudentLoanCalculationResult?.StudentLoanDeduction ?? 0.0m,
            PostgraduateLoanRepaymentsYtd = PostgraduateLoanRepaymentsYtd + employeePayRunOutputs.StudentLoanCalculationResult?.PostgraduateLoanDeduction ?? 0.0m,

            PayrolledBenefitsYtd = PayrolledBenefitsYtd + employeePayRunInputs.PayrolledBenefits.Select<IPayrolledBenefitForPeriod, decimal>(pb => pb.AmountForPeriod).Sum(),

            EmployeePensionContributionsUnderNpaYtd = EmployeePensionContributionsUnderNpaYtd +
                (hasPension && PensionIsUnderNpa(employeePayRunOutputs.PensionContributionCalculationResult) ?
                    employeePayRunOutputs.PensionContributionCalculationResult!.CalculatedEmployeeContributionAmount : 0.0m),

            EmployeePensionContributionsUnderRasYtd = EmployeePensionContributionsUnderRasYtd +
                (hasPension && !PensionIsUnderNpa(employeePayRunOutputs.PensionContributionCalculationResult) ?
                    employeePayRunOutputs.PensionContributionCalculationResult!.CalculatedEmployeeContributionAmount : 0.0m),

            EmployerPensionContributionsYtd = EmployerPensionContributionsYtd +
            employeePayRunOutputs.PensionContributionCalculationResult?.CalculatedEmployerContributionAmount ?? 0.0m,

            TaxUnpaidDueToRegulatoryLimit = TaxUnpaidDueToRegulatoryLimit + employeePayRunOutputs.TaxCalculationResult.TaxUnpaidDueToRegulatoryLimit
        };

        return newHistory;
    }
}