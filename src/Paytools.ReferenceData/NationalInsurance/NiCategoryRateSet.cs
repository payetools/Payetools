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

namespace Paytools.ReferenceData.NationalInsurance;

/// <summary>
/// Represents a set of National Insurance rates across all NI categories.  Each NI
/// category specifies its own set rate of rates across the various NI thresholds.
/// </summary>
public record NiCategoryRateSet
{
    private readonly Dictionary<NiCategory, INiCategoryRatesEntry> _categories;

    /// <summary>
    /// Initialises a new instance of <see cref="NiCategoryRateSet"/>.
    /// </summary>
    public NiCategoryRateSet()
    {
        _categories = new Dictionary<NiCategory, INiCategoryRatesEntry>();
    }

    /// <summary>
    /// Gets the set of NI rates applicable to the specified <see cref="NiCategory"/>.
    /// </summary>
    /// <param name="category">NI category to retrieve the applicable rates for.</param>
    /// <returns>Set of rates applicable to the specified NI category.</returns>
    public INiCategoryRatesEntry GetRatesForCategory(NiCategory category)
    {
        return _categories[category];
    }

    internal void Add(NiCategory category, NiCategoryRatesEntry rates)
    {
        _categories.TryAdd(category, rates);
    }
}
