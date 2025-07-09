// Copyright (c) 2023-2025, Payetools Ltd.
//
// Payetools Ltd licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.ReferenceData;

namespace Payetools.Testing.Utils;

public abstract class CalculatorFactoryDataFixture<T> where T : class
{
    protected AsyncLazy<T> _factory;

    protected CalculatorFactoryDataFixture()
    {
        _factory = new AsyncLazy<T>(async () =>
        {
            var provider = await ReferenceDataHelper.CreateProviderAsync<IHmrcReferenceDataProvider>();

            return MakeFactory(provider);
        });

    }

    public async Task<T> GetFactory()
    {
        return await (_factory ?? throw new InvalidOperationException("Factory member uninitialised"));
    }

    protected abstract T MakeFactory(IHmrcReferenceDataProvider provider);
}