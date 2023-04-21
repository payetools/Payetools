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