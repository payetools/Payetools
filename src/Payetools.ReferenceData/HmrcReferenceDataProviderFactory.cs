// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

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
    /// Initialises a new <see cref="HmrcReferenceDataProviderFactory"/>.
    /// </summary>
    /// <param name="logger">Implementation of <see cref="ILogger"/> for logging.</param>
    public HmrcReferenceDataProviderFactory(ILogger<HmrcReferenceDataProviderFactory>? logger)
    {
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