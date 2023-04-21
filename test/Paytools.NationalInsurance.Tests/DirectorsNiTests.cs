// Copyright (c) 2023 Paytools Foundation.
//
// Paytools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Paytools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

using FluentAssertions;
using Paytools.Common.Model;
using Paytools.NationalInsurance.Model;
using Paytools.Testing.Data;
using Paytools.Testing.Data.NationalInsurance;
using Xunit.Abstractions;

namespace Paytools.NationalInsurance.Tests;

public class DirectorsNiTests : IClassFixture<NiCalculatorFactoryDataFixture>
{
    private readonly ITestOutputHelper Output;
    private readonly NiCalculatorFactoryDataFixture _calculatorDataFixture;

    public DirectorsNiTests(ITestOutputHelper output, NiCalculatorFactoryDataFixture calculatorDataFixture)
    {
        Output = output;
        _calculatorDataFixture = calculatorDataFixture;
    }


    [Fact]
    public async Task RunFullYearDirectorTests()
    {
        var taxYear = new TaxYear(TaxYearEnding.Apr5_2023);

        var testData = GetTests(NiCategory.A, PayFrequency.Monthly, 12);

        int testsCompleted = 0;

        foreach (var test in testData.ToList().Where(t => t.RelatesTo == "Director" &&
            (t.PayFrequency == PayFrequency.Monthly || t.PayFrequency == PayFrequency.Weekly || t.PayFrequency == PayFrequency.FourWeekly)))
        {
            var payDate = new PayDate(taxYear.GetLastDayOfTaxPeriod(test.PayFrequency, test.Period), test.PayFrequency);

            var calculator = await GetCalculator(payDate);

            calculator.CalculateDirectors(DirectorsNiCalculationMethod.StandardAnnualisedEarningsMethod,
                test.NiCategory, test.GrossPay, 0, 0, null, out var result);

            result.EmployeeContribution.Should().Be(test.EmployeeNiContribution, "(test #{0}) input is {1} and output is {{ {2} }}", (testsCompleted + 1).ToString(), test.ToDebugString(), result.ToString());
            result.EmployerContribution.Should().Be(test.EmployerNiContribution, "(test #{0}) input is {1} and output is {{ {2} }}", (testsCompleted + 1).ToString(), test.ToDebugString(), result.ToString());
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

    private static IHmrcNiTestDataEntry[] GetTests(NiCategory niCategory, PayFrequency payFrequency, int taxPeriod)
    {
        return new IHmrcNiTestDataEntry[]
        {
            new HmrcNiTestDataEntry()
            {
                PayFrequency = payFrequency,
                NiCategory = niCategory,
                Period = taxPeriod,
                RelatesTo = "Director",
                GrossPay = 5000,
                EarningsAtLEL_YTD = 0,
                EarningsLELtoPT_YTD = 0,
                EarningsPTtoUEL_YTD = 0,
                EmployeeNiContribution = 0,
                EmployerNiContribution = 0,
                TotalNiContribution = 0,
                TotalEmployeeContributions_YTD = 0,
                TotalEmployerContributions_YTD = 0,
            },
            new HmrcNiTestDataEntry()
            {
                PayFrequency = payFrequency,
                NiCategory = niCategory,
                Period = taxPeriod,
                RelatesTo = "Director",
                GrossPay = 9000,
                EarningsAtLEL_YTD = 6396.0m,
                EarningsLELtoPT_YTD = 2604.0m,
                EarningsPTtoUEL_YTD = 0,
                EmployeeNiContribution = 0,
                EmployerNiContribution = 0,
                TotalNiContribution = 0,
                TotalEmployeeContributions_YTD = 0,
                TotalEmployerContributions_YTD = 0,
            },
            new HmrcNiTestDataEntry()
            {
                PayFrequency = payFrequency,
                NiCategory = niCategory,
                Period = taxPeriod,
                RelatesTo = "Director",
                GrossPay = 9100,
                EarningsAtLEL_YTD = 6396.0m,
                EarningsLELtoPT_YTD = 2704.0m,
                EarningsPTtoUEL_YTD = 0,
                EmployeeNiContribution = 0,
                EmployerNiContribution = 0,
                TotalNiContribution = 0,
                TotalEmployeeContributions_YTD = 0,
                TotalEmployerContributions_YTD = 0,
            },
            new HmrcNiTestDataEntry()
            {
                PayFrequency = payFrequency,
                NiCategory = niCategory,
                Period = taxPeriod,
                RelatesTo = "Director",
                GrossPay = 9101,
                EarningsAtLEL_YTD = 6396.0m,
                EarningsLELtoPT_YTD = 2705.0m,
                EarningsPTtoUEL_YTD = 0,
                EmployeeNiContribution = 0,
                EmployerNiContribution = 0.14m,
                TotalNiContribution = 0.14m,
                TotalEmployeeContributions_YTD = 0,
                TotalEmployerContributions_YTD = 0,
            },
            new HmrcNiTestDataEntry()
            {
                PayFrequency = payFrequency,
                NiCategory = niCategory,
                Period = taxPeriod,
                RelatesTo = "Director",
                GrossPay = 11908,
                EarningsAtLEL_YTD = 6396.0m,
                EarningsLELtoPT_YTD = 11908-6396,
                EarningsPTtoUEL_YTD = 0,
                EmployeeNiContribution = 0,
                EmployerNiContribution = 408.0m,
                TotalNiContribution = 408.0m,
                TotalEmployeeContributions_YTD = 0,
                TotalEmployerContributions_YTD = 0,
            },
            new HmrcNiTestDataEntry()
            {
                PayFrequency = payFrequency,
                NiCategory = niCategory,
                Period = taxPeriod,
                RelatesTo = "Director",
                GrossPay = 11909,
                EarningsAtLEL_YTD = 6396.0m,
                EarningsLELtoPT_YTD = 11908-6396,
                EarningsPTtoUEL_YTD = 1,
                EmployeeNiContribution = 0.13m,
                EmployerNiContribution = 408.15m,
                TotalNiContribution = 408.28m,
                TotalEmployeeContributions_YTD = 0,
                TotalEmployerContributions_YTD = 0,
            },
        };
    }

    private async Task<INiCalculator> GetCalculator(PayDate payDate)
    {
        var provider = await _calculatorDataFixture.GetFactory();

        return provider.GetCalculator(payDate);
    }
}
