// Copyright (c) 2023 Paytools Foundation
//
// Licensed under the Apache License, Version 2.0 (the "License")~
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

using Paytools.NationalInsurance;
using Paytools.NationalInsurance.ReferenceData;

namespace Paytools.ReferenceData.NationalInsurance;

public record NiCategoryRateSet
{
    private readonly Dictionary<NiCategory, INiCategoryRatesEntry> _categories;

    public NiCategoryRateSet()
    {
        _categories = new();
    }

    public INiCategoryRatesEntry GetRatesForCategory(NiCategory category)
    {
        return _categories[category];
    }

    public void Add(NiCategory category, NiCategoryRatesEntry rates)
    {
        _categories.TryAdd(category, rates);
    }

    public Dictionary<NiCategory, INiCategoryRatesEntry> GetRates() => _categories;
}
