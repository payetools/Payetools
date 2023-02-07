using System.Text;

namespace Paytools.Common;

/// <summary>
/// Represents one or more countries within the United Kingdom for tax purposes.  For example,
/// Scotland has had its own thresholds and rates of income tax since 2016.
/// </summary>
[Flags]
public enum CountriesForTaxPurposes
{
    England = 1,
    NorthernIreland = 2,
    Scotland = 4,
    Wales = 8
}

public static class CountriesForTaxPurposesConverter
{
    private const string _iso3166_England = "GB-ENG";
    private const string _iso3166_NorthernIreland = "GB-NIR";
    private const string _iso3166_Scotland = "GB-SCT";
    private const string _iso3166_Wales = "GB-WLS";

    public static string ToString(CountriesForTaxPurposes countries)
    {
        StringBuilder sb = new StringBuilder();

        if (countries.HasFlag(CountriesForTaxPurposes.England))
            sb.Append($"{_iso3166_England} ");

        if (countries.HasFlag(CountriesForTaxPurposes.NorthernIreland))
            sb.Append($"{_iso3166_NorthernIreland} ");

        if (countries.HasFlag(CountriesForTaxPurposes.Scotland))
            sb.Append($"{_iso3166_Scotland} ");

        if (countries.HasFlag(CountriesForTaxPurposes.Wales))
            sb.Append($"{_iso3166_Wales} ");

        return sb.ToString().TrimEnd();
    }

    public static CountriesForTaxPurposes ToEnum(string? iso3166Countries)
    {
        CountriesForTaxPurposes countries = new();

        if (iso3166Countries != null)
        {
            var applicableCountries = iso3166Countries.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            var selectedCountries = applicableCountries.Select(ac => ac switch
            {
                _iso3166_England => CountriesForTaxPurposes.England,
                _iso3166_NorthernIreland => CountriesForTaxPurposes.NorthernIreland,
                _iso3166_Scotland => CountriesForTaxPurposes.Scotland,
                _iso3166_Wales => CountriesForTaxPurposes.Wales,
                _ => throw new ArgumentException($"Unrecognised country '{ac}'", nameof(iso3166Countries))
            });

            countries |= (CountriesForTaxPurposes)selectedCountries.Sum(sc => (int)sc);
        }

        return countries;
    }
}