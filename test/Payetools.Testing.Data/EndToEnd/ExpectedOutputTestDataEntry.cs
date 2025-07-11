// Copyright (c) 2023-2025, Payetools Ltd.
//
// Payetools Ltd licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;

namespace Payetools.Testing.Data.EndToEnd;

public class ExpectedOutputTestDataEntry : IExpectedOutputTestDataEntry
{
    public string DefinitionVersion { get; set; } = string.Empty;
    public string TestIdentifier { get; set; } = string.Empty;
    public TaxYearEnding TaxYearEnding { get; set; }
    public string TestReference { get; set; } = string.Empty;
    public decimal StatutoryMaternityPay { get; set; }
    public decimal StatutoryPaternityPay { get; set; }
    public decimal StatutoryAdoptionPay { get; set; }
    public decimal SharedParentalPay { get; set; }
    public decimal StatutoryParentalBereavementPay { get; set; }
    public decimal GrossPay { get; set; }
    public decimal TaxablePay { get; set; }
    public decimal NicablePay { get; set; }
    public decimal TaxPaid { get; set; }
    public decimal EmployeeNiContribution { get; set; }
    public decimal EmployerNiContribution { get; set; }
    public decimal StudentLoanRepayments { get; set; }
    public decimal GraduateLoanRepayments { get; set; }
    public decimal PayrolledBenefits { get; set; }
    public decimal TaxUnpaidDueToRegulatoryLimit { get; set; }
    public decimal EmployerPensionContribution { get; set; }
    public bool PensionUnderNpa { get; set; }
    public decimal EmployeePensionContribution { get; set; }
    public decimal OtherDeductions { get; set; }
    public decimal NetPay { get; set; }
    public decimal StatutoryMaternityPayYtd { get; set; }
    public decimal StatutoryPaternityPayYtd { get; set; }
    public decimal StatutoryAdoptionPayYtd { get; set; }
    public decimal SharedParentalPayYtd { get; set; }
    public decimal StatutoryParentalBereavementPayYtd { get; set; }
    public decimal GrossPayYtd { get; set; }
    public decimal TaxablePayYtd { get; set; }
    public decimal NicablePayYtd { get; set; }
    public decimal TaxPaidYtd { get; set; }
    public decimal StudentLoanRepaymentsYtd { get; set; }
    public decimal GraduateLoanRepaymentsYtd { get; set; }
    public decimal PayrolledBenefitsYtd { get; set; }
    public decimal TaxUnpaidDueToRegulatoryLimitYtd { get; set; }
    public decimal EmployerPensionContributionYtd { get; set; }
    public decimal EmployeePensionContributionsUnderNpaYtd { get; set; }
    public decimal EmployeePensionContributionsUnderRasYtd { get; set; }
}
