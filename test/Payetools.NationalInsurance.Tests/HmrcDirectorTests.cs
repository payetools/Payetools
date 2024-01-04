// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using FluentAssertions;
using Payetools.Common.Model;
using Payetools.NationalInsurance.Model;
using Payetools.Testing.Data;
using Payetools.Testing.Data.NationalInsurance;
using Xunit.Abstractions;

namespace Payetools.NationalInsurance.Tests;

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
        var taxYear = new TaxYear(TaxYearEnding.Apr5_2023);

        // HMRC tests require that first pay date is 8th April 2022
        var firstPayDate = taxYear.StartOfTaxYear.AddDays(2);

        await RunAllDirectorTests(taxYear, firstPayDate);
    }

    [Fact]
    public async Task RunAllDirectorTests_2023_2024()
    {
        var taxYear = new TaxYear(TaxYearEnding.Apr5_2024);

        var firstPayDate = taxYear.StartOfTaxYear.AddDays(5);

        await RunAllDirectorTests(taxYear, firstPayDate);
    }

    private async Task RunAllDirectorTests(TaxYear taxYear, DateOnly firstPayDate)
    {
        using var db = new TestDataRepository("National Insurance", Output);

        var testData = db.GetTestData<IHmrcDirectorsNiTestDataEntry>(TestSource.Hmrc, TestScope.NationalInsurance)
            .Where(t => t.RelatesTo == "Director" && t.TaxYearEnding == taxYear.TaxYearEnding);

        if (!testData.Any())
            Assert.Fail("No National Insurance tests found");

        Console.WriteLine($"{testData.Count()} tests found");
        Output.WriteLine($"{testData.Count()} National Insurance tests found");

        int testsCompleted = 0;

        foreach (var test in testData.ToList())
        {
            INiCalculationResult result;

            // The following is needed because HMRC refers the last tax period for an annual payroll as 52 rather than 1.
            var payDate = test.PayFrequency switch
            {
                PayFrequency.Weekly =>
                    new PayDate(firstPayDate.AddDays(test.PayFrequency.GetTaxPeriodLength() * (test.Period - 1)), test.PayFrequency),
                PayFrequency.Annually => new PayDate(taxYear.GetLastDayOfTaxPeriod(PayFrequency.Annually, 1), PayFrequency.Annually),
                _ => throw new InvalidOperationException("Currently only weekly and annual frequencies are included in HMRC test data")
            }; ;

            var calculator = await GetCalculator(payDate);

            // Clean up issues caused by original Excel-based source data
            var employeeNiContributionYtd = decimal.Round(test.EmployeeNiContributionYtd, 4, MidpointRounding.ToZero);
            var employerNiContributionYtd = decimal.Round(test.EmployerNiContributionYtd, 4, MidpointRounding.ToZero);
            var grossPayYtd = decimal.Round(test.GrossPayYtd, 4, MidpointRounding.ToZero);

            switch (test.StatusMethod)
            {
                case "ALT":
                    calculator.CalculateDirectors(DirectorsNiCalculationMethod.AlternativeMethod, test.NiCategory, test.GrossPay, grossPayYtd - test.GrossPay,
                        employeeNiContributionYtd - test.EmployeeNiContribution, employerNiContributionYtd - test.EmployerNiContribution, test.ProRataFactor,
                        out result);
                    break;

                case "STD":
                    calculator.CalculateDirectors(DirectorsNiCalculationMethod.StandardAnnualisedEarningsMethod, test.NiCategory, test.GrossPay, test.GrossPayYtd - test.GrossPay,
                        test.EmployeeNiContributionYtd - test.EmployeeNiContribution, test.EmployerNiContributionYtd - test.EmployerNiContribution, test.ProRataFactor,
                        out result);
                    break;

                case "EMP":
                    calculator.Calculate(test.NiCategory, test.GrossPay, out result);
                    break;

                default:
                    throw new InvalidOperationException($"Unrecognised value for StatusMethod: '{test.StatusMethod}'");
            }

            var testInfo = string.Format("input is {0} and output is {{ {1} }} (test #{2})", test.ToDebugString(), result.ToString(), (testsCompleted + 1).ToString());

            result.EmployeeContribution.Should().Be(test.EmployeeNiContribution, testInfo);
            result.EmployerContribution.Should().Be(test.EmployerNiContribution, testInfo);
            result.TotalContribution.Should().Be(test.TotalNiContribution, testInfo);

            // Currently can only test this on the last payrun of the year
            if (test.Period == 52)
            {
                result.EarningsBreakdown.EarningsUpToAndIncludingLEL.Should().Be(test.EarningsAtLEL_YTD, testInfo);

                var lelToPt = result.EarningsBreakdown.EarningsAboveLELUpToAndIncludingST + result.EarningsBreakdown.EarningsAboveSTUpToAndIncludingPT;
                lelToPt.Should().Be(test.EarningsLELtoPT_YTD, testInfo);

                var ptToUel = result.EarningsBreakdown.EarningsAbovePTUpToAndIncludingFUST + result.EarningsBreakdown.EarningsAboveFUSTUpToAndIncludingUEL;
                ptToUel.Should().Be(test.EarningsPTtoUEL_YTD, testInfo);
            }

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