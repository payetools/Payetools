// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using FluentAssertions;
using Payetools.Common.Model;
using Payetools.Testing.Data;
using Payetools.Testing.Data.NationalInsurance;
using Xunit.Abstractions;

namespace Payetools.NationalInsurance.Tests;

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
    public async Task RunAllNonDirectorTests_2022_2023()
    {
        await RunAllNonDirectorTests(new TaxYear(TaxYearEnding.Apr5_2023));
    }

    [Fact]
    public async Task RunAllNonDirectorTests_2023_2024()
    {
        await RunAllNonDirectorTests(new TaxYear(TaxYearEnding.Apr5_2024));
    }

    private async Task RunAllNonDirectorTests(TaxYear taxYear)
    {
        using var db = new TestDataRepository("National Insurance", Output);

        var testData = db.GetTestData<IHmrcNiTestDataEntry>(TestSource.Hmrc, TestScope.NationalInsurance)
            .Where(t => t.RelatesTo == "Employee" && t.TaxYearEnding == taxYear.TaxYearEnding);

        if (!testData.Any())
            Assert.Fail("No National Insurance tests found");

        Console.WriteLine($"{testData.Count()} tests found");
        Output.WriteLine($"{testData.Count()} National Insurance tests found");

        int testsCompleted = 0;

        foreach (var test in testData.ToList())
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