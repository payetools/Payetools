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
using Paytools.Common.Serialization;
using Paytools.IncomeTax.ReferenceData;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace Paytools.ReferenceData;

public class TaxBandProvider : ITaxBandProvider
{
    private readonly ReadOnlyDictionary<CountriesForTaxPurposes, TaxBandwidthSet> _taxBands;

    private TaxBandProvider(Stream jsonContent)
    {
        int year = 0;


        var taxBands = JsonSerializer.Deserialize<TaxBandSet>(jsonContent, new JsonSerializerOptions()
        {
            // See https://github.com/dotnet/runtime/issues/31081 on why we can't just use JsonStringEnumConverter
            Converters =
            {
                new PayFrequencyJsonConverter(),
                new CountriesForTaxPurposesJsonConverter(),
                new TaxYearEndingJsonConverter()
            },
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        if (taxBands == null || taxBands.TaxYearEntries == null)
            throw new ArgumentException($"Unable to retrieve tax band(s) for tax year ending 5 April {year} from stream", nameof(jsonContent));

        _taxBands = new ReadOnlyDictionary<CountriesForTaxPurposes, TaxBandwidthSet>(taxBands.TaxYearEntries
            .ToDictionary(e => e.ApplicableCountries, e => new TaxBandwidthSet(e.GetTaxBandwidthEntries())));
    }

    public ReadOnlyDictionary<CountriesForTaxPurposes, TaxBandwidthSet> GetBandsForTaxYear(TaxYear taxYear)
    {
        //int year = (int)taxYear.TaxYearEnding;

        return _taxBands;
    }

    public static async Task<TaxBandProvider> GetTaxBandProvider(string url) 
    {
        using var client = new HttpClient();

        var response = await client.GetAsync(url);

        return new TaxBandProvider(response.Content.ReadAsStream());
    }
}