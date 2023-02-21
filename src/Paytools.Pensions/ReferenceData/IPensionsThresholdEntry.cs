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

namespace Paytools.Pensions.ReferenceData;

/// <summary>
/// Interface for types that provide access to a given set of pensions threshold values.
/// </summary>
public interface IPensionsThresholdEntry
{
    /// <summary>
    /// Gets the per week value of the threshold.
    /// </summary>
    decimal ThresholdValuePerWeek { get; }

    /// <summary>
    /// Gets the per 2-week value of the threshold.
    /// </summary>
    decimal ThresholdValuePerTwoWeeks { get; }

    /// <summary>
    /// Gets the per 4-week value of the threshold.
    /// </summary>
    decimal ThresholdValuePerFourWeeks { get; }

    /// <summary>
    /// Gets the per month value of the threshold.
    /// </summary>
    decimal ThresholdValuePerMonth { get; }

    /// <summary>
    /// Gets the per quarter value of the threshold.
    /// </summary>
    decimal ThresholdValuePerQuarter { get; }

    /// <summary>
    /// Gets the per half-year value of the threshold.
    /// </summary>
    decimal ThresholdValuePerHalfYear { get; }

    /// <summary>
    /// Gets the per annum value of the threshold.
    /// </summary>
    decimal ThresholdValuePerYear { get; }
}