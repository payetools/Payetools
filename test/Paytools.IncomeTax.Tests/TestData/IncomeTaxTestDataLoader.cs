using Paytools.Common.Serialization;
using System.Text.Json;

namespace Paytools.IncomeTax.Tests.TestData
{
    internal class IncomeTaxTestDataLoader
    {
        public static List<IncomeTaxTestEntry> Load()
        {
            var json = File.ReadAllText(@"TestData\income-tax-tests-2022-23-eng-nir.json");

            var testEntries = JsonSerializer.Deserialize<List<IncomeTaxTestEntry>>(json, new JsonSerializerOptions()
            {
                // See https://github.com/dotnet/runtime/issues/31081 on why we can't just use JsonStringEnumConverter
                Converters =
            {
                new PayFrequencyJsonConverter()
                //new JsonCountriesForTaxPurposesConverter(),
                //new JsonTaxYearEndingConverter()
            },
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            return testEntries ?? throw new InvalidOperationException("Unable to load test data");
        }
    }
}
