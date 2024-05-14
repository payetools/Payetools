// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;
using Payetools.NationalInsurance.ReferenceData;
using System.Text;

namespace Payetools.NationalInsurance.Model;

/// <summary>
/// Represents a National Insurance calculation result.
/// </summary>
public readonly struct NiCalculationResult : INiCalculationResult
{
    /// <summary>
    /// Gets the NI category used for this calculation.
    /// </summary>
    public NiCategory NiCategory { get; }

    /// <summary>
    /// Gets the gross pay for NI purposes ("Nicable pay") used in this calculation.
    /// </summary>
    public decimal NicablePay { get; } = default!;

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
    public decimal EmployeeContribution { get; } = default!;

    /// <summary>
    /// Gets the total employer contribution due as a result of this calculation.
    /// </summary>
    public decimal EmployerContribution { get; } = default!;

    /// <summary>
    /// Gets the total contribution due (employee + employer) as a result of this calculation.
    /// </summary>
    public decimal TotalContribution { get; } = default!;

    /// <summary>
    /// Gets a value indicating whether the results of this calculation need to be reported to HMRC.
    /// </summary>
    public bool NoRecordingRequired { get; init; }

    /// <summary>
    /// Gets the value of any Class 1A National Insurance contributions payable. Null if none.
    /// </summary>
    public decimal? Class1ANicsPayable { get; init; } = default;

    /// <summary>
    /// Initialises a new instance of <see cref="NiCalculationResult"/> with the supplied values.
    /// </summary>
    /// <param name="category">NI category used for this calculation.</param>
    /// <param name="nicablePay">Gross pay for NI purposes ("Nicable pay") used in this calculation.</param>
    /// <param name="ratesUsed">Rates used for this calculation.</param>
    /// <param name="thresholdsUsed">Thresholds used for this calculation.</param>
    /// <param name="earningsBreakdown">Breakdown of earnings across each of the different National Insurance thresholds.</param>
    /// <param name="employeeContribution">Total employee contribution due as a result of this calculation.</param>
    /// <param name="employerContribution">Total employer contribution due as a result of this calculation.</param>
    /// <param name="totalContribution">Total contribution due (employee + employer) as a result of this calculation.</param>
    /// <param name="class1ANicsPayable">Value of any Class 1A contributions due.</param>
    public NiCalculationResult(
        NiCategory category,
        decimal nicablePay,
        INiCategoryRatesEntry ratesUsed,
        INiThresholdSet thresholdsUsed,
        NiEarningsBreakdown earningsBreakdown,
        decimal employeeContribution,
        decimal employerContribution,
        decimal? totalContribution = null,
        decimal? class1ANicsPayable = null)
    {
        NiCategory = category;
        NicablePay = nicablePay;
        RatesUsed = ratesUsed;
        ThresholdsUsed = thresholdsUsed;
        EarningsBreakdown = earningsBreakdown;
        EmployeeContribution = employeeContribution;
        EmployerContribution = employerContribution;
        TotalContribution = totalContribution.HasValue ? (decimal)totalContribution : employeeContribution + employerContribution;
        Class1ANicsPayable = class1ANicsPayable;

        NoRecordingRequired = false;
    }

    /// <summary>
    /// Initialises a new instance of <see cref="NiCalculationResult"/> with zero values except
    /// the NI category. Used to indicate that no recording is required.
    /// </summary>
    /// <param name="category">NI category used for this calculation.</param>
    public NiCalculationResult(NiCategory category)
    {
        NiCategory = category;
        RatesUsed = default!;
        ThresholdsUsed = default!;
        EarningsBreakdown = default!;
        NoRecordingRequired = true;
    }

    /// <summary>
    /// Gets the string representation of this calculation result. Intended for debugging/logging purposes.
    /// </summary>
    /// <returns>String representation of this calculation result.</returns>
    public override string ToString()
    {
        if (NoRecordingRequired)
            return $"{{ NI category: {NiCategory}, NoRecordingRequiredIndicator: {NoRecordingRequired} }}";

        var sb = new StringBuilder();

        sb.Append("{{ ");
        sb.Append($"NI category: {NiCategory}, ");
        sb.Append($"Rates: {RatesUsed.ToString()}, ");
        sb.Append($"Thresholds: {ThresholdsUsed.ToString()}, ");
        sb.Append($"Up And including to LEL: {EarningsBreakdown.EarningsAtLEL}, ");
        sb.Append($"LEL to PT: {EarningsBreakdown.EarningsAboveLELUpToAndIncludingST + EarningsBreakdown.EarningsAboveSTUpToAndIncludingPT}, ");
        sb.Append($"PT to UEL: {EarningsBreakdown.EarningsAbovePTUpToAndIncludingFUST + EarningsBreakdown.EarningsAboveFUSTUpToAndIncludingUEL}, ");
        sb.Append($"ST to UEL: {EarningsBreakdown.EarningsAboveSTUpToAndIncludingUEL}, ");
        sb.Append($"above UEL: {EarningsBreakdown.EarningsAboveUEL}; ");
        sb.Append($"Contributions: Employee {EmployeeContribution}, Employer {EmployerContribution}, Total {TotalContribution}");
        sb.Append(" }}");

        return sb.ToString();
    }
}