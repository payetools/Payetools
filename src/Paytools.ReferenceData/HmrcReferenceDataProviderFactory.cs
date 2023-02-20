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

using Paytools.Common.Diagnostics;
using Paytools.Common.Serialization;
using Paytools.ReferenceData.Serialization;
using System.Text.Json;

namespace Paytools.ReferenceData;

/// <summary>
/// Factory class that is used to create new HMRC reference data providers that implement
/// <see cref="IHmrcReferenceDataProvider"/>.
/// </summary>
/// <remarks>If the <see cref="CreateProviderAsync"/> method completes successfully, the <see cref="IHmrcReferenceDataProvider.Health"/>
/// property of the created <see cref="IHmrcReferenceDataProvider"/> provides human-readable information on
/// the status of each tax year loaded.</remarks>
public class HmrcReferenceDataProviderFactory
{
    private readonly IHttpClientFactory _httpClientFactory;

    private static readonly JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions()
    {
        // See https://github.com/dotnet/runtime/issues/31081 on why we can't just use JsonStringEnumConverter
        Converters =
            {
            new PayFrequencyJsonConverter(),
                new CountriesForTaxPurposesJsonConverter(),
                new TaxYearEndingJsonConverter(),
                new DateOnlyJsonConverter(),
                new NiThresholdTypeJsonConverter(),
                new NiCategoryJsonTypeConverter()
            },
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    /// <summary>
    /// Initialises a new instance of <see cref="HmrcReferenceDataProviderFactory"/>.  An <see cref="IHttpClientFactory"/>
    /// is required to provide <see cref="HttpClient"/> instances to retrieve the reference data from the cloud.
    /// </summary>
    /// <param name="httpClientFactory"></param>
    public HmrcReferenceDataProviderFactory(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    /// <summary>
    /// Creates a new HMRC reference data that implements <see cref="IHmrcReferenceDataProvider"/>. 
    /// </summary>
    /// <param name="referenceDataEndpoint">The HTTP(S) endpoint to retrieve HMRC reference data from.</param>
    /// <returns>An instance of a type that implements <see cref="IHmrcReferenceDataProvider"/>.</returns>
    /// <exception cref="InvalidReferenceDataException">Thrown if it was not possible to retrieve
    /// reference data from the supplied endpoint.</exception>
    public async Task<IHmrcReferenceDataProvider> CreateProviderAsync(Uri referenceDataEndpoint)
    {
        // Get the list of supported tax years
        var taxYearUris = await Retrieve<List<Uri>>(referenceDataEndpoint);

        if (!taxYearUris.Any())
            throw new InvalidReferenceDataException($"No valid tax year entries returned from endpoint {referenceDataEndpoint}");

        var referenceDataProvider = new HmrcReferenceDataProvider();
        var health = new List<string>();

        // NB This was originally implemented in Parallel.Foreach() loop but there must be some
        // issue with the default IHttpClientFactory implementation that prevents parallel usage
        // (or some other non-obvious issue).
        foreach (var uri in taxYearUris)
        {
            try
            {
                var taxYearEntry = await Retrieve<HmrcTaxYearReferenceDataSet>(uri);

                health.Add(referenceDataProvider.TryAdd(taxYearEntry) ?
                    $"{taxYearEntry.ApplicableTaxYearEnding}:OK" :
                    $"{taxYearEntry.ApplicableTaxYearEnding}:Failed to load from '{uri}'");
            }
            catch (InvalidReferenceDataException ex)
            {
                health.Add($"Uri '{uri}' failed to load with message: {ex.Message}");
            }
        }

        referenceDataProvider.Health = string.Join('|', health.ToArray());

        return referenceDataProvider;
    }

    /// <summary>
    /// Retrieves the item or items of the specified type from the supplied endpoint over HTTP(S).
    /// </summary>
    /// <typeparam name="T">Type of object to retrieve.</typeparam>
    /// <param name="endpoint">Endpoint to retrieve object(s) from.</param>
    /// <returns></returns>
    /// <exception cref="InvalidReferenceDataException">Thrown if the HTTP(S) request was unsuccessful or if
    /// it was not possible to deserialise the data retrieved into the specified type.</exception>
    private async Task<T> Retrieve<T>(Uri endpoint)
    {
        try
        {
            using var client = _httpClientFactory.CreateClient();

            var response = await client.GetAsync(endpoint);

            if (response == null || !response.IsSuccessStatusCode)
                throw new InvalidReferenceDataException($"Unable to retrieve data from reference data endpoint '{endpoint}'; status code = {response?.StatusCode}, status text = ''{response?.ReasonPhrase}");

            return JsonSerializer.Deserialize<T>(response.Content.ReadAsStream(), _jsonSerializerOptions) ??
                throw new InvalidReferenceDataException($"Unable to deserialise response reference data endpoint '{endpoint}' into type '{typeof(T).Name}'");
        }
        catch (HttpRequestException ex)
        {
            throw new InvalidReferenceDataException($"Unable to retrieve data from reference data endpoint '{endpoint}' (see inner exception for details)", ex);
        }
        catch (JsonException ex)
        {
            throw new InvalidReferenceDataException($"Unable to parse data retrieved from reference data endpoint '{endpoint}' (see inner exception for details)", ex);
        }
    }
}