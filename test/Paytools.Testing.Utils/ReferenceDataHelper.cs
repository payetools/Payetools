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