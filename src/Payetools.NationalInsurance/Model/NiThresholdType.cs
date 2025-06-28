// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

#pragma warning disable SA1649 // File name should match first type name

namespace Payetools.NationalInsurance.Model;

/// <summary>
/// Enum enumerating the various National Insurance threshold levels.
/// </summary>
public enum NiThresholdType
{
    /// <summary>Lower Earnings Limit.</summary>
    LEL,

    /// <summary>Primary Threshold.</summary>
    PT,

    /// <summary>Secondary Threshold.</summary>
    ST,

    /// <summary>Freeport Upper Secondary Threshold.</summary>
    FUST,

    /// <summary>Upper Secondary Threshold.</summary>
    UST,

    /// <summary>Apprentice Upper Secondary Threshold.</summary>
    AUST,

    /// <summary>Veterans Upper Secondary Threshold.</summary>
    VUST,

    /// <summary>Upper Earnings Limit.</summary>
    UEL,

    /// <summary>Directors Primary Threshold (if applicable).</summary>
    DPT,

    /// <summary>Investment Zone Upper Secondary Threshold.</summary>
    IZUST
}

/// <summary>
/// Extension methods for instances of <see cref="NiThresholdType"/>.
/// </summary>
public static class NationalInsuranceThresholdExtensions
{
    /// <summary>
    /// Gets the zero-based index of the supplied <see cref="NiThresholdType"/>.  Used when retrieving thresholds from arrays or lists
    /// who elements are ordered the same as this enum.
    /// </summary>
    /// <param name="threshold"><see cref="NiThresholdType"/> value.</param>
    /// <returns>Zero-based index of this NiThresholdType.</returns>
    public static int GetIndex(this NiThresholdType threshold) =>
        (int)threshold;

    /// <summary>
    /// Gets the full name of the threshold as a string.
    /// </summary>
    /// <param name="thresholdType"><see cref="NiThresholdType"/> value.</param>
    /// <returns>Full name of the threshold as a string, e.g., "Lower Earnings Limit".</returns>
    /// <exception cref="ArgumentException">Thrown if the NiThresholdType value supplied is unrecognised.</exception>
    public static string GetFullName(this NiThresholdType thresholdType) =>
        thresholdType switch
        {
            NiThresholdType.LEL => "Lower Earnings Limit",
            NiThresholdType.PT => "Primary Threshold",
            NiThresholdType.ST => "Secondary Threshold",
            NiThresholdType.FUST => "Freeport Upper Secondary Threshold",
            NiThresholdType.UST => "Upper Secondary Threshold",
            NiThresholdType.AUST => "Apprentice Upper Secondary Threshold",
            NiThresholdType.VUST => "Veterans Upper Secondary Threshold",
            NiThresholdType.UEL => "Upper Earnings Limit",
            NiThresholdType.DPT => "Directors Primary Threshold",
            NiThresholdType.IZUST => "Investment Zone Upper Secondary Threshold",
            _ => throw new ArgumentException("Unrecognised NiThresholdType value", nameof(thresholdType))
        };
}