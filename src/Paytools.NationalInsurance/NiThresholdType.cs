// Copyright (c) 2023 Paytools Foundation.  All rights reserved.
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

using System.Runtime.Intrinsics.Arm;

namespace Paytools.NationalInsurance;

/// <summary>
/// Enum enumerating the various National Insurance threshold levels.
/// </summary>
public enum NiThresholdType
{
    /// <summary>Lower Earnings Limit</summary>
    LEL,

    /// <summary>Primary Threshold</summary>
    PT,

    /// <summary>Secondary Threshold</summary>
    ST,

    /// <summary>Freeport Upper Secondary Threshold</summary>
    FUST,

    /// <summary>Upper Secondary Threshold</summary>
    UST,

    /// <summary>Apprentice Upper Secondary Threshold</summary>
    AUST,

    /// <summary>Veterans Upper Secondary Threshold</summary>
    VUST,

    /// <summary>Upper Earnings Limit</summary>
    UEL,

    /// <summary>Directors Primary Threshold (if applicable)</summary>
    DPT,

    /// <summary>Number of elements in enum</summary>
    Count = 9
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
            _ => throw new ArgumentException("Unrecognised NiThresholdType value", nameof(thresholdType))
        };
}