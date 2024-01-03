// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Payetools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

using Microsoft.Extensions.Logging;
using Payetools.Common.Diagnostics;
using Payetools.Common.Serialization;
using Payetools.ReferenceData.Serialization;
using System;
using System.Text.Json;

namespace Payetools.ReferenceData;

/// <summary>
/// Factory class that is used to create new HMRC reference data providers that implement
/// <see cref="IHmrcReferenceDataProvider"/>.
/// </summary>
public class HmrcReferenceDataProviderFactory : IHmrcReferenceDataProviderFactory
{
    private readonly IHttpClientFactory? _httpClientFactory;
    private readonly ILogger<HmrcReferenceDataProviderFactory>? _logger;

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
    /// Initialises a new instance of <see cref="HmrcReferenceDataProviderFactory"/> for use with Streams; do not use this
    /// constructor if accessing HMRC reference data over HTTP(S) is required.
    /// </summary>
    /// <param name="logger">An implementation of <see cref="ILogger{HmrcReferenceDataProviderFactory}"/> used
    /// for logging.</param>
    public HmrcReferenceDataProviderFactory(ILogger<HmrcReferenceDataProviderFactory>? logger = null)
    {
        _httpClientFactory = null;
        _logger = logger;
    }

    /// <summary>
    /// Initialises a new instance of <see cref="HmrcReferenceDataProviderFactory"/>.  An <see cref="IHttpClientFactory"/>
    /// is required to provide <see cref="HttpClient"/> instances to retrieve the reference data from the cloud.
    /// </summary>
    /// <param name="httpClientFactory">Implementation of <see cref="IHttpClientFactory"/>.</param>
    /// <param name="logger">An implementation of <see cref="ILogger{HmrcReferenceDataProviderFactory}"/> used
    /// for logging.</param>
    public HmrcReferenceDataProviderFactory(
        IHttpClientFactory httpClientFactory,
        ILogger<HmrcReferenceDataProviderFactory>? logger = null)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    /// <summary>
    /// Creates a new HMRC reference data that implements <see cref="IHmrcReferenceDataProvider"/> using reference
    /// data loaded from an array of streams.
    /// </summary>
    /// <param name="referenceDataStreams">Array of data streams to load HMRC reference data from.</param>
    /// <returns>An instance of a type that implements <see cref="IHmrcReferenceDataProvider"/>.</returns>
    /// <remarks>If the method completes successfully, the <see cref="IHmrcReferenceDataProvider.Health"/>
    /// property of the created <see cref="IHmrcReferenceDataProvider"/> provides human-readable information on
    /// the status of each tax year loaded.</remarks>
    /// <exception cref="InvalidReferenceDataException">Thrown if it was not possible to load
    /// reference data from the supplied set of streams.</exception>
    public async Task<IHmrcReferenceDataProvider> CreateProviderAsync(Stream[] referenceDataStreams)
    {
        _logger?.LogInformation("Attempting to create implementation of IHmrcReferenceDataProvider with array of Streams; {referenceDataStreams.Length} streams provided",
            referenceDataStreams.Length);

        var provider = new HmrcReferenceDataProvider();
        var health = new List<string>();

        for (int i = 0; i < referenceDataStreams.Length; i++)
        {
            var entry = await DeserializeAsync<HmrcTaxYearReferenceDataSet>(referenceDataStreams[i], $"Stream #{i}");

            _logger?.LogInformation("Retrieved reference data for tax year {entry.ApplicableTaxYearEnding}, version {entry.Version}",
                entry.ApplicableTaxYearEnding, entry.Version);

            health.Add(provider.TryAdd(entry) ?
                $"{entry.ApplicableTaxYearEnding}:OK" :
                $"{entry.ApplicableTaxYearEnding}:Failed to load data using from stream #{i}");
        }

        provider.Health = string.Join('|', health.ToArray());

        return provider;
    }

    /// <summary>
    /// Creates a new HMRC reference data that implements <see cref="IHmrcReferenceDataProvider"/> using reference data returned from
    /// an HTTP(S) endpoint.
    /// </summary>
    /// <param name="referenceDataEndpoint">The HTTP(S) endpoint to retrieve HMRC reference data from.</param>
    /// <returns>An instance of a type that implements <see cref="IHmrcReferenceDataProvider"/>.</returns>
    /// <exception cref="InvalidReferenceDataException">Thrown if it was not possible to retrieve
    /// reference data from the supplied endpoint.</exception>
    /// <exception cref="InvalidOperationException">Thrown if this factory was created without a valid <see cref="IHttpClientFactory"/>
    /// instance.</exception>
    /// <remarks>If the method completes successfully, the <see cref="IHmrcReferenceDataProvider.Health"/>
    /// property of the created <see cref="IHmrcReferenceDataProvider"/> provides human-readable information on
    /// the status of each tax year loaded.</remarks>
    public async Task<IHmrcReferenceDataProvider> CreateProviderAsync(Uri referenceDataEndpoint)
    {
        if (_httpClientFactory == null)
            throw new InvalidOperationException("Unable to retrieve reference data via HTTP(S); no IHttpClientFactory provided.  Use alternate constructor.");

        _logger?.LogInformation("Attempting to create implementation of IHmrcReferenceDataProvider from HTTP(S) endpoint '{referenceDataEndpoint}'",
            referenceDataEndpoint);

        // Get the list of supported tax years
        var taxYearUris = await RetrieveFromHttpEndpoint<List<Uri>>(referenceDataEndpoint);

        if (!taxYearUris.Any())
            throw new InvalidReferenceDataException($"No valid tax year entries returned from endpoint {referenceDataEndpoint}");

        _logger?.LogInformation("Retrieved links to {taxYearUris.Count} item(s) from HTTP(S) endpoint '{referenceDataEndpoint}'",
            taxYearUris.Count, referenceDataEndpoint);

        var provider = new HmrcReferenceDataProvider();
        var health = new List<string>();

        foreach (var uri in taxYearUris)
        {
            var entry = await RetrieveFromHttpEndpoint<HmrcTaxYearReferenceDataSet>(uri);

            _logger?.LogInformation("Retrieved reference data for tax year {entry.ApplicableTaxYearEnding}, version {entry.Version}",
                entry.ApplicableTaxYearEnding, entry.Version);

            health.Add(provider.TryAdd(entry) ?
                $"{entry.ApplicableTaxYearEnding}:OK" :
                $"{entry.ApplicableTaxYearEnding}:Failed to load data using from URI '{uri}'");
        }

        provider.Health = string.Join('|', health.ToArray());

        return provider;
    }

    private async Task<T> RetrieveFromHttpEndpoint<T>(Uri endpoint)
    {
        try
        {
            var client = _httpClientFactory?.CreateClient() ??
                throw new InvalidOperationException("Unable to retrieve reference data via HTTP(S); failed to create HTTP client.");

            _logger?.LogInformation("Retrieving reference data set via HTTP GET from '{endpoint}'", endpoint);

            var response = await client.GetAsync(endpoint);

            if (response == null || !response.IsSuccessStatusCode)
            {
                _logger?.LogWarning("Failed to retrieve item from endpoint {endpoint}; HTTP status code = {response?.StatusCode}", endpoint, response?.StatusCode);

                throw new InvalidReferenceDataException($"Unable to retrieve data from reference data endpoint '{endpoint}'; status code = {response?.StatusCode}, status text = '{response?.ReasonPhrase}'");
            }

            _logger?.LogInformation("Retrieved data from endpoint {endpoint} with HTTP status code {response.StatusCode}", endpoint, response.StatusCode);

            return await DeserializeAsync<T>(response.Content.ReadAsStream(), endpoint.ToString());
        }
        catch (HttpRequestException ex)
        {
            throw new InvalidReferenceDataException($"Unable to retrieve data from reference data endpoint '{endpoint}' (see inner exception for details)", ex);
        }
    }

    private static async Task<T> DeserializeAsync<T>(Stream data, string source)
    {
        try
        {
            return await JsonSerializer.DeserializeAsync<T>(data, _jsonSerializerOptions) ??
                throw new InvalidReferenceDataException($"Unable to deserialise response reference data from source '{source}' into type '{typeof(T).Name}'");
        }
        catch (JsonException ex)
        {
            throw new InvalidReferenceDataException($"Unable to parse data retrieved from reference data source '{source}' (see inner exception for details)", ex);
        }
    }
}