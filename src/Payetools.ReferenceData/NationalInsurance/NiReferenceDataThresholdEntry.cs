// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.NationalInsurance.Model;
using Payetools.NationalInsurance.ReferenceData;
using System.Text.Json.Serialization;

namespace Payetools.ReferenceData.NationalInsurance;

/// <summary>
/// Record that represents a given National Insurance threshold across various pay frequencies.  Note
/// that HMRC guidance is that only the annual threshold should be used for calculations; weekly and
/// monthly thresholds are given for information only.
/// </summary>
public class NiReferenceDataThresholdEntry : INiThresholdEntry
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