// Copyright (c) 2023 Paytools Foundation.
//
// Licensed under the Apache License, Version 2.0 (the "License");
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

using Paytools.IncomeTax.Model;
using Paytools.NationalInsurance.Model;
using Paytools.Common.Model;

namespace Paytools.Testing.Data.EndToEnd;

public class StaticInputTestDataEntry : IStaticInputTestDataEntry
{
    public string DefinitionVersion { get; set; } = string.Empty;
    public string TestIdentifier { get; set; } = string.Empty;
    public TaxYearEnding TaxYearEnding { get; set; }
    public string TestReference { get; set; } = string.Empty;
    public string EmployeeFirstName { get; set; } = string.Empty;
    public string EmployeeLastName { get; set; } = string.Empty;
    public TaxCode TaxCode { get; set; }
    public NiCategory NiCategory { get; set; }
    public string? StudentLoanPlan { get; set; }
    public bool GraduateLoan { get; set; }
    public bool IsInPension { get; set; }
    public bool UsesSalaryExchange { get; set; }
    public decimal? EmployeePercentage { get; set; }
    public decimal? EmployerPercentage { get; set; }
}
