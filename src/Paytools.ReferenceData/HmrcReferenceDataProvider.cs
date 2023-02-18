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
using Paytools.IncomeTax.ReferenceData;
using Paytools.NationalInsurance;
using Paytools.NationalInsurance.ReferenceData;
using System.Collections.Concurrent;
using System.Collections.ObjectModel;

namespace Paytools.ReferenceData;

internal class HmrcReferenceDataProvider : IHmrcReferenceDataProvider
{
    private ConcurrentDictionary<TaxYearEnding, HmrcTaxYearReferenceDataSet> _referenceDataSets;

    public string Health { get; internal set; }

    /// <summary>
    /// Initialises a new intance of <see cref="IHmrcReferenceDataProvider"/>.
    /// </summary>
    internal HmrcReferenceDataProvider()
    {
        _referenceDataSets = new ConcurrentDictionary<TaxYearEnding, HmrcTaxYearReferenceDataSet>();
        Health = "No tax years added";
    }

    internal bool TryAdd(HmrcTaxYearReferenceDataSet referenceDataSet)
        => _referenceDataSets.TryAdd(referenceDataSet.ApplicableTaxYearEnding, referenceDataSet);

    public ReadOnlyDictionary<CountriesForTaxPurposes, TaxBandwidthSet> GetBandsForTaxYearAndPeriod(TaxYear taxYear, PayFrequency payFrequency, int taxPeriod)
    {
        throw new NotImplementedException();
    }

    public ReadOnlyDictionary<NiCategory, INiCategoryRatesEntry> GetRatesForTaxYearAndPeriod(TaxYear taxYear, PayFrequency payFrequency, int taxPeriod)
    {
        throw new NotImplementedException();
    }

    public INiThresholdSet GetThresholdsForTaxYearAndPeriod(TaxYear taxYear, PayFrequency payFrequency, int taxPeriod)
    {
        throw new NotImplementedException();
    }
}