// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;

namespace Payetools.Testing.Data.NationalInsurance;

public class HmrcDirectorsNiTestDataEntry : IHmrcDirectorsNiTestDataEntry
{
    public string DefinitionVersion { get; set; } = string.Empty;
    public string TestIdentifier { get; set; } = string.Empty;
    public TaxYearEnding TaxYearEnding { get; set; }
    public string RelatesTo { get; set; } = string.Empty;
    public PayFrequency PayFrequency { get; set; }
    public decimal GrossPay { get; set; }
    public int Period { get; set; }
    public NiCategory NiCategory { get; set; }
    public decimal EmployeeNiContribution { get; set; }
    public decimal EmployerNiContribution { get; set; }
    public decimal TotalNiContribution { get; set; }
    public decimal EarningsAtLEL_YTD { get; set; }
    public decimal EarningsLELtoPT_YTD { get; set; }
    public decimal EarningsPTtoUEL_YTD { get; set; }
    public decimal TotalEmployerContributions_YTD { get; set; }
    public decimal TotalEmployeeContributions_YTD { get; set; }
    public string StatusMethod { get; set; } = string.Empty;
    public decimal GrossPayYtd { get; set; }
    public decimal EmployeeNiContributionYtd { get; set; }
    public decimal EmployerNiContributionYtd { get; set; }
    public decimal? ProRataFactor { get; set; }
}
