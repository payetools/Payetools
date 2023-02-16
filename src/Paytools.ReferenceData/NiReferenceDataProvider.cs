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
using System.Collections.ObjectModel;

namespace Paytools.ReferenceData;

public class NiReferenceDataProvider : INiReferenceDataProvider
{
    private readonly ReadOnlyDictionary<NiCategory, NiPeriodThresholdSet> _thresholdsByCategory;
    private readonly NiCategoryRateSet _ratesByCategory;

    public NiReferenceDataProvider(Dictionary<NiCategory, NiPeriodThresholdSet> thresholdsByCategory,
        NiCategoryRateSet ratesByCategory)
    {
        _thresholdsByCategory = new ReadOnlyDictionary<NiCategory, NiPeriodThresholdSet>(thresholdsByCategory);
        _ratesByCategory = ratesByCategory;
    }

    public INiCategoryRateEntry GetRatesForCategory(NiCategory niCategory) =>
        _ratesByCategory.GetRatesForCategory(niCategory);

    public NiPeriodThresholdSet GetThresholdsForCategory(NiCategory niCategory) =>
        _thresholdsByCategory[niCategory];

}
