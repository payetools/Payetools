using Paytools.Common;
using Paytools.Common.Model;
using Paytools.Common.Serialization;
using Paytools.IncomeTax.ReferenceData;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace Paytools.ReferenceData;

public class TaxBandProvider : ITaxBandProvider
{
    public ReadOnlyDictionary<CountriesForTaxPurposes, TaxBandwidthSet> GetBandsForTaxYear(TaxYear taxYear)
    {
        int year = (int)taxYear.TaxYearEnding;

        var json = File.ReadAllText($@"C:\Users\Paytools\Dropbox\Output\TaxBands\taxbands_year_ending_Apr5_{year}.json");

        var taxBands = JsonSerializer.Deserialize<TaxBandSet>(json, new JsonSerializerOptions()
        {
            // See https://github.com/dotnet/runtime/issues/31081 on why we can't just use JsonStringEnumConverter
            Converters =
            {
                new PayFrequencyJsonConverter(),
                new CountriesForTaxPurposesJsonConverter(),
                new TaxYearEndingJsonConverter()
            },
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        if (taxBands == null || taxBands.TaxYearEntries == null)
            throw new ArgumentException($"Unable to retrieve tax band(s) for tax year ending 5 April {year}", nameof(taxYear));

        return new ReadOnlyDictionary<CountriesForTaxPurposes, TaxBandwidthSet>(taxBands.TaxYearEntries
            .ToDictionary(e => e.ApplicableCountries, e => new TaxBandwidthSet(e)));
    }
}