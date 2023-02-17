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

using Paytools.Common.Model;
using Paytools.ReferenceData.IncomeTax;

namespace Paytools.ReferenceData.Tests;

public class TaxBandProviderTests
{
    [Fact]
    public async Task InitialiseTest()
    {
        TaxReferenceDataProvider provider = await TaxReferenceDataProvider.GetTaxBandProvider("https://stellular-bombolone-34e67e.netlify.app/hmrc.json");

        var bands = provider.GetBandsForTaxYearAndPeriod(new TaxYear(TaxYearEnding.Apr5_2023), PayFrequency.Monthly, 1);

        //var bandwidths = new TaxBandwidthSet(bands[Common.CountriesForTaxPurposes.England | Common.CountriesForTaxPurposes.NorthernIreland]);

        //var bandwidthsForPeriod = bandwidths.GetBandwidthSetForPeriod(8, 12);
    }
}
