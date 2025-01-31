// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;

namespace Payetools.Testing.Data.EndToEnd;

public interface IExpectedOutputTestDataEntry
{
    string DefinitionVersion { get; }
    string TestIdentifier { get; }
    TaxYearEnding TaxYearEnding { get; }
    string TestReference { get; }
    decimal StatutoryMaternityPay { get; }
    decimal StatutoryPaternityPay { get; }
    decimal StatutoryAdoptionPay { get; }
    decimal SharedParentalPay { get; }
    decimal StatutoryParentalBereavementPay { get; }
    decimal GrossPay { get; }
    decimal TaxablePay { get; }
    decimal NicablePay { get; }
    decimal TaxPaid { get; }
    decimal EmployeeNiContribution { get; }
    decimal EmployerNiContribution { get; }
    decimal StudentLoanRepayments { get; }
    decimal GraduateLoanRepayments { get; }
    decimal PayrolledBenefits { get; }
    decimal TaxUnpaidDueToRegulatoryLimit { get; }
    decimal EmployerPensionContribution { get; }
    bool PensionUnderNpa { get; }
    decimal EmployeePensionContribution { get; }
    decimal OtherDeductions { get; }
    decimal NetPay { get; }
    decimal StatutoryMaternityPayYtd { get; }
    decimal StatutoryPaternityPayYtd { get; }
    decimal StatutoryAdoptionPayYtd { get; }
    decimal SharedParentalPayYtd { get; }
    decimal StatutoryParentalBereavementPayYtd { get; }
    decimal GrossPayYtd { get; }
    decimal TaxablePayYtd { get; }
    decimal NicablePayYtd { get; }
    decimal TaxPaidYtd { get; }
    decimal StudentLoanRepaymentsYtd { get; }
    decimal GraduateLoanRepaymentsYtd { get; }
    decimal PayrolledBenefitsYtd { get; }
    decimal TaxUnpaidDueToRegulatoryLimitYtd { get; }
    decimal EmployerPensionContributionYtd { get; }
    decimal EmployeePensionContributionsUnderNpaYtd { get; }
    decimal EmployeePensionContributionsUnderRasYtd { get; }
}
