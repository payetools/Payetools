// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;
using Payetools.Pensions.ReferenceData;
using System.Text.Json.Serialization;

namespace Payetools.ReferenceData.Pensions;

/// <summary>
/// Represents a given set of pensions threshold values.
/// </summary>
public readonly struct PensionsThresholdEntry : IPensionsThresholdEntry
{
    /// <summary>
    /// Gets the per week value of the threshold.
    /// </summary>
    [JsonPropertyName("perWeek")]
    public decimal ThresholdValuePerWeek { get; init; }

    /// <summary>
    /// Gets the per 2-week value of the threshold.
    /// </summary>
    [JsonPropertyName("perTwoWeeks")]
    public decimal ThresholdValuePerTwoWeeks { get; init; }

    /// <summary>
    /// Gets the per 4-week value of the threshold.
    /// </summary>
    [JsonPropertyName("perFourWeeks")]
    public decimal ThresholdValuePerFourWeeks { get; init; }

    /// <summary>
    /// Gets the per month value of the threshold.
    /// </summary>
    [JsonPropertyName("perMonth")]
    public decimal ThresholdValuePerMonth { get; init; }

    /// <summary>
    /// Gets the per quarter value of the threshold.
    /// </summary>
    [JsonPropertyName("perQuarter")]
    public decimal ThresholdValuePerQuarter { get; init; }

    /// <summary>
    /// Gets the per half-year value of the threshold.
    /// </summary>
    [JsonPropertyName("perHalfYear")]
    public decimal ThresholdValuePerHalfYear { get; init; }

    /// <summary>
    /// Gets the per annum value of the threshold.
    /// </summary>
    [JsonPropertyName("perYear")]
    public decimal ThresholdValuePerYear { get; init; }

    /// <summary>
    /// Gets the applicable threshold value for the supplied pay frequency.
    /// </summary>
    /// <param name="payFrequency">Applicable pay frequency.</param>
    /// <returns>Applicable threshold value for the supplied pay frequency.</returns>
    public decimal GetThresholdForPayFrequency(PayFrequency payFrequency) =>
        payFrequency switch
        {
            PayFrequency.Weekly => ThresholdValuePerWeek,
            PayFrequency.Fortnightly=> ThresholdValuePerTwoWeeks,
            PayFrequency.FourWeekly=> ThresholdValuePerFourWeeks,
            PayFrequency.Monthly => ThresholdValuePerMonth,
            PayFrequency.Quarterly => ThresholdValuePerQuarter,
            PayFrequency.BiAnnually => ThresholdValuePerHalfYear,
            PayFrequency.Annually => ThresholdValuePerYear,
            _ => throw new ArgumentException("Invalid pay frequency supplied", nameof(payFrequency))
        };
}