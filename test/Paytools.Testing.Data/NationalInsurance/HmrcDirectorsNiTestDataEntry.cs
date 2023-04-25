// Copyright (c) 2023 Paytools Foundation.
//
// Paytools Foundation licenses this file to you under one of the following licenses:
//
//   * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//   * Paytools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

using System;
using Paytools.Common.Model;

namespace Paytools.Testing.Data.NationalInsurance;

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
