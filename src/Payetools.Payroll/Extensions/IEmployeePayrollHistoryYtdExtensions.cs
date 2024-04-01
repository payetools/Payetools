// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;
using Payetools.Payroll.Model;
using Payetools.Pensions.Model;
using System.Runtime.CompilerServices;

namespace Payetools.Payroll.Extensions;

/// <summary>
/// Extension methods for <see cref="IEmployeePayrollHistoryYtd"/>.
/// </summary>
public static class IEmployeePayrollHistoryYtdExtensions
{
    /// <summary>
    /// Adds the results of the payrun provided to the current instance and returns a new instance of <see cref="IEmployeePayrollHistoryYtd"/>.
    /// </summary>
    /// <param name="value">Instance of <see cref="IEmployeePayrollHistoryYtd"/> to be updated.</param>
    /// <param name="payrunResult">Results of a set of payroll calculations for a given employee.</param>
    /// <returns>New instance of <see cref="IEmployeePayrollHistoryYtd"/> with the calculation results applied.</returns>
    public static IEmployeePayrollHistoryYtd Add(this IEmployeePayrollHistoryYtd value, IEmployeePayRunResult payrunResult)
    {
        var hasPension = payrunResult.PensionContributionCalculationResult != null;

        return new EmployeePayrollHistoryYtd()
        {
            // StatutoryMaternityPayYtd +=
            // StatutoryPaternityPayYtd +=
            // StatutoryAdoptionPayYtd += q
            // SharedParentalPayYtd +=
            // StatutoryParentalBereavementPayYtd +=

            EmployeeNiHistoryEntries = value.EmployeeNiHistoryEntries.Add(payrunResult.NiCalculationResult),
            GrossPayYtd = value.GrossPayYtd + payrunResult.TotalGrossPay,
            TaxablePayYtd = value.TaxablePayYtd + payrunResult.TaxablePay,
            NicablePayYtd = value.NicablePayYtd + payrunResult.NicablePay,
            TaxPaidYtd = value.TaxPaidYtd + payrunResult.TaxCalculationResult.FinalTaxDue,
            StudentLoanRepaymentsYtd = value.StudentLoanRepaymentsYtd + payrunResult.StudentLoanCalculationResult?.StudentLoanDeduction ?? 0.0m,
            PostgraduateLoanRepaymentsYtd = value.PostgraduateLoanRepaymentsYtd + payrunResult.StudentLoanCalculationResult?.PostgraduateLoanDeduction ?? 0.0m,

            // PayrolledBenefitsYtd = value.PayrolledBenefitsYtd + payrunResult.PayrolledBenefits,

            EmployeePensionContributionsUnderNpaYtd = value.EmployeePensionContributionsUnderNpaYtd +
                (hasPension && PensionIsUnderNpa(payrunResult.PensionContributionCalculationResult) ?
                    payrunResult.PensionContributionCalculationResult!.CalculatedEmployeeContributionAmount : 0.0m),

            EmployeePensionContributionsUnderRasYtd = value.EmployeePensionContributionsUnderRasYtd +
                (hasPension && !PensionIsUnderNpa(payrunResult.PensionContributionCalculationResult) ?
                    payrunResult.PensionContributionCalculationResult!.CalculatedEmployeeContributionAmount : 0.0m),

            EmployerPensionContributionsYtd = value.EmployerPensionContributionsYtd +
            payrunResult.PensionContributionCalculationResult?.CalculatedEmployerContributionAmount ?? 0.0m,

            TaxUnpaidDueToRegulatoryLimit = value.TaxUnpaidDueToRegulatoryLimit + payrunResult.TaxCalculationResult.TaxUnpaidDueToRegulatoryLimit,

            // EarningsHistoryYtd +=
            // IDeductionHistoryYtd DeductionHistoryYtd +=
        };
    }

    private static bool PensionIsUnderNpa(IPensionContributionCalculationResult? pensionCalculationResult) =>
        pensionCalculationResult?.TaxTreatment == PensionTaxTreatment.NetPayArrangement;
}