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

using System.Text;

namespace Paytools.Common.Model;

/// <summary>
/// Represents one or more countries within the United Kingdom for tax purposes.  For example,
/// Scotland has had its own thresholds and rates of income tax since 2016.  Note that this enum is marked with
/// the [Flags} attribute as it is possible to combine countries for situations where the same set of tax
/// parameters applies to more than one country, e.g. England and Northern Ireland.
/// </summary>
[Flags]
public enum CountriesForTaxPurposes
{
    /// <summary>England</summary>
    England = 1,

    /// <summary>Northern Ireland</summary>
    NorthernIreland = 2,

    /// <summary>Scotland</summary>
    Scotland = 4,

    /// <summary>Wales</summary>
    Wales = 8
}

/// <summary>
/// Converter that translates between the string format of countries based on ISO-3166 and <see cref="CountriesForTaxPurposes"/> enum values.
/// </summary>
public static class CountriesForTaxPurposesConverter
{
    private const string Iso3166_England = "GB-ENG";
    private const string Iso3166_NorthernIreland = "GB-NIR";
    private const string Iso3166_Scotland = "GB-SCT";
    private const string Iso3166_Wales = "GB-WLS";

    /// <summary>
    /// Gets the ISO-3166 sub-entity for the supplied country or countries enum value.
    /// </summary>
    /// <param name="countries">Instance of <see cref="CountriesForTaxPurposes"/> specifying one or more countries with the UK.</param>
    /// <returns>Space separated ISO-3166 countries list, e.g., "GB-ENG GB-NIR".</returns>
    public static string ToString(CountriesForTaxPurposes countries)
    {
        var sb = new StringBuilder();

        if (countries.HasFlag(CountriesForTaxPurposes.England))
            sb.Append($"{Iso3166_England} ");

        if (countries.HasFlag(CountriesForTaxPurposes.NorthernIreland))
            sb.Append($"{Iso3166_NorthernIreland} ");

        if (countries.HasFlag(CountriesForTaxPurposes.Scotland))
            sb.Append($"{Iso3166_Scotland} ");

        if (countries.HasFlag(CountriesForTaxPurposes.Wales))
            sb.Append($"{Iso3166_Wales} ");

        return sb.ToString().TrimEnd();
    }

    /// <summary>
    /// Gets the <see cref="CountriesForTaxPurposes"/> enum value for the supplied country or space separated list of
    /// ISO-3166 countries, e.g., "GB-ENG GB-NIR".
    /// </summary>
    /// <param name="iso3166Countries">Space separated list of ISO-3166 countries.</param>
    /// <returns>Equivalent CountriesForTaxPurposes enum value.</returns>
    /// <exception cref="ArgumentException">Thrown if an invalid country value is supplied.</exception>
    public static CountriesForTaxPurposes ToEnum(string? iso3166Countries)
    {
        CountriesForTaxPurposes countries = 0;

        if (iso3166Countries != null)
        {
            var applicableCountries = iso3166Countries.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            var selectedCountries = applicableCountries.Select(ac => ac switch
            {
                Iso3166_England => CountriesForTaxPurposes.England,
                Iso3166_NorthernIreland => CountriesForTaxPurposes.NorthernIreland,
                Iso3166_Scotland => CountriesForTaxPurposes.Scotland,
                Iso3166_Wales => CountriesForTaxPurposes.Wales,
                _ => throw new ArgumentException($"Unrecognised country '{ac}'", nameof(iso3166Countries))
            });

            countries |= (CountriesForTaxPurposes)selectedCountries.Sum(sc => (int)sc);
        }

        return countries;
    }
}