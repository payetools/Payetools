// Copyright (c) 2022-2023 Paytools Ltd
//
// Licensed under the Apache License, Version 2.0 (the "License")~
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

using System.Text;

namespace Paytools.NationalInsurance;

public readonly struct NiCalculationResult : INiCalculationResult
{
    private bool _noRecordingRequired { get; init; }

    private static readonly NiCalculationResult _noRecordingRequiredResult = new NiCalculationResult() { _noRecordingRequired = true };
    
    public NiEarningsBreakdown EarningsBreakdown { get; init; }
    public decimal EmployeeContribution { get; init; }
    public decimal EmployerContribution { get; init; }
    public decimal TotalContribution { get; init; }
    public bool NoRecordingRequiredIndicator => _noRecordingRequired;

    public static NiCalculationResult NoRecordingRequired => _noRecordingRequiredResult;

    public NiCalculationResult(NiEarningsBreakdown earningsBreakdown, 
        decimal employeeContribution, 
        decimal employerContribution,
        decimal? totalContribution = null)
    {
        EarningsBreakdown = earningsBreakdown;
        EmployeeContribution = employeeContribution;
        EmployerContribution = employerContribution;
        TotalContribution = totalContribution.HasValue ? (decimal)totalContribution : employeeContribution + employerContribution;

        _noRecordingRequired = false;
    }

    public override string ToString()
    {
        var sb = new StringBuilder();

        sb.Append($"Up And including to LEL: {EarningsBreakdown.EarningsUpToAndIncludingLEL}, ");
        sb.Append($"LEL to PT: {EarningsBreakdown.EarningsAboveLELUpToAndIncludingST + EarningsBreakdown.EarningsAboveSTUpToAndIncludingPT}, ");
        sb.Append($"PT to UEL: {EarningsBreakdown.EarningsAbovePTUpToAndIncludingFUST + EarningsBreakdown.EarningsAboveFUSTUpToAndIncludingUEL}, ");
        sb.Append($"ST to UEL: {EarningsBreakdown.EarningsAboveSTUpToAndIncludingUEL}, ");
        sb.AppendLine($"above UEL: {EarningsBreakdown.EarningsAboveUEL}");
        sb.AppendLine($"Contributions: Employee {EmployeeContribution}, Employer {EmployerContribution}, Total {TotalContribution}");

        return sb.ToString();
    }
}