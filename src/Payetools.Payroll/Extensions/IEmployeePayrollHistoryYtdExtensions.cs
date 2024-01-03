// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under one of the following licenses:
//
//   * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//   * Payetools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

using Payetools.Common.Model;
using Payetools.Payroll.Model;

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
    public static IEmployeePayrollHistoryYtd Add(this IEmployeePayrollHistoryYtd value, IEmployeePayrunResult payrunResult)
    {
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
            StudentLoanRepaymentsYtd = value.StudentLoanRepaymentsYtd + payrunResult.StudentLoanCalculationResult.StudentLoanDeduction,
            GraduateLoanRepaymentsYtd = value.GraduateLoanRepaymentsYtd + payrunResult.StudentLoanCalculationResult.PostGraduateLoanDeduction,

            // PayrolledBenefitsYtd = value.PayrolledBenefitsYtd + payrunResult.PayrolledBenefits,

            EmployeePensionContributionsUnderNpaYtd = value.EmployeePensionContributionsUnderNpaYtd +
                (payrunResult.PensionContributionCalculationResult.TaxTreatment == PensionTaxTreatment.NetPayArrangement ?
                payrunResult.PensionContributionCalculationResult.CalculatedEmployeeContributionAmount : 0.0m),
            EmployeePensionContributionsUnderRasYtd = value.EmployeePensionContributionsUnderRasYtd +
                (payrunResult.PensionContributionCalculationResult.TaxTreatment == PensionTaxTreatment.ReliefAtSource ?
                payrunResult.PensionContributionCalculationResult.CalculatedEmployeeContributionAmount : 0.0m),

            EmployerPensionContributionsYtd = value.EmployerPensionContributionsYtd + payrunResult.PensionContributionCalculationResult.CalculatedEmployerContributionAmount,

            TaxUnpaidDueToRegulatoryLimit = value.TaxUnpaidDueToRegulatoryLimit + payrunResult.TaxCalculationResult.TaxUnpaidDueToRegulatoryLimit,

            // EarningsHistoryYtd +=
            // IDeductionHistoryYtd DeductionHistoryYtd +=
        };
    }
}