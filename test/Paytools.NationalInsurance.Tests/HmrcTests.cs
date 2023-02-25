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

using FluentAssertions;
using Paytools.Common.Model;
using Paytools.NationalInsurance.ReferenceData;
using Paytools.Testing.Data;
using Paytools.Testing.Utils;
using Xunit.Abstractions;

namespace Paytools.NationalInsurance.Tests;

public class HmrcTests
{
    private readonly ITestOutputHelper Output;

    public HmrcTests(ITestOutputHelper output)
    {
        Output = output;
    }

    [Fact]
    public async Task RunAllTests()
    {
        var taxYear = new TaxYear(TaxYearEnding.Apr5_2023);

        var referenceDataFactory = Testing.Utils.ReferenceData.GetFactory();

        var provider = await Testing.Utils.ReferenceData.CreateProviderAsync<INiReferenceDataProvider>(new Stream[] { Resource.Load(@"ReferenceData\NationalInsurance_2022_2023.json") });

        using var db = new TestDataRepository();

        var testData = db.GetTestData<IHmrcNiTestDataEntry>(TestSource.Hmrc, TestScope.NationalInsurance);

        int testsCompleted = 0;

        var factory = new NiCalculatorFactory(provider);

        foreach (var test in testData.ToList().Where(t => //t.NiCategory == NiCategory.A &&
            t.PayFrequency == PayFrequency.Monthly || t.PayFrequency == PayFrequency.Weekly || t.PayFrequency == PayFrequency.FourWeekly))
        //foreach (var test in testData)
        {
            var calculator = factory.GetCalculator(taxYear, test.PayFrequency, test.Period);

            var result1 = calculator.Calculate(test.NiCategory, test.GrossPay);

            test.EmployeeNiContribution.Should().Be(result1.EmployeeContribution, "input is {0} and output is {1}", test.ToDebugString(), result1.ToString());
            test.EmployerNiContribution.Should().Be(result1.EmployerContribution, "input is {0} and output is {1}", test.ToDebugString(), result1.ToString());

            testsCompleted++;
        }

        Output.WriteLine($"{testsCompleted} tests completed");
    }
}
