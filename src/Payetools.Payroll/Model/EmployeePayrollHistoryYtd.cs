// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;
using Payetools.NationalInsurance.Model;

namespace Payetools.Payroll.Model;

/// <summary>
/// Represents the historical set of information for an employee's payroll for the
/// current tax year.
/// </summary>
public record EmployeePayrollHistoryYtd : IEmployeePayrollHistoryYtd
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
    public decimal SharedParentalPayYtd { get; init; }

    /// <summary>
    /// Gets any statutory parental bereavement pay paid to date this tax year.
    /// </summary>
    public decimal StatutoryParentalBereavementPayYtd { get; init; }

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
    /// Gets the graduate loan deductions made to date this tax year.
    /// </summary>
    public decimal GraduateLoanRepaymentsYtd { get; init; }

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
    public IDeductionHistoryYtd DeductionHistoryYtd { get; init; } = default!;

    /// <summary>
    /// Initialises a new empty instance of <see cref="EmployeePayrollHistoryYtd"/>.
    /// </summary>
    public EmployeePayrollHistoryYtd()
    {
        EarningsHistoryYtd = new EarningsHistoryYtd();
    }

    /// <summary>
    /// Initialises a new instance of <see cref="EmployeePayrollHistoryYtd"/> with the supplied payrun result for a
    /// given employee.  This constructor is intended for use to create the first history record from the first
    /// payrun of the tax year.
    /// </summary>
    /// <param name="initialResult">Payrun calculation result for the given employee.</param>
    public EmployeePayrollHistoryYtd(IEmployeePayrunResult initialResult)
    {
        // StatutoryMaternityPayYtd +=
        // StatutoryPaternityPayYtd +=
        // StatutoryAdoptionPayYtd += q
        // SharedParentalPayYtd +=
        // StatutoryParentalBereavementPayYtd +=

        EmployeeNiHistoryEntries = new NiYtdHistory(initialResult.NiCalculationResult);
        GrossPayYtd = initialResult.TotalGrossPay;
        TaxablePayYtd = initialResult.TaxablePay;
        NicablePayYtd = initialResult.NicablePay;
        TaxPaidYtd = initialResult.TaxCalculationResult.FinalTaxDue;
        StudentLoanRepaymentsYtd = initialResult.StudentLoanCalculationResult.StudentLoanDeduction;
        GraduateLoanRepaymentsYtd = initialResult.StudentLoanCalculationResult.PostGraduateLoanDeduction;

        // PayrolledBenefitsYtd = value.PayrolledBenefitsYtd + initialResult.PayrolledBenefits,

        EmployeePensionContributionsUnderNpaYtd = initialResult.PensionContributionCalculationResult.TaxTreatment == PensionTaxTreatment.NetPayArrangement ?
            initialResult.PensionContributionCalculationResult.CalculatedEmployeeContributionAmount : 0.0m;
        EmployeePensionContributionsUnderRasYtd = initialResult.PensionContributionCalculationResult.TaxTreatment == PensionTaxTreatment.ReliefAtSource ?
                initialResult.PensionContributionCalculationResult.CalculatedEmployeeContributionAmount : 0.0m;
        EmployerPensionContributionsYtd = initialResult.PensionContributionCalculationResult.CalculatedEmployerContributionAmount;

        TaxUnpaidDueToRegulatoryLimit = initialResult.TaxCalculationResult.TaxUnpaidDueToRegulatoryLimit;

        // EarningsHistoryYtd +=
        // IDeductionHistoryYtd DeductionHistoryYtd +=
    }
}