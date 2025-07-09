// Copyright (c) 2023-2025, Payetools Ltd.
//
// Payetools Ltd licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;

namespace Payetools.Testing.Data.EndToEnd;

public interface IPreviousYtdTestDataEntry
{
    string DefinitionVersion { get; }
    string TestIdentifier { get; }
    TaxYearEnding TaxYearEnding { get; }
    string TestReference { get; }
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
    decimal TaxUnpaidDueToRegulatoryLimit { get; }
    decimal EmployerPensionContributionsYtd { get; }
    decimal EmployeePensionContributionsUnderNpaYtd { get; }
    decimal EmployeePensionContributionsUnderRasYtd { get; }
}
