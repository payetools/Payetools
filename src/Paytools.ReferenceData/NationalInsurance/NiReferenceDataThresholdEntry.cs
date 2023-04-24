// Copyright (c) 2023 Paytools Foundation.
//
// Paytools Foundation licenses this file to you under one of the following licenses:
//
//   * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//   * Paytools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

using Paytools.NationalInsurance.Model;
using Paytools.NationalInsurance.ReferenceData;
using System.Text.Json.Serialization;

namespace Paytools.ReferenceData.NationalInsurance;

/// <summary>
/// Record that represents a given National Insurance threshold across various pay frequencies.  Note
/// that HMRC guidance is that only the annual threshold should be used for calculations; weekly and
/// monthly thresholds are given for information only.
/// </summary>
public record NiReferenceDataThresholdEntry : INiThresholdEntry
{
    /// <summary>
    /// Gets the applicable threshold's name.  This name is mapped to the relevant <see cref="NiThresholdType"/> as
    /// part of the deserialisation process.
    /// </summary>
    [JsonPropertyName("thresholdName")]
    public NiThresholdType ThresholdType { get; init; }

    /// <summary>
    /// Gets the per week value of the threshold.
    /// </summary>
    [JsonPropertyName("perWeek")]
    public decimal ThresholdValuePerWeek { get; init; }

    /// <summary>
    /// Gets the per month value of the threshold.
    /// </summary>
    [JsonPropertyName("perMonth")]
    public decimal ThresholdValuePerMonth { get; init; }

    /// <summary>
    /// Gets the per year value of the threshold.
    /// </summary>
    [JsonPropertyName("perYear")]
    public decimal ThresholdValuePerYear { get; init; }
}
