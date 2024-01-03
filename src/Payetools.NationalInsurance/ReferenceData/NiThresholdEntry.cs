// Copyright (c) 2023-2024, Payetools Foundation.
//
// Paytools Foundation licenses this file to you under one of the following licenses:
//
//   * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//   * Paytools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

using Payetools.NationalInsurance.Model;

namespace Payetools.NationalInsurance.ReferenceData;

/// <summary>
/// Record that represents a given National Insurance threshold across various pay frequencies.  Note
/// that HMRC guidance is that only the annual threshold should be used for calculations; weekly and
/// monthly thresholds are given for information only.
/// </summary>
public record NiThresholdEntry : INiThresholdEntry
{
    /// <summary>
    /// Gets the type of threshold this instance pertains to.
    /// </summary>
    public NiThresholdType ThresholdType { get; init; }

    /// <summary>
    /// Gets the per annum value of the threshold.
    /// </summary>
    public decimal ThresholdValuePerYear { get; init; }

    /// <summary>
    /// Gets the string representation of this <see cref="NiThresholdEntry"/> for debugging purposes.
    /// </summary>
    /// <returns>String representation of this instance.</returns>
    public override string ToString()
    {
        return $"{{ {ThresholdType}: {ThresholdValuePerYear} p.a. }}";
    }
}