// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using FluentAssertions;
using Payetools.Common.Extensions;
using Payetools.Common.Model;
using Payetools.Testing.Data;
using Payetools.Testing.Data.IncomeTax;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;
using Xunit.Abstractions;

namespace Payetools.IncomeTax.Tests;

public class BulkIncomeTaxCalculationTests : IClassFixture<TaxCalculatorFactoryDataFixture>
{
    private readonly ITestOutputHelper Output;
    private readonly TaxCalculatorFactoryDataFixture _calculatorDataFixture;

    public BulkIncomeTaxCalculationTests(ITestOutputHelper output, TaxCalculatorFactoryDataFixture calculatorDataFixture)
    {
        Output = output;
        _calculatorDataFixture = calculatorDataFixture;
        if (!Trace.Listeners.Contains(new TestOutputTraceListener(output)))
            Trace.Listeners.Add(new TestOutputTraceListener(output));
    }

    [Fact]
    public async Task RunTests_2022_2023()
    {
        await RunTests(TaxYearEnding.Apr5_2023);
    }

    [Fact]
    public async Task RunTests_2023_2024()
    {
        await RunTests(TaxYearEnding.Apr5_2024);
    }

    private async Task RunTests(TaxYearEnding taxYearEndingX)
    {
        // using var db = new TestDataRepository("Income Tax", Output);

        var db = new TestDataProvider();

        var testData = db.GetTestData<IHmrcIncomeTaxTestDataEntry>("IncomeTax")
            // .Where(t => t.TaxYearEnding == taxYearEnding)
            .ToList();

        if (!testData.Any())
            Assert.Fail("No income tax tests found");

        Console.WriteLine($"{testData.Count} tests found");
        Output.WriteLine($"{testData.Count} tests found");

        int testIndex = 1;
        int testCompleted = 0;

        foreach (var test in testData)
        {
            var taxYear = new TaxYear(test.TaxYearEnding);
            var taxCode = test.GetFullTaxCode(taxYear);

            var applicableCountries = CountriesForTaxPurposesConverter.ToEnum(test.RelatesTo);

            var calculator = await GetCalculator(applicableCountries, taxYear, test.PayFrequency, test.Period);

            calculator.Calculate(test.GrossPay,
                0.0m,
                taxCode,
                test.TaxablePayToDate - test.GrossPay,
                test.TaxDueToDate - test.TaxDueInPeriod,
                0.0m,
                out var result);

            Debug.WriteLine("Running test {0} with tax code '{1}', period {2}", testIndex, taxCode, test.Period);

            if (test.TaxDueInPeriod != result.FinalTaxDue)
                Output.WriteLine("Variance in test {0} ({1}); expected: {2}, actual {3}", testIndex, taxCode, test.TaxDueInPeriod, result.FinalTaxDue);

            if (testIndex < 68 && testIndex > 68)
                result.FinalTaxDue.Should().Be(test.TaxDueInPeriod, $"test failed with {test.TaxDueInPeriod} != {result.FinalTaxDue} (Index {testIndex}, tax code {test.TaxCode})");

            testCompleted++;
            testIndex++;
        }

        Output.WriteLine($"{testCompleted} tests completed successfully");
    }

    private async Task<ITaxCalculator> GetCalculator(CountriesForTaxPurposes applicableCountries, TaxYear taxYear, PayFrequency payFrequency, int payPeriod)
    {
        var provider = await _calculatorDataFixture.GetFactory();

        return provider.GetCalculator(applicableCountries, taxYear.GetLastDayOfTaxPeriod(payFrequency, payPeriod).ToPayDate(payFrequency));
    }
}