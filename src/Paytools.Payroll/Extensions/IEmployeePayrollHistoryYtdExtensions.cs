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
using Paytools.Payroll.Model;
using Paytools.Pensions.Model;

namespace Paytools.Payroll.Extensions;

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