// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Microsoft.Extensions.Logging;
using Payetools.ReferenceData;

namespace Payetools.Testing.Utils;

public static class ReferenceDataHelper
{
    private static readonly string[] _resourcePaths =
    [
        @"ReferenceData\hmrc-reference-data-2022-2023.json",
        @"ReferenceData\hmrc-reference-data-2023-2024.json",
        @"ReferenceData\hmrc-reference-data-2024-2025.json",
        @"ReferenceData\hmrc-reference-data-2025-2026.json"
    ];

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