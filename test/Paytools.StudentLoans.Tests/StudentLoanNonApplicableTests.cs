// Copyright (c) 2023 Paytools Foundation.  All rights reserved.
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

using Paytools.Common.Model;
using Paytools.NationalMinimumWage.Tests;

namespace Paytools.StudentLoans.Tests;

public class StudentLoanNonApplicableTests : IClassFixture<StudentLoanCalculatorFactoryDataFixture>
{
    private readonly PayDate _payDate = new PayDate(2022, 5, 5, PayFrequency.Monthly);
    private readonly StudentLoanCalculatorFactoryDataFixture _factoryProviderFixture;

    public StudentLoanNonApplicableTests(StudentLoanCalculatorFactoryDataFixture factoryProviderFixture)
    {
        _factoryProviderFixture = factoryProviderFixture;
    }
    private async Task<IStudentLoanCalculator> GetCalculator()
    {
        var provider = await _factoryProviderFixture.GetFactory();

        return provider.GetCalculator(_payDate);
    }
}
