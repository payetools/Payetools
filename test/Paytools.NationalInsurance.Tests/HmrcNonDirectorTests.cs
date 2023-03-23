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
using Paytools.Testing.Data;
using Paytools.Testing.Data.NationalInsurance;
using Xunit.Abstractions;

namespace Paytools.NationalInsurance.Tests;

public class HmrcNonDirectorTests : IClassFixture<NiCalculatorFactoryDataFixture>
{
    private readonly ITestOutputHelper Output;
    private readonly NiCalculatorFactoryDataFixture _calculatorDataFixture;

    public HmrcNonDirectorTests(ITestOutputHelper output, NiCalculatorFactoryDataFixture calculatorDataFixture)
    {
        Output = output;
        _calculatorDataFixture = calculatorDataFixture;
    }

    [Fact]
    public async Task RunAllNonDirectorTests()
    {
        var taxYear = new TaxYear(TaxYearEnding.Apr5_2023);

        using var db = new TestDataRepository("National Insurance", Output);

        var testData = db.GetTestData<IHmrcNiTestDataEntry>(TestSource.Hmrc, TestScope.NationalInsurance);

        int testsCompleted = 0;

        foreach (var test in testData.ToList().Where(t => t.RelatesTo == "Employee" &&
            (t.PayFrequency == PayFrequency.Monthly || t.PayFrequency == PayFrequency.Weekly || t.PayFrequency == PayFrequency.FourWeekly)))
        {
            var payDate = new PayDate(taxYear.GetLastDayOfTaxPeriod(test.PayFrequency, test.Period), test.PayFrequency);

            var calculator = await GetCalculator(payDate);

            calculator.Calculate(test.NiCategory, test.GrossPay, out var result);

            result.EmployeeContribution.Should().Be(test.EmployeeNiContribution, "input is {0} and output is {{ {1} }} (test #{2})", test.ToDebugString(), result.ToString(), (testsCompleted + 1).ToString());
            result.EmployerContribution.Should().Be(test.EmployerNiContribution, "input is {0} and output is {{ {1} }} (test #{2})", test.ToDebugString(), result.ToString(), (testsCompleted + 1).ToString());
            result.EarningsBreakdown.EarningsUpToAndIncludingLEL.Should().Be(test.EarningsAtLEL_YTD);

            var lelToPt = result.EarningsBreakdown.EarningsAboveLELUpToAndIncludingST + result.EarningsBreakdown.EarningsAboveSTUpToAndIncludingPT;
            lelToPt.Should().Be(test.EarningsLELtoPT_YTD);

            var ptToUel = result.EarningsBreakdown.EarningsAbovePTUpToAndIncludingFUST + result.EarningsBreakdown.EarningsAboveFUSTUpToAndIncludingUEL;
            ptToUel.Should().Be(test.EarningsPTtoUEL_YTD);

            result.TotalContribution.Should().Be(test.TotalNiContribution);

            testsCompleted++;
        }

        Output.WriteLine($"{testsCompleted} tests completed");
    }

    private async Task<INiCalculator> GetCalculator(PayDate payDate)
    {
        var provider = await _calculatorDataFixture.GetFactory();

        return provider.GetCalculator(payDate);
    }
}