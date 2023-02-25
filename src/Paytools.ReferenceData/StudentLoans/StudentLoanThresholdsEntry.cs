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
