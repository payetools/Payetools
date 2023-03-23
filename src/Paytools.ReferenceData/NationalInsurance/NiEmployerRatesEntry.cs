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

using Paytools.Common.Model;
using System.Collections.Immutable;

namespace Paytools.ReferenceData.NationalInsurance;

/// <summary>
/// Record that represents the employee NI rates to be applied at various earnings thresholds.
/// </summary>
public record NiEmployerRatesEntry
{
    /// <summary>
    /// Gets the list of applicable NI categories for this rates entry.
    /// </summary>
    public ImmutableList<NiCategory> NiCategories { get; init; } = default!;

    /// <summary>
    /// Gets the employer rate applicable for earnings between the Lower Earnings
    /// Limit and the Secondary Threshold.
    /// </summary>
    public decimal ForEarningsAtOrAboveLELUpToAndIncludingST { get; init; }

    /// <summary>
    /// Gets the employer rate applicable for earnings between the Secondary Threshold
    /// and the Freeport Upper Secondary Threshold.
    /// </summary>
    public decimal ForEarningsAboveSTUpToAndIncludingFUST { get; init; }

    /// <summary>
    /// Gets the employer rate applicable for earnings between the Freeport Upper
    /// Secondary Threshold and the Upper Earnings Limit or any applicable Upper
    /// Secondary Threshold.
    /// </summary>
    public decimal ForEarningsAboveFUSTUpToAndIncludingUELOrUST { get; init; }

    /// <summary>
    /// Gets the employer rate applicable for earnings above the Upper Earnings Limit.
    /// </summary>
    public decimal ForEarningsAboveUELOrUST { get; init; }
}
