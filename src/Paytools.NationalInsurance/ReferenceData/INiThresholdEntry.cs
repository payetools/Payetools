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
/// Interface for types that provide access to a given NI threshold value.
/// </summary>
public interface INiThresholdEntry
{
    /// <summary>
    /// Gets the type of threshold this instance pertains to.
    /// </summary>
    NiThresholdType ThresholdType { get; }

    /// <summary>
    /// Gets the per annum value of the threshold.
    /// </summary>
    decimal ThresholdValuePerYear { get; }
}