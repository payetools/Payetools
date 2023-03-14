﻿// Copyright (c) 2023 Paytools Foundation.
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

using Paytools.Payroll.Payruns;
using Paytools.ReferenceData;
using Paytools.Testing.Utils;

namespace Paytools.Payroll.Tests;

public class PayrollProcessorFactoryFixture
{
    private readonly AsyncLazy<IPayrunProcessorFactory> _factory = new AsyncLazy<IPayrunProcessorFactory>(async () =>
    {
        var referenceDataFactory = ReferenceDataHelper.GetFactory();

        var provider = await ReferenceDataHelper.CreateProviderAsync<IHmrcReferenceDataProvider>(new Stream[] { Resource.Load(@"ReferenceData\Payroll_2022_2023.json") });

        return new PayrunProcessorFactory(provider);
    });

    public async Task<IPayrunProcessorFactory> GetFactory()
    {
        return await _factory;
    }

}