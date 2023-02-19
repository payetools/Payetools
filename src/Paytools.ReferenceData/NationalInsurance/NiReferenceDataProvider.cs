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

using Paytools.Common.Model;
using Paytools.NationalInsurance;
using Paytools.NationalInsurance.ReferenceData;
using System.Collections.ObjectModel;

namespace Paytools.ReferenceData.NationalInsurance;

public class NiReferenceDataProvider : INiReferenceDataProvider
{
    private readonly ReadOnlyDictionary<NiCategory, INiCategoryRatesEntry> _ratesByCategory;
    private readonly INiThresholdSet _thresholds;

    public NiReferenceDataProvider(INiThresholdSet thresholds, NiCategoryRateSet ratesByCategory)
    {
        _thresholds = thresholds;
        _ratesByCategory = new ReadOnlyDictionary<NiCategory, INiCategoryRatesEntry>(ratesByCategory.GetRates());
    }

    public ReadOnlyDictionary<NiCategory, INiCategoryRatesEntry> GetTaxRatesForTaxYearAndPeriod(
        TaxYear taxYear, 
        PayFrequency payFrequency,
        int taxPeriod)
    {
        return _ratesByCategory;
    }

    public INiThresholdSet GetThresholdsForTaxYearAndPeriod(TaxYear taxYear, PayFrequency payFrequency, int taxPeriod)
    {
        return _thresholds;
    }
}
