// Copyright (c) 2023 Paytools Foundation
//
// Licensed under the Apache License, Version 2.0 (the "License")~
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
using Paytools.IncomeTax.Tests.TestData;
using Paytools.ReferenceData;
using System.Diagnostics;
using Xunit.Abstractions;
using Xunit.Sdk;

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
        var taxYear = new TaxYear(TaxYearEnding.Apr5_2023);
        TaxBandProvider provider = await TaxBandProvider.GetTaxBandProvider("https://stellular-bombolone-34e67e.netlify.app/hmrc.json");

        var taxCalculatorFactory = new TaxCalculatorFactory(provider);
        var tests = IncomeTaxTestDataLoader.Load();

        int testIndex = 1;
        int testCompleted = 0;

        foreach (var test in tests)
        {
            var fullTaxCode = string.Format("{0}{1}", test.TaxCode, test.IsNonCumulative ? " X" : "");

            if (!TaxCode.TryParse(fullTaxCode, out var taxCode))
                throw new XunitException($"Unable to parse tax code '{test.TaxCode}'");

            var applicableCountries = taxCode.TaxTreatment == TaxTreatment.NT ?
                taxYear.GetDefaultCountriesForYear() : taxCode.ApplicableCountries;

            var calculator = taxCalculatorFactory.GetCalculator(applicableCountries, taxYear, test.Period, test.PayFrequency);

            var result = calculator.Calculate(test.GrossPay,
                0.0m,
                (TaxCode)taxCode,
                test.TaxablePayToDate - test.GrossPay,
                test.TaxDueToDate - test.TaxDue);

            Debug.WriteLine("Running test {0} with tax code '{1}', period {2}", testIndex, fullTaxCode, test.Period);

            if (test.TaxDue != result.TaxDue)
                Output.WriteLine("Variance in test {0} ({1}); expected: {2}, actual {3}", testIndex, fullTaxCode, test.TaxDue, result.TaxDue);

            result.TaxDue.Should().Be(test.TaxDue, $"Failure {test.TaxDue} != {result.TaxDue} (Index {testIndex})");

            testCompleted++;
            testIndex++;
        }

        Output.WriteLine($"{testCompleted} tests completed successfully");
    }
}
