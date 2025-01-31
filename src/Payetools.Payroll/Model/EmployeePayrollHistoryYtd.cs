// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;
using Payetools.NationalInsurance.Model;
using Payetools.Pensions.Model;

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
    /// Initialises a new empty instance of <see cref="EmployeePayrollHistoryYtd"/>.
    /// </summary>
    public EmployeePayrollHistoryYtd()
    {
        EarningsHistoryYtd = new EarningsHistoryYtd();
        DeductionsHistoryYtd = new DeductionsHistoryYtd();
        EmployeeNiHistoryEntries = new NiYtdHistory();
    }

    private EmployeePayrollHistoryYtd(IEarningsHistoryYtd earningsHistoryYtd, IDeductionsHistoryYtd deductionsHistoryYtd)
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
    public IEmployeePayrollHistoryYtd Add(IEmployeePayRunInputEntry payRunInput, IEmployeePayRunResult payrunResult)
    {
        var hasPension = payrunResult.PensionContributionCalculationResult != null;

        IEmployeePayrollHistoryYtd newHistory = new EmployeePayrollHistoryYtd(EarningsHistoryYtd.Apply(payRunInput.Earnings), DeductionsHistoryYtd.Apply(payRunInput.Deductions))
        {
            StatutoryMaternityPayYtd = StatutoryMaternityPayYtd +
                payRunInput.Earnings
                    .Where(e => e.EarningsDetails.PaymentType == PaymentType.StatutoryMaternityPay)
                    .Select(e => e.TotalEarnings)
                    .Sum(),

            StatutoryPaternityPayYtd = StatutoryPaternityPayYtd +
                payRunInput.Earnings
                    .Where(e => e.EarningsDetails.PaymentType == PaymentType.StatutoryPaternityPay)
                    .Select(e => e.TotalEarnings)
                    .Sum(),

            StatutoryAdoptionPayYtd = StatutoryAdoptionPayYtd +
                payRunInput.Earnings
                    .Where(e => e.EarningsDetails.PaymentType == PaymentType.StatutoryAdoptionPay)
                    .Select(e => e.TotalEarnings)
                    .Sum(),

            StatutorySharedParentalPayYtd = StatutorySharedParentalPayYtd +
                payRunInput.Earnings
                    .Where(e => e.EarningsDetails.PaymentType == PaymentType.StatutorySharedParentalPay)
                    .Select(e => e.TotalEarnings)
                    .Sum(),

            StatutoryParentalBereavementPayYtd = StatutoryParentalBereavementPayYtd +
                payRunInput.Earnings
                    .Where(e => e.EarningsDetails.PaymentType == PaymentType.StatutoryParentalBereavementPay)
                    .Select(e => e.TotalEarnings)
                    .Sum(),

            StatutoryNeonatalCarePayYtd = StatutoryNeonatalCarePayYtd +
                payRunInput.Earnings
                    .Where(e => e.EarningsDetails.PaymentType == PaymentType.StatutoryNeonatalCarePay)
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
    public static bool IsReclaimableStatutoryPayment(PaymentType paymentType) =>
        paymentType == PaymentType.StatutoryMaternityPay ||
        paymentType == PaymentType.StatutoryAdoptionPay ||
        paymentType == PaymentType.StatutoryPaternityPay ||
        paymentType == PaymentType.StatutorySharedParentalPay ||
        paymentType == PaymentType.StatutoryParentalBereavementPay ||
        paymentType == PaymentType.StatutoryNeonatalCarePay;

    private static bool PensionIsUnderNpa(IPensionContributionCalculationResult? pensionCalculationResult) =>
        pensionCalculationResult?.TaxTreatment == PensionTaxTreatment.NetPayArrangement;
}