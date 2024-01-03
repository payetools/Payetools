// Copyright (c) 2023-2024, Payetools Foundation.
//
// Paytools Foundation licenses this file to you under one of the following licenses:
//
//   * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//   * Paytools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

using Payetools.Common.Diagnostics;

namespace Payetools.ReferenceData;

/// <summary>
/// Interface for factory classes that are used to create new HMRC reference data providers that implement
/// <see cref="IHmrcReferenceDataProvider"/>.
/// </summary>
/// <remarks>If the CreateProviderAsync method completes successfully, the <see cref="IHmrcReferenceDataProvider.Health"/>
/// property of the created <see cref="IHmrcReferenceDataProvider"/> provides human-readable information on
/// the status of each tax year loaded.</remarks>
public interface IHmrcReferenceDataProviderFactory
{
    /// <summary>
    /// Creates a new HMRC reference data that implements <see cref="IHmrcReferenceDataProvider"/> using reference
    /// data loaded from an array of streams.
    /// </summary>
    /// <param name="referenceDataStreams">Array of data streams to load HMRC reference data from.</param>
    /// <returns>An instance of a type that implements <see cref="IHmrcReferenceDataProvider"/>.</returns>
    Task<IHmrcReferenceDataProvider> CreateProviderAsync(Stream[] referenceDataStreams);

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
    Task<IHmrcReferenceDataProvider> CreateProviderAsync(Uri referenceDataEndpoint);
}
