// Copyright (c) 2023 Paytools Foundation.
//
// Paytools Foundation licenses this file to you under one of the following licenses:
//
//   * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//   * Paytools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

using FluentAssertions;
using Paytools.Common.Model;
using Paytools.NationalInsurance.Model;
using Paytools.Testing.Data;
using Paytools.Testing.Data.NationalInsurance;
using Xunit.Abstractions;
using static System.Net.Mime.MediaTypeNames;

namespace Paytools.NationalInsurance.Tests;

public class HmrcDirectorTests : IClassFixture<NiCalculatorFactoryDataFixture>
{
    private readonly ITestOutputHelper Output;
    private readonly NiCalculatorFactoryDataFixture _calculatorDataFixture;

    public HmrcDirectorTests(ITestOutputHelper output, NiCalculatorFactoryDataFixture calculatorDataFixture)
    {
        Output = output;
        _calculatorDataFixture = calculatorDataFixture;
    }

    [Fact]
    public async Task RunAllDirectorTests_2022_2023()
    {
        //await RunAllDirectorTests(new TaxYear(TaxYearEnding.Apr5_2023));
        await Task.CompletedTask;
    }

    //[Fact]
    //public async Task RunAllDirectorTests_2023_2024()
    //{
    //    await RunAllNonDirectorTests(new TaxYear(TaxYearEnding.Apr5_2024));
    //}

    private async Task RunAllDirectorTests(TaxYear taxYear)
    {
        await Task.CompletedTask;

        //using var db = new TestDataRepository("National Insurance", Output);

        //var testData = db.GetTestData<IHmrcDirectorsNiTestDataEntry>(TestSource.Hmrc, TestScope.NationalInsurance)
        //    .Where(t => t.RelatesTo == "Director" && t.TaxYearEnding == taxYear.TaxYearEnding);

        //if (!testData.Any())
        //    Assert.Fail("No National Insurance tests found");

        //Console.WriteLine($"{testData.Count()} tests found");
        //Output.WriteLine($"{testData.Count()} National Insurance tests found");

        //int testsCompleted = 0;

        //foreach (var test in testData.ToList())
        //{
        //    INiCalculationResult result;
        //    var payDate = new PayDate(taxYear.GetLastDayOfTaxPeriod(test.PayFrequency, test.Period), test.PayFrequency);

        //    var calculator = await GetCalculator(payDate);

        //    switch (test.StatusMethod)
        //    {
        //        case "ALT":
        //            calculator.CalculateDirectors(DirectorsNiCalculationMethod.AlternativeMethod, test.NiCategory,  test.GrossPay, out result);
        //            break;

        //        case "STD":
        //            break;

        //        case "EMP":
        //            calculator.Calculate(test.NiCategory, test.GrossPay, out result);
        //            break;

        //        default:
        //            throw new InvalidOperationException($"Unrecognised value for StatusMethod: '{test.StatusMethod}'");
        //    }

        //    result.EmployeeContribution.Should().Be(test.EmployeeNiContribution, "input is {0} and output is {{ {1} }} (test #{2})", test.ToDebugString(), result.ToString(), (testsCompleted + 1).ToString());
        //    result.EmployerContribution.Should().Be(test.EmployerNiContribution, "input is {0} and output is {{ {1} }} (test #{2})", test.ToDebugString(), result.ToString(), (testsCompleted + 1).ToString());
        //    result.EarningsBreakdown.EarningsUpToAndIncludingLEL.Should().Be(test.EarningsAtLEL_YTD);

        //    var lelToPt = result.EarningsBreakdown.EarningsAboveLELUpToAndIncludingST + result.EarningsBreakdown.EarningsAboveSTUpToAndIncludingPT;
        //    lelToPt.Should().Be(test.EarningsLELtoPT_YTD);

        //    var ptToUel = result.EarningsBreakdown.EarningsAbovePTUpToAndIncludingFUST + result.EarningsBreakdown.EarningsAboveFUSTUpToAndIncludingUEL;
        //    ptToUel.Should().Be(test.EarningsPTtoUEL_YTD);

        //    result.TotalContribution.Should().Be(test.TotalNiContribution);

        //    testsCompleted++;
        //}

        //Output.WriteLine($"{testsCompleted} tests completed");
    }

    private async Task<INiCalculator> GetCalculator(PayDate payDate)
    {
        var provider = await _calculatorDataFixture.GetFactory();

        return provider.GetCalculator(payDate);
    }
}