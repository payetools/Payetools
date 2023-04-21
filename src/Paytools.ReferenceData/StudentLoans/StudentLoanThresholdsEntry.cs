// Copyright (c) 2023 Paytools Foundation.
//
// Paytools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Paytools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

using System.Text.Json.Serialization;

namespace Paytools.ReferenceData.StudentLoans;

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
