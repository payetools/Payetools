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
using Paytools.IncomeTax.ReferenceData;
using Paytools.Testing.Data;
using Paytools.Testing.Data.IncomeTax;
using Paytools.Testing.Utils;
using System.Diagnostics;
using Xunit.Abstractions;

namespace Paytools.IncomeTax.Tests;

public class BulkIncomeTaxCalculationTests
{
    private readonly ITestOutputHelper Output;

    public BulkIncomeTaxCalculationTests(ITestOutputHelper output)
    {
        Output = output;

        if (!Trace.Listeners.Contains(new TestOutputTraceListener(output)))
            Trace.Listeners.Add(new TestOutputTraceListener(output));
    }

    [Fact]
    public async Task RunTests()
    {
        var provider = await
            Testing.Utils.ReferenceDataHelper.CreateProviderAsync<ITaxReferenceDataProvider>(new Stream[] { Resource.Load(@"ReferenceData\IncomeTax_2022_2023.json") });

        var taxCalculatorFactory = new TaxCalculatorFactory(provider);

        using var db = new TestDataRepository("Income Tax", Output);

        var testData = db.GetTestData<IHmrcIncomeTaxTestDataEntry>(TestSource.Hmrc, TestScope.IncomeTax);

        if (!testData.Any())
            Assert.Fail("No National Insurance tests found");

        Console.WriteLine($"{testData.Count()} tests found");
        Output.WriteLine($"{testData.Count()} tests found");

        int testIndex = 1;
        int testCompleted = 0;

        foreach (var test in testData)
        {
            var taxYear = new TaxYear(test.TaxYearEnding);
            var taxCode = test.GetFullTaxCode(taxYear);

            var applicableCountries = CountriesForTaxPurposesConverter.ToEnum(test.RelatesTo);

            var calculator = taxCalculatorFactory.GetCalculator(applicableCountries, taxYear, test.PayFrequency, test.Period);

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

            result.FinalTaxDue.Should().Be(test.TaxDueInPeriod, $"test failed with {test.TaxDueInPeriod} != {result.FinalTaxDue} (Index {testIndex}, tax code {test.TaxCode})");

            testCompleted++;
            testIndex++;
        }

        Output.WriteLine($"{testCompleted} tests completed successfully");
    }

}
