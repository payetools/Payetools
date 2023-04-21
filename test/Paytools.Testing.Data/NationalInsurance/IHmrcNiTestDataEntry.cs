// Copyright (c) 2023 Paytools Foundation.
//
// Paytools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Paytools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

using Paytools.Common.Model;

namespace Paytools.Testing.Data.NationalInsurance;

public interface IHmrcNiTestDataEntry
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
}
