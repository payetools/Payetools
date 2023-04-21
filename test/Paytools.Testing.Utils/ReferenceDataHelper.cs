// Copyright (c) 2023 Paytools Foundation.
//
// Paytools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Paytools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

using Paytools.ReferenceData;

namespace Paytools.Testing.Utils;

public static class ReferenceDataHelper
{
    private static readonly string[] _resourcePaths = new[]
{
        @"ReferenceData\HmrcReferenceData_2022_2023.json",
        @"ReferenceData\HmrcReferenceData_2023_2024.json"
    };

    public static HmrcReferenceDataProviderFactory GetFactory() =>
        new HmrcReferenceDataProviderFactory();

    public async static Task<T> CreateProviderAsync<T>() where T : class
    {
        var referenceDataStreams = _resourcePaths.Select(p => Resource.Load(p)).ToArray();

        var factory = await GetFactory().CreateProviderAsync(referenceDataStreams) as T ??
            throw new InvalidCastException("Unable to cast reference data provider to specified type");

        referenceDataStreams.ToList().ForEach(s => s.Dispose());

        return factory;
    }
}