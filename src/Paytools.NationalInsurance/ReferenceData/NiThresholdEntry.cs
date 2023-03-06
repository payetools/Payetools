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

namespace Paytools.NationalInsurance.ReferenceData;

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