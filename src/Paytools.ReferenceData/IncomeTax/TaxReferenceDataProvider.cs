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
using Paytools.Common.Serialization;
using Paytools.IncomeTax.ReferenceData;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace Paytools.ReferenceData.IncomeTax;

public class TaxReferenceDataProvider : ITaxReferenceDataProvider
{
    private readonly ReadOnlyDictionary<CountriesForTaxPurposes, TaxBandwidthSet> _taxBands;

    private TaxReferenceDataProvider(Stream jsonContent)
    {
        //int year = 0;


        //var taxBands = JsonSerializer.Deserialize<TaxBandSet>(jsonContent, new JsonSerializerOptions()
        //{
        //    // See https://github.com/dotnet/runtime/issues/31081 on why we can't just use JsonStringEnumConverter
        //    Converters =
        //    {
        //        new PayFrequencyJsonConverter(),
        //        new CountriesForTaxPurposesJsonConverter(),
        //        new TaxYearEndingJsonConverter()
        //    },
        //    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        //});

        //if (taxBands == null || taxBands.TaxYearEntries == null)
        //    throw new ArgumentException($"Unable to retrieve tax band(s) for tax year ending 5 April {year} from stream", nameof(jsonContent));

        //_taxBands = new ReadOnlyDictionary<CountriesForTaxPurposes, TaxBandwidthSet>(taxBands.TaxYearEntries
        //    .ToDictionary(e => e.ApplicableCountries, e => new TaxBandwidthSet(e.GetTaxBandwidthEntries())));
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="taxYear"></param>
    /// <param name="payFrequency">Pay frequency pertaining.  Used in conjunction with the taxPeriod parameter to
    /// <param name="period"></param>
    /// <returns></returns>
    public ReadOnlyDictionary<CountriesForTaxPurposes, TaxBandwidthSet> GetBandsForTaxYearAndPeriod(
        TaxYear taxYear,
        PayFrequency payFrequency,
        int period)
    {
        //int year = (int)taxYear.TaxYearEnding;

        return _taxBands;
    }

    public static async Task<TaxReferenceDataProvider> GetTaxReferenceDataProvider(string url)
    {
        using var client = new HttpClient();

        var response = await client.GetAsync(url);

        return new TaxReferenceDataProvider(response.Content.ReadAsStream());
    }
}