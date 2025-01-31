// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;

namespace Payetools.Testing.Data.NationalInsurance;

public interface IHmrcDirectorsNiTestDataEntry
{
    string DefinitionVersion { get; }
    string TestIdentifier { get; }
    TaxYearEnding TaxYearEnding { get; }
    string RelatesTo { get; }
    PayFrequency PayFrequency { get; }
    decimal GrossPay { get; }
    int Period { get; }
    NiCategory NiCategory { get; }
    decimal EmployeeNiContribution { get; }
    decimal EmployerNiContribution { get; }
    decimal TotalNiContribution { get; }
    decimal EarningsAtLEL_YTD { get; }
    decimal EarningsLELtoPT_YTD { get; }
    decimal EarningsPTtoUEL_YTD { get; }
    decimal TotalEmployerContributions_YTD { get; }
    decimal TotalEmployeeContributions_YTD { get; }
    string StatusMethod { get; }
    decimal GrossPayYtd { get; }
    decimal EmployeeNiContributionYtd { get; }
    decimal EmployerNiContributionYtd { get; }
    decimal? ProRataFactor { get; }
}
