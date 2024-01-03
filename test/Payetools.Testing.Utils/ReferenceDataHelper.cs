// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Microsoft.Extensions.Logging;
using Payetools.ReferenceData;

namespace Payetools.Testing.Utils;

public static class ReferenceDataHelper
{
    private static readonly string[] _resourcePaths = new[]
{
        @"ReferenceData\HmrcReferenceData_2022_2023.json",
        @"ReferenceData\HmrcReferenceData_2023_2024.json"
    };

    public static HmrcReferenceDataProviderFactory GetFactory() =>
        new HmrcReferenceDataProviderFactory(MakeLogger());

    public async static Task<T> CreateProviderAsync<T>() where T : class
    {
        var referenceDataStreams = _resourcePaths.Select(p => Resource.Load(p)).ToArray();

        var factory = await GetFactory().CreateProviderAsync(referenceDataStreams) as T ??
            throw new InvalidCastException("Unable to cast reference data provider to specified type");

        referenceDataStreams.ToList().ForEach(s => s.Dispose());

        return factory;
    }

    private static ILogger<HmrcReferenceDataProviderFactory> MakeLogger()
    {
        var factory = LoggerFactory.Create(builder => {
            builder.AddConsole();
        });

        return factory.CreateLogger<HmrcReferenceDataProviderFactory>();
    }
}