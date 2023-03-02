// Copyright (c) 2023 Paytools Foundation.
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

namespace Paytools.ReferenceData;

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
