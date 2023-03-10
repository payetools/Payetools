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

namespace Paytools.NationalInsurance.ReferenceData;

/// <summary>
/// Interface that represents a set of NI thresholds that have been adjusted to a proportion of the tax year.
/// </summary>
public interface INiPeriodThresholdSet : INiThresholdSet
{
    /// <summary>
    /// Gets the modified threshold for the period (as distinct from the value returned by <see cref="GetThreshold1"/>)
    /// where rounding is applied based on whether the pay frequency is weekly or monthly, or otherwise.  As detailed in
    /// HMRC's NI calculation documentation as 'p1'.
    /// </summary>
    /// <param name="thresholdType">Applicable threshold (e.g., LEL, UEL, PT).</param>
    /// <returns>Pro-rata threshold value applicable to the period and threshold type.</returns>
    decimal GetThreshold1(NiThresholdType thresholdType);
}
