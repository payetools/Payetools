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
