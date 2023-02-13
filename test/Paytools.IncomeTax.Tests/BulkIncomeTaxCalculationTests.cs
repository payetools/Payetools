using Paytools.Common;
using Paytools.IncomeTax.Tests.TestData;
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
    public void RunTests()
    {
        var provider = new TaxBandProvider();

        TaxCalculatorFactory taxCalculatorFactory = new TaxCalculatorFactory(provider);
        var bands = provider.GetBandsForTaxYear(new TaxYear(TaxYearEnding.Apr5_2023));
        var bandwidthSet = bands[CountriesForTaxPurposes.England | CountriesForTaxPurposes.NorthernIreland];
        var tests = IncomeTaxTestDataLoader.Load();

        int testIndex = 1;
        int testCompleted = 0;

        foreach (var test in tests)
        {
            var calculator = taxCalculatorFactory.GetTaxCalculator(bandwidthSet, test.PayFrequency, test.Period);

            var fullTaxCode = string.Format("{0}{1}", test.TaxCode, test.IsNonCumulative ? " X" : "");

            if (!TaxCode.TryParse(fullTaxCode, out var taxCode))
                throw new XunitException($"Unable to parse tax code '{test.TaxCode}'");

            var result = calculator.Calculate(test.GrossPay,
                0.0m,
                (TaxCode)taxCode,
                test.TaxablePayToDate - test.GrossPay,
                test.TaxDueToDate - test.TaxDue);

            Debug.WriteLine("Running test {0} with tax code '{1}', period {2}", testIndex, fullTaxCode, test.Period);

            if (test.TaxDue != result.TaxDue)
                Output.WriteLine("Variance in test {0} ({1}); expected: {2}, actual {3}", testIndex, fullTaxCode, test.TaxDue, result.TaxDue);

            Assert.True(test.TaxDue == result.TaxDue, $"Failed {test.TaxDue} != {result.TaxDue} (Index {testIndex})");

            testCompleted++;
            testIndex++;
        }

        Output.WriteLine($"{testCompleted} tests completed successfully");
    }
}
