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

using Paytools.NationalInsurance.ReferenceData;
using System.Text;

namespace Paytools.NationalInsurance;

/// <summary>
/// Represents a National Insurance calculation result.
/// </summary>
public readonly struct NiCalculationResult : INiCalculationResult
{
    private static readonly NiCalculationResult _noRecordingRequiredResult = new NiCalculationResult() { NoRecordingRequiredIndicator = true };

    /// <summary>
    /// Gets the rates used for this calculation.
    /// </summary>
    public INiCategoryRatesEntry RatesUsed { get; }

    /// <summary>
    /// Gets the set of thresholds used for this calculation.  These thresholds are adjusted to match the
    /// length of the pay period.
    /// </summary>
    public INiThresholdSet ThresholdsUsed { get; }

    /// <summary>
    /// Gets the breakdown of earnings across each of the different National Insurance thresholds.
    /// </summary>
    public NiEarningsBreakdown EarningsBreakdown { get; }

    /// <summary>
    /// Gets the total employee contribution due as a result of this calculation.
    /// </summary>
    public decimal EmployeeContribution { get; }

    /// <summary>
    /// Gets the total employer contribution due as a result of this calculation.
    /// </summary>
    public decimal EmployerContribution { get; }

    /// <summary>
    /// Gets the total contribution due (employee + employer) as a result of this calculation.
    /// </summary>
    public decimal TotalContribution { get; }

    /// <summary>
    /// Gets a value indicating whether the results of this calculation need to be reported to HMRC.
    /// </summary>
    public bool NoRecordingRequiredIndicator { get; init; }

    /// <summary>
    /// Gets a static value representing an empty result with the NoRecordingRequiredIndicator set to true.
    /// </summary>
    public static NiCalculationResult NoRecordingRequired => _noRecordingRequiredResult;

    /// <summary>
    /// Initialises a new instance of <see cref="NiCalculationResult"/> with the supplied values.
    /// </summary>
    /// <param name="ratesUsed">Rates used for this calculation.</param>
    /// <param name="thresholdsUsed">Thresholds used for this calculation.</param>
    /// <param name="earningsBreakdown">Breakdown of earnings across each of the different National Insurance thresholds.</param>
    /// <param name="employeeContribution">Total employee contribution due as a result of this calculation.</param>
    /// <param name="employerContribution">Total employer contribution due as a result of this calculation.</param>
    /// <param name="totalContribution">Total contribution due (employee + employer) as a result of this calculation.</param>
    public NiCalculationResult(
        INiCategoryRatesEntry ratesUsed,
        INiThresholdSet thresholdsUsed,
        NiEarningsBreakdown earningsBreakdown,
        decimal employeeContribution,
        decimal employerContribution,
        decimal? totalContribution = null)
    {
        RatesUsed = ratesUsed;
        ThresholdsUsed = thresholdsUsed;
        EarningsBreakdown = earningsBreakdown;
        EmployeeContribution = employeeContribution;
        EmployerContribution = employerContribution;
        TotalContribution = totalContribution.HasValue ? (decimal)totalContribution : employeeContribution + employerContribution;

        NoRecordingRequiredIndicator = false;
    }

    /// <summary>
    /// Gets the string representation of this calculation result.
    /// </summary>
    /// <returns>String representation of this calculation result.</returns>
    public override string ToString()
    {
        if (NoRecordingRequiredIndicator)
            return "{{ NoRecordingRequiredIndicator: true }}";

        var sb = new StringBuilder();

        sb.Append("{{ ");
        sb.Append($"Rates: {RatesUsed.ToString()}, ");
        sb.Append($"Thresholds: {ThresholdsUsed.ToString()}, ");
        sb.Append($"Up And including to LEL: {EarningsBreakdown.EarningsUpToAndIncludingLEL}, ");
        sb.Append($"LEL to PT: {EarningsBreakdown.EarningsAboveLELUpToAndIncludingST + EarningsBreakdown.EarningsAboveSTUpToAndIncludingPT}, ");
        sb.Append($"PT to UEL: {EarningsBreakdown.EarningsAbovePTUpToAndIncludingFUST + EarningsBreakdown.EarningsAboveFUSTUpToAndIncludingUEL}, ");
        sb.Append($"ST to UEL: {EarningsBreakdown.EarningsAboveSTUpToAndIncludingUEL}, ");
        sb.Append($"above UEL: {EarningsBreakdown.EarningsAboveUEL}; ");
        sb.Append($"Contributions: Employee {EmployeeContribution}, Employer {EmployerContribution}, Total {TotalContribution}");
        sb.Append(" }}");

        return sb.ToString();
    }
}