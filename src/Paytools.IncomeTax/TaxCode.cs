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

using Paytools.Common;
using Paytools.Common.Diagnostics;
using Paytools.Common.Model;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace Paytools.IncomeTax;

public readonly struct TaxCode
{
    private const string _nonCumulative = "NonCumulative";
    private const string _fixedCode = "FixedCode";
    private const string _countryPrefix = "CountryPrefix";
    private const string _otherPrefix = "OtherPrefix";
    private const string _digits = "Digits";
    private const string _suffix = "Suffix";

    private static readonly Regex _nonCumulativeRegex = new($@"^[SC]?(?:BR|NT|K|D[0-2]?)?\d*[TLMN]?\s*(?<{_nonCumulative}>W1M1|W1/M1|X|W1|M1)$",
        RegexOptions.IgnoreCase | RegexOptions.Compiled);
    private static readonly Regex _fixedCodeRegex = new($@"^(?<{_countryPrefix}>^[SC]?)(?<{_fixedCode}>0T|BR|NT|D0|D1|D2)\s*(?:W1M1|W1/M1|X|W1|M1)?$",
        RegexOptions.IgnoreCase | RegexOptions.Compiled);
    private static readonly Regex _standardCodeRegex = new($@"(?<{_countryPrefix}>^[SC]?)(?<{_otherPrefix}>[K]?)(?<{_digits}>\d*)(?<{_suffix}>[LMN]?)\s*(?:W1M1|W1/M1|X|W1|M1)?$",
        RegexOptions.IgnoreCase | RegexOptions.Compiled);
    private static readonly CountriesForTaxPurposes _allCountries = CountriesForTaxPurposes.England | CountriesForTaxPurposes.Wales | CountriesForTaxPurposes.NorthernIreland | CountriesForTaxPurposes.Scotland;

    private static readonly decimal _taxCodeDivisor = 500.0m;
    private static readonly decimal _quotientWeeklyTaxFreePay = decimal.Round(_taxCodeDivisor * 10 / 52, 2, MidpointRounding.ToPositiveInfinity);
    private static readonly decimal _quotientMonthlyTaxFreePay = decimal.Round(_taxCodeDivisor * 10 / 12, 2, MidpointRounding.ToPositiveInfinity);

    private readonly TaxYear _taxYear;

    public bool IsNonCumulative { get; }
    public TaxTreatment TaxTreatment { get; }
    public CountriesForTaxPurposes ApplicableCountries { get; }
    public decimal NotionalAllowance { get; }
    public int NumericPortionOfCode { get; }
    public bool IsFixedCode { get; }
    public string TaxRegime =>
        ApplicableCountries switch
        {
            CountriesForTaxPurposes.Scotland => "S",
            CountriesForTaxPurposes.Wales => "C",
            _ => ""
        };

    private string BaseCode =>
        TaxTreatment switch
        {
            TaxTreatment._0T => $"0T",
            TaxTreatment.K => $"K{-NumericPortionOfCode}",
            TaxTreatment.L or TaxTreatment.M or TaxTreatment.N => $"{NumericPortionOfCode}{TaxTreatment}",
            _ => $"{TaxTreatment}"
        };

    private TaxCode(TaxYear taxYear,
        CountriesForTaxPurposes applicableCountries,
        TaxTreatment taxTreatment,
        int numericPortionOfCode,
        bool isNonCumulative,
        bool isFixedCode = false)
    {
        _taxYear = taxYear;
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
    /// 
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return $"{TaxRegime}{BaseCode}";
    }


    public string ToString(bool includeNonCumulativeFlag, bool includeTaxRegime)
    {
        return (includeNonCumulativeFlag, includeTaxRegime) switch
        {
            (false, true) => ToString(),
            (true, true) => $"{ToString()} X",
            (false, false) => BaseCode,
            (true, false) => $"{BaseCode} X",
        };
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="taxCode"></param>
    /// <param name="result"></param>
    /// <returns>True if the tax code could be parsed; false otherwise.</returns>
    public static bool TryParse(string taxCode, [NotNullWhen(true)] out TaxCode? result)
    {
        return TryParse(taxCode, new(TaxYear.Current), out result);
    }

    /// <summary>
    /// Attempts to parse the supplied tax code.  Non-cumulative codes must be identified by an 'X', 'W1', 'M1' or 'W1/M1' suffix,
    /// with or without preceding space.  Tax code parsing is case-insensitive.
    /// </summary>
    /// <param name="taxCode"></param>
    /// <param name="taxYear"></param>
    /// <param name="result"></param>
    /// <returns>True if the tax code could be parsed; false otherwise.</returns>
    public static bool TryParse(string taxCode, TaxYear taxYear, [NotNullWhen(true)] out TaxCode? result)
    {
        var isNonCumulative = IsNonCumulativeCode(taxCode);

        var fixedCodeMatch = _fixedCodeRegex.Match(taxCode);

        Match? standardCodeMatch = fixedCodeMatch.Success ? null : _standardCodeRegex.Match(taxCode);

        result = (fixedCodeMatch.Success, standardCodeMatch) switch
        {
            (true, null) => ProcessFixedCodeMatch(fixedCodeMatch, taxYear, isNonCumulative),
            (false, not null) => standardCodeMatch.Success ? ProcessStandardCodeMatch(standardCodeMatch, taxYear, isNonCumulative) : null,
            _ => null
        };

        return result != null && result?.Validate() == true;
    }

    public decimal GetTaxFreePayForPeriod(int taxPeriod, int periodCount)
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

        var taxFreePayForOnePeriod = quotient * GetQuotientTaxFreePay(periodCount) +
            decimal.Round(decimal.Round(((remainder * 10.0m) + 9.0m) / periodCount, 4, MidpointRounding.ToZero), 2, MidpointRounding.ToPositiveInfinity);

        if (TaxTreatment == TaxTreatment.K)
            taxFreePayForOnePeriod = -taxFreePayForOnePeriod;

        decimal taxFreePayForPeriod = IsNonCumulative ? taxFreePayForOnePeriod : taxFreePayForOnePeriod * taxPeriod;

        Debug.WriteLine("Calculating tax free pay for period {0} (annual periods: {1}) for tax code {2}\n   allowanceForOnePeriod = {3}, taxFreePayForPeriod = {4}",
            taxPeriod, periodCount, this, taxFreePayForOnePeriod, taxFreePayForPeriod);

        return taxFreePayForPeriod;
    }

    private static decimal GetQuotientTaxFreePay(int periodCount)
    {
        return periodCount switch
        {
            52 => _quotientWeeklyTaxFreePay,
            26 => _quotientWeeklyTaxFreePay * 2,
            13 => _quotientWeeklyTaxFreePay * 4,
            12 => _quotientMonthlyTaxFreePay,
            1 => _taxCodeDivisor,
            _ => throw new ArgumentOutOfRangeException(nameof(periodCount), $"Unsupported value for periodCount: {periodCount}")
        };
    }

    public bool Validate()
    {
        // TODO: Add validation
        return true;
    }

    private static TaxCode? ProcessFixedCodeMatch(Match match, TaxYear taxYear, bool isNonCumulative)
    {
        var fixedCode = match.Groups[_fixedCode] ??
            throw new InvalidOperationException($"{_fixedCode} not found in matched output");

        var code = fixedCode.Value.ToUpper();

        TaxTreatment treatment = code switch
        {
            "BR" => TaxTreatment.BR,
            "D0" => TaxTreatment.D0,
            "D1" => TaxTreatment.D1,
            "D2" => TaxTreatment.D2,
            "0T" => TaxTreatment._0T,
            "NT" => TaxTreatment.NT,
            _ => throw new InvalidOperationException($"Unrecognised fixed tax code value '{fixedCode.Value}'")
        };

        var countries = treatment == TaxTreatment.NT ? _allCountries : GetApplicableCountries(match, taxYear);
        var allowance = treatment == TaxTreatment.NT ? int.MaxValue : 0;

        return new TaxCode(taxYear, countries, treatment, allowance, isNonCumulative, true);
    }

    private static TaxCode? ProcessStandardCodeMatch(Match match, TaxYear taxYear, bool isNonCumulative)
    {
        Group otherPrefix = match.Groups[_otherPrefix] ??
            throw new InvalidOperationException($"{_otherPrefix} not found in matched output");
        Group digits = match.Groups[_digits] ??
            throw new InvalidOperationException($"{_digits} not found in matched output");
        Group suffix = match.Groups[_suffix] ??
            throw new InvalidOperationException($"{_suffix} not found in matched output");

        CountriesForTaxPurposes countries = GetApplicableCountries(match, taxYear);

        if (!int.TryParse(digits.Value, out var numericPortion))
            return null;

        TaxTreatment treatment = (ToChar(otherPrefix.Value), ToChar(suffix.Value)) switch
        {
            ('K', null) => TaxTreatment.K,
            (null, 'L') => TaxTreatment.L,
            (null, 'M') => TaxTreatment.M,
            (null, 'N') => TaxTreatment.N,
            _ => TaxTreatment.Unspecified
        };

        return treatment != TaxTreatment.Unspecified ?
            new TaxCode(taxYear, countries, treatment, GetNumericPortionOfCode(numericPortion, treatment), isNonCumulative) :
            null;
    }

    private static int GetNumericPortionOfCode(int numericPortionOfCode, TaxTreatment treatment)
    {
        return treatment switch
        {
            TaxTreatment.K or TaxTreatment.L or TaxTreatment.M or TaxTreatment.N => numericPortionOfCode,
            TaxTreatment.NT => int.MaxValue,
            _ => 0
        };
    }

    private static CountriesForTaxPurposes GetApplicableCountries(Match match, TaxYear taxYear)
    {
        var countryCode = match.Groups[_countryPrefix] ??
            throw new InvalidOperationException($"{_countryPrefix} not found in matched output");

        var applicableCountries = taxYear.GetCountriesForYear();

        CountriesForTaxPurposes countries = ToChar(countryCode.Value) switch
        {
            'S' => CountriesForTaxPurposes.Scotland,
            'C' => CountriesForTaxPurposes.Wales,
            _ => taxYear.GetDefaultCountriesForYear()
        };

        return taxYear.IsValidForYear(countries) ? countries :
            throw new InconsistentDataException("Country-specific tax code supplied but country not valid for tax year");
    }

    private static bool IsNonCumulativeCode(string taxCode)
    {
        var match = _nonCumulativeRegex.Match(taxCode);

        return match.Success && !string.IsNullOrEmpty(match.Groups[_nonCumulative]?.Value);
    }

    private static char? ToChar(string s) => s.Length == 1 ? s.ToUpper()[0] : null;
}