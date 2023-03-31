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

namespace Paytools.Testing.Data.EndToEnd;

public class NiYtdHistoryTestDataEntry : INiYtdHistoryTestDataEntry
{
    public string DefinitionVersion { get; set; } = string.Empty;
    public string TestIdentifier { get; set; } = string.Empty;
    public TaxYearEnding TaxYearEnding { get; set; }
    public string TestReference { get; set; } = string.Empty;
    public NiCategory NiCategoryPertaining { get; set; }
    public decimal GrossNicableEarnings { get; set; }
    public decimal EmployeeContribution { get; set; }
    public decimal EmployerContribution { get; set; }
    public decimal TotalContribution { get; set; }
    public decimal EarningsUpToAndIncludingLEL { get; set; }
    public decimal EarningsAboveLELUpToAndIncludingST { get; set; }
    public decimal EarningsAboveSTUpToAndIncludingPT { get; set; }
    public decimal EarningsAbovePTUpToAndIncludingFUST { get; set; }
    public decimal EarningsAboveFUSTUpToAndIncludingUEL { get; set; }
    public decimal EarningsAboveUEL { get; set; }
    public decimal EarningsAboveSTUpToAndIncludingUEL { get; set; }
}
