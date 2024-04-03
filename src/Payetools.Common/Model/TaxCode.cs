// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Diagnostics;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Payetools.Common.Model;

/// <summary>
/// Represents a UK tax code, with the ability to calculate tax-free pay based the code and the relevant tax period.
/// </summary>
public readonly partial struct TaxCode
{
    // Constants used in the following regular expressions, and in the code for extracting values
    private const string _nonCumulative = "NonCumulative";
    private const string _fixedCode = "FixedCode";
    private const string _countryPrefix = "CountryPrefix";
    private const string _otherPrefix = "OtherPrefix";
    private const string _digits = "Digits";
    private const string _suffix = "Suffix";

#if NET7_0_OR_GREATER

    [GeneratedRegex(@"^[SC]?(?:BR|NT|K|D[0-2]?)?\d*[TLMN]?\s*(?<NonCumulative>W1M1|W1/M1|X|W1|M1)$", RegexOptions.IgnoreCase | RegexOptions.Compiled, "en-GB")]
    private static partial Regex GetNonCumulativeRegex();

    [GeneratedRegex(@"^(?<CountryPrefix>^[SC]?)(?<FixedCode>0T|BR|NT|D0|D1|D2)\s*(?:W1M1|W1/M1|X|W1|M1)?$", RegexOptions.IgnoreCase | RegexOptions.Compiled, "en-GB")]
    private static partial Regex GetFixedCodeRegex();

    [GeneratedRegex(@"(?<CountryPrefix>^[SC]?)(?<OtherPrefix>[K]?)(?<Digits>\d*)(?<Suffix>[LMN]?)\s*(?:W1M1|W1/M1|X|W1|M1)?$", RegexOptions.IgnoreCase | RegexOptions.Compiled, "en-GB")]
    private static partial Regex GetStandardCodeRegex();

#else

    private static readonly Regex _nonCumulativeRegex = new ($@"^[SC]?(?:BR|NT|K|D[0-2]?)?\d*[TLMN]?\s*(?<{_nonCumulative}>W1M1|W1/M1|X|W1|M1)$",
        RegexOptions.IgnoreCase | RegexOptions.Compiled);

    private static readonly Regex _fixedCodeRegex = new ($@"^(?<{_countryPrefix}>^[SC]?)(?<{_fixedCode}>0T|BR|NT|D0|D1|D2)\s*(?:W1M1|W1/M1|X|W1|M1)?$",
        RegexOptions.IgnoreCase | RegexOptions.Compiled);

    private static readonly Regex _standardCodeRegex = new ($@"(?<{_countryPrefix}>^[SC]?)(?<{_otherPrefix}>[K]?)(?<{_digits}>\d*)(?<{_suffix}>[LMN]?)\s*(?:W1M1|W1/M1|X|W1|M1)?$",
        RegexOptions.IgnoreCase | RegexOptions.Compiled);

    private static Regex GetNonCumulativeRegex() => _nonCumulativeRegex;

    private static Regex GetFixedCodeRegex() => _fixedCodeRegex;

    private static Regex GetStandardCodeRegex() => _standardCodeRegex;

#endif

    private static readonly CountriesForTaxPurposes _allCountries = CountriesForTaxPurposes.England | CountriesForTaxPurposes.Wales | CountriesForTaxPurposes.NorthernIreland | CountriesForTaxPurposes.Scotland;

    private static readonly decimal _taxCodeDivisor = 500.0m;
    private static readonly decimal _quotientWeeklyTaxFreePay = decimal.Round(_taxCodeDivisor * 10 / 52, 2, MidpointRounding.ToPositiveInfinity);
    private static readonly decimal _quotientMonthlyTaxFreePay = decimal.Round(_taxCodeDivisor * 10 / 12, 2, MidpointRounding.ToPositiveInfinity);

    /// <summary>
    /// Gets the tax year that this tax code pertains to.
    /// </summary>
    public TaxYear TaxYear { get; }

    /// <summary>
    /// Gets a value indicating whether the tax code is cumulative (e.g., 1257L) or non-cumulative (e.g., 1257L W1/M1).
    /// </summary>
    public bool IsNonCumulative { get; }

    /// <summary>
    /// Gets the tax treatment specified by this tax code.  For the most part, this is the prefix or suffix for the tax code, omitting the
    /// tax regime letter.
    /// </summary>
    public TaxTreatment TaxTreatment { get; }

    /// <summary>
    /// Gets the country or countries that this tax code applies to, i.e., the tax regime.
    /// </summary>
    public CountriesForTaxPurposes ApplicableCountries { get; }

    /// <summary>
    /// Gets the notional annual personal allowance for the tax code.  This may be negative in the event the tax code has a K prefix.
    /// </summary>
    public decimal NotionalAllowance { get; }

    /// <summary>
    /// Gets the integer portion of the tax code, if applicable, or zero otherwise.
    /// </summary>
    public int NumericPortionOfCode { get; }

    /// <summary>
    /// Gets a value indicating whether the tax code is a "fixed code", such as BR, D0, NT, etc., or a variable code, such as 1257L.
    /// </summary>
    public bool IsFixedCode { get; }

    /// <summary>
    /// Gets the tax regime letter, e.g., S for Scotland, C for Wales.  Returns an empty string if no specific regime is applicable.
    /// </summary>
    public string TaxRegimeLetter => MapCountryToTaxRegime(ApplicableCountries);

    /// <summary>
    /// Static method that maps a <see cref="CountriesForTaxPurposes"/> value to the regime prefix letter, for
    /// example Scotland => "S".
    /// </summary>
    /// <param name="countries">Country or countries to be mapped.</param>
    /// <returns>Prefix letter for single country regimes (currently Scotland, Wales); Empty string otherwise.</returns>
    public static string MapCountryToTaxRegime(in CountriesForTaxPurposes countries) =>
        countries switch
        {
            CountriesForTaxPurposes.Scotland => "S",
            CountriesForTaxPurposes.Wales => "C",
            _ => string.Empty
        };

    private string BaseCode =>
        TaxTreatment switch
        {
            TaxTreatment._0T => $"0T",
            TaxTreatment.K => $"K{NumericPortionOfCode}",
            TaxTreatment.L or TaxTreatment.M or TaxTreatment.N => $"{NumericPortionOfCode}{TaxTreatment}",
            _ => $"{TaxTreatment}"
        };

    private TaxCode(
        in TaxYear taxYear,
        in CountriesForTaxPurposes applicableCountries,
        in TaxTreatment taxTreatment,
        in int numericPortionOfCode,
        in bool isNonCumulative,
        in bool isFixedCode = false)
    {
        TaxYear = taxYear;
        ApplicableCountries = applicableCountries;
        TaxTreatment = taxTreatment;
        NumericPortionOfCode = numericPortionOfCode;
        IsNonCumulative = isNonCumulative;
        IsFixedCode = isFixedCode;

        NotionalAllowance = taxTreatment switch
        {
            TaxTreatment.K => -numericPortionOfCode * 10,
            TaxTreatment.NT => decimal.MaxValue,
            _ => numericPortionOfCode * 10
        };
    }

    /// <summary>
    /// Returns the string representation of the tax code including the tax regime letter if applicable, but without
    /// any indication of whether the code is cumulative or non-cumulative.
    /// </summary>
    /// <returns>String representation of tax code with the tax regime prefix.</returns>
    public override string ToString() => $"{TaxRegimeLetter}{BaseCode}";

    /// <summary>
    /// Returns the string representation of the tax code optional including the tax regime letter if applicable, and optionally
    /// indicating whether the code is cumulative or non-cumulative by means of an "X" suffix.
    /// </summary>
    /// <param name="includeNonCumulativeFlag">True to include the non-cumulative flag; false otherwise.</param>
    /// <param name="includeTaxRegime">True to include the tax regime prefix; false otherwise.</param>
    /// <returns>String representation of tax code with or without tax regime prefix and with or without non-cumulative indicator.</returns>
    public string ToString(
        in bool includeNonCumulativeFlag,
        in bool includeTaxRegime) =>
        (includeNonCumulativeFlag, includeTaxRegime) switch
        {
            (false, true) => ToString(),
            (true, true) => IsNonCumulative ? $"{ToString()} X" : ToString(),
            (false, false) => BaseCode,
            (true, false) => IsNonCumulative ? $"{BaseCode} X" : BaseCode
        };

    /// <summary>
    /// Attempts to parse the supplied tax code into its component parts, assuming the tax regimes for the current tax year.
    /// Non-cumulative codes must be identified by an 'X', 'W1', 'M1' or 'W1/M1' suffix, with or without preceding space.
    /// Tax code parsing is case-insensitive.
    /// </summary>
    /// <param name="taxCode">Tax code as a string.</param>
    /// <param name="result">Instance of <see cref="TaxCode"/> if valid; default(TaxCode) otherwise.</param>
    /// <returns>True if the tax code could be parsed; false otherwise.</returns>
    public static bool TryParse(in string taxCode, out TaxCode result) =>
        TryParse(taxCode, new TaxYear(TaxYear.Current), out result);

    /// <summary>
    /// Attempts to parse the supplied tax code.  Non-cumulative codes must be identified by an 'X', 'W1', 'M1' or 'W1/M1' suffix,
    /// with or without preceding space.  Tax code parsing is case-insensitive.
    /// </summary>
    /// <param name="taxCode">Tax code as a string.</param>
    /// <param name="taxYear">Tax year for the supplied tax code.</param>
    /// <param name="result">Instance of <see cref="TaxCode"/> if valid; default(TaxCode) otherwise.</param>
    /// <returns>True if the tax code could be parsed; false otherwise.</returns>
    public static bool TryParse(in string taxCode, in TaxYear taxYear, out TaxCode result)
    {
        var isNonCumulative = IsNonCumulativeCode(taxCode);

        var fixedCodeMatch = GetFixedCodeRegex().Match(taxCode);

        Match? standardCodeMatch = fixedCodeMatch.Success ? null : GetStandardCodeRegex().Match(taxCode);

        TaxCode? taxCodeResult = null;

        var success = (fixedCodeMatch.Success, standardCodeMatch) switch
        {
            (true, null) => ProcessFixedCodeMatch(fixedCodeMatch, taxYear, isNonCumulative, out taxCodeResult),
            (false, not null) => standardCodeMatch.Success && ProcessStandardCodeMatch(standardCodeMatch, taxYear, isNonCumulative, out taxCodeResult),
            _ => false
        };

        result = taxCodeResult ?? default;

        return success;
    }

    /// <summary>
    /// Calculates the tax free pay for the specified tax period and given tax code.
    /// </summary>
    /// <param name="taxPeriod">Tax period.</param>
    /// <param name="periodCount">Number of tax periods in the year (e.g., 12 for monthly pay).</param>
    /// <returns>Tax-free pay applicable up to and including the end of the specified tax period.  May be negative.</returns>
    public decimal GetTaxFreePayForPeriod(in int taxPeriod, in int periodCount)
    {
        // Calculating the tax free pay for a period involves deriving the tax free pay for the year and then multiplying by
        // the applicable fraction of the year.  The first of these steps is somewhat counter-intuitive however, for historical
        // reasons to do with the use of tax tables.
        if (NumericPortionOfCode == 0)
            return 0.0m;

        // The following maths requires everything to be a decimal
        decimal numericPortionOfCode = NumericPortionOfCode;
        decimal remainder;
        decimal quotient;

        if (numericPortionOfCode > _taxCodeDivisor)
        {
            remainder = numericPortionOfCode % _taxCodeDivisor;
            quotient = (numericPortionOfCode - remainder) / _taxCodeDivisor;

            if (remainder == 0)
            {
                quotient--;
                remainder = 500.0m;
            }
        }
        else
        {
            remainder = numericPortionOfCode;
            quotient = 0.0m;
        }

        var taxFreePayForOnePeriod = (quotient * GetQuotientTaxFreePay(periodCount)) +
            decimal.Round(decimal.Round(((remainder * 10.0m) + 9.0m) / periodCount, 4, MidpointRounding.ToZero), 2, MidpointRounding.ToPositiveInfinity);

        if (TaxTreatment == TaxTreatment.K)
            taxFreePayForOnePeriod = -taxFreePayForOnePeriod;

        decimal taxFreePayForPeriod = IsNonCumulative ? taxFreePayForOnePeriod : taxFreePayForOnePeriod * taxPeriod;

        Debug.WriteLine(
            "Calculating tax free pay for period {0} (annual periods: {1}) for tax code {2}\n   allowanceForOnePeriod = {3}, taxFreePayForPeriod = {4}",
            taxPeriod,
            periodCount,
            this,
            taxFreePayForOnePeriod,
            taxFreePayForPeriod);

        return taxFreePayForPeriod;
    }

    private static decimal GetQuotientTaxFreePay(in int periodCount) =>
        periodCount switch
        {
            52 => _quotientWeeklyTaxFreePay,
            26 => _quotientWeeklyTaxFreePay * 2,
            13 => _quotientWeeklyTaxFreePay * 4,
            12 => _quotientMonthlyTaxFreePay,
            1 => _taxCodeDivisor,
            _ => throw new ArgumentOutOfRangeException(nameof(periodCount), $"Unsupported value for periodCount: {periodCount}")
        };

    private static bool ProcessFixedCodeMatch(
        in Match match,
        in TaxYear taxYear,
        in bool isNonCumulative,
        out TaxCode? taxCode)
    {
        taxCode = null;

        var fixedCode = match.Groups[_fixedCode];

        if (fixedCode == null)
            return false;

        var code = fixedCode.Value.ToUpperInvariant();

        TaxTreatment treatment = code switch
        {
            "BR" => TaxTreatment.BR,
            "D0" => TaxTreatment.D0,
            "D1" => TaxTreatment.D1,
            "D2" => TaxTreatment.D2,
            "0T" => TaxTreatment._0T,
            "NT" => TaxTreatment.NT,
            _ => TaxTreatment.Unspecified
        };

        if (treatment == TaxTreatment.Unspecified)
            return false;

        var countries = treatment == TaxTreatment.NT ? _allCountries : GetApplicableCountries(match, taxYear);
        var allowance = treatment == TaxTreatment.NT ? int.MaxValue : 0;

        taxCode = new TaxCode(taxYear, countries, treatment, allowance, isNonCumulative, true);

        return true;
    }

    private static bool ProcessStandardCodeMatch(
        in Match match,
        in TaxYear taxYear,
        in bool isNonCumulative,
        out TaxCode? taxCode)
    {
        taxCode = null;

        Group otherPrefix = match.Groups[_otherPrefix];
        Group digits = match.Groups[_digits];
        Group suffix = match.Groups[_suffix];

        if (otherPrefix == null || digits == null || suffix == null)
            return false;

        CountriesForTaxPurposes countries = GetApplicableCountries(match, taxYear);

        if (!int.TryParse(digits.Value, out var numericPortion))
            return false;

        TaxTreatment treatment = (ToChar(otherPrefix.Value), ToChar(suffix.Value)) switch
        {
            ('K', null) => TaxTreatment.K,
            (null, 'L') => TaxTreatment.L,
            (null, 'M') => TaxTreatment.M,
            (null, 'N') => TaxTreatment.N,
            _ => TaxTreatment.Unspecified
        };

        if (treatment == TaxTreatment.Unspecified)
            return false;

        taxCode = new TaxCode(taxYear, countries, treatment, GetNumericPortionOfCode(numericPortion, treatment), isNonCumulative);

        return true;
    }

    private static int GetNumericPortionOfCode(in int numericPortionOfCode, in TaxTreatment treatment) =>
        treatment switch
        {
            TaxTreatment.K or TaxTreatment.L or TaxTreatment.M or TaxTreatment.N => numericPortionOfCode,
            TaxTreatment.NT => int.MaxValue,
            _ => 0
        };

    private static CountriesForTaxPurposes GetApplicableCountries(in Match match, in TaxYear taxYear)
    {
        var countryCode = match.Groups[_countryPrefix] ??
            throw new InvalidOperationException($"{_countryPrefix} not found in matched output");

        CountriesForTaxPurposes countries = ToChar(countryCode.Value) switch
        {
            'S' => CountriesForTaxPurposes.Scotland,
            'C' => CountriesForTaxPurposes.Wales,
            _ => taxYear.GetDefaultCountriesForYear()
        };

        return taxYear.IsValidForYear(countries) ? countries :
            throw new InconsistentDataException("Country-specific tax code supplied but country not valid for tax year");
    }

    private static bool IsNonCumulativeCode(in string taxCode)
    {
        var match = GetNonCumulativeRegex().Match(taxCode);

        return match.Success && !string.IsNullOrEmpty(match.Groups[_nonCumulative]?.Value);
    }

    private static char? ToChar(in string s) => s.Length == 1 ? s.ToUpperInvariant()[0] : null;
}