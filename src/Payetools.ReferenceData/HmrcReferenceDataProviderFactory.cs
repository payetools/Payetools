// Copyright (c) 2023-2025, Payetools Ltd.
//
// Payetools Ltd licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Microsoft.Extensions.Logging;
using Payetools.AttachmentOrders.Serialisation;
using Payetools.Common.Diagnostics;
using Payetools.Common.Serialization;
using Payetools.ReferenceData.Serialisation;
using System.Text.Json;

namespace Payetools.ReferenceData;

/// <summary>
/// Factory class that is used to create new HMRC reference data providers that implement
/// <see cref="IHmrcReferenceDataProvider"/>.
/// </summary>
public class HmrcReferenceDataProviderFactory : IHmrcReferenceDataProviderFactory
{
    /// <summary>
    /// Gets logger for logging.  If not supplied in constructor (or null supplied), no logging is performed.
    /// </summary>
    protected ILogger<HmrcReferenceDataProviderFactory>? Logger { get; }

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
                new NiCategoryJsonTypeConverter(),
                new AttachmentOrderCalculationTypeConverter()
            },
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    /// <summary>
    /// Initialises a new <see cref="HmrcReferenceDataProviderFactory"/>.
    /// </summary>
    /// <param name="logger">Implementation of <see cref="ILogger"/> for logging.</param>
    public HmrcReferenceDataProviderFactory(ILogger<HmrcReferenceDataProviderFactory>? logger)
    {
        Logger = logger;
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
        Logger?.LogInformation("Attempting to create implementation of IHmrcReferenceDataProvider with array of Streams; {referenceDataStreams.Length} streams provided",
            referenceDataStreams.Length);

        var dataSets = new List<HmrcTaxYearReferenceDataSet>();

        for (int i = 0; i < referenceDataStreams.Length; i++)
        {
            var entry = await DeserializeAsync<HmrcTaxYearReferenceDataSet>(referenceDataStreams[i], $"Stream #{i}");

            Logger?.LogInformation("Retrieved reference data for tax year {entry.ApplicableTaxYearEnding}, version {entry.Version}",
                entry.ApplicableTaxYearEnding, entry.Version);

            dataSets.Add(entry);
        }

        return new HmrcReferenceDataProvider(dataSets);
    }

    /// <summary>
    /// Deserialises the supplied JSON stream.  Primarily used to deserialise into <see cref="HmrcTaxYearReferenceDataSet"/>
    /// but may be used to other types needed by derived classes of this factory.
    /// </summary>
    /// <param name="data">Stream to use as source.</param>
    /// <param name="source">Source name.</param>
    /// <typeparam name="T">Type of object to deserialise.</typeparam>
    /// <returns>Object of type T containing the deserialised data.</returns>
    /// <exception cref="InvalidReferenceDataException">Thrown if the supplied stream cannot be deserialised.</exception>
    protected static async Task<T> DeserializeAsync<T>(Stream data, string source)
    {
        try
        {
            return await JsonSerializer.DeserializeAsync<T>(data, _jsonSerializerOptions) ??
                throw new InvalidReferenceDataException($"Unable to deserialise response reference data from source '{source}' into type '{nameof(HmrcTaxYearReferenceDataSet)}'");
        }
        catch (JsonException ex)
        {
            throw new InvalidReferenceDataException($"Unable to parse data retrieved from reference data source '{source}' (see inner exception for details)", ex);
        }
    }
}