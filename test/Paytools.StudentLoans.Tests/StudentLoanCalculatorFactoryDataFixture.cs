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

using Paytools.StudentLoans;
using Paytools.StudentLoans.ReferenceData;
using Paytools.Testing.Utils;

namespace Paytools.NationalMinimumWage.Tests;

public class StudentLoanCalculatorFactoryDataFixture
{
    private AsyncLazy<IStudentLoanCalculatorFactory> _factory = new AsyncLazy<IStudentLoanCalculatorFactory>(async () =>
    {
        var referenceDataFactory = Testing.Utils.ReferenceDataHelper.GetFactory();

        var provider = await Testing.Utils.ReferenceDataHelper.CreateProviderAsync<IStudentLoanReferenceDataProvider>(new Stream[] { Resource.Load(@"ReferenceData\StudentLoans_2022_2023.json") });

        return new StudentLoanCalculatorFactory(provider);
    });

    public async Task<IStudentLoanCalculatorFactory> GetFactory()
    {
        return await _factory;
    }
}