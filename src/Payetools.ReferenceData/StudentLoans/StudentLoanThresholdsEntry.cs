// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using System.Text.Json.Serialization;

namespace Payetools.ReferenceData.StudentLoans;

/// <summary>
/// Represents a set of student loan thresholds, expressed per week, per month and per year.
/// </summary>
public readonly struct StudentLoanThresholdsEntry
{
    /// <summary>
    /// Gets the per week threshold for student or post-grad loan deductions.
    /// </summary>
    [JsonPropertyName("perWeek")]
    public decimal ThresholdValuePerWeek { get; init; }

    /// <summary>
    /// Gets the per week threshold for student or post-grad loan deductions.
    /// </summary>
    [JsonPropertyName("perMonth")]
    public decimal ThresholdValuePerMonth { get; init; }

    /// <summary>
    /// Gets the per week threshold for student or post-grad loan deductions.
    /// </summary>
    [JsonPropertyName("perYear")]
    public decimal ThresholdValuePerYear { get; init; }
}
