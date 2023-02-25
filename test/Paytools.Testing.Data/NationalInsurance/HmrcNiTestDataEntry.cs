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

using Paytools.Common.Model;
using Paytools.NationalInsurance;

namespace Paytools.Testing.Data.NationalInsurance;

public class HmrcNiTestDataEntry : IHmrcNiTestDataEntry
{
    public string DefinitionVersion { get; set; } = string.Empty;
    public string TestIdentifier { get; set; } = string.Empty;
    public TaxYearEnding TaxYearEnding { get; set; }
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
}
