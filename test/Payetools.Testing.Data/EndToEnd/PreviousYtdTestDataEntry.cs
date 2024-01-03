// Copyright (c) 2023-2024, Payetools Foundation.
//
// Paytools Foundation licenses this file to you under one of the following licenses:
//
//   * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//   * Paytools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

using Payetools.Common.Model;

namespace Payetools.Testing.Data.EndToEnd;

public class PreviousYtdTestDataEntry : IPreviousYtdTestDataEntry
{
    public string DefinitionVersion { get; set; } = string.Empty;
    public string TestIdentifier { get; set; } = string.Empty;
    public TaxYearEnding TaxYearEnding { get; set; }
    public string TestReference { get; set; } = string.Empty;
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
    public decimal TaxUnpaidDueToRegulatoryLimit { get; set; }
    public decimal EmployerPensionContributionsYtd { get; set; }
    public decimal EmployeePensionContributionsUnderNpaYtd { get; set; }
    public decimal EmployeePensionContributionsUnderRasYtd { get; set; }
}
