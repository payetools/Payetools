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

namespace Paytools.Common.Model;

public record TaxYear
{
    private static readonly CountriesForTaxPurposes _defaultCountriesBefore6Apr2020 =
        CountriesForTaxPurposes.England | CountriesForTaxPurposes.Wales | CountriesForTaxPurposes.NorthernIreland;

    private static readonly CountriesForTaxPurposes _defaultCountriesFrom6Apr2020 =
        CountriesForTaxPurposes.England | CountriesForTaxPurposes.NorthernIreland;

    private static readonly CountriesForTaxPurposes[] _countriesForBefore6Apr2020 = new[]
    {
        _defaultCountriesBefore6Apr2020,
        CountriesForTaxPurposes.Scotland
    };
    private static readonly CountriesForTaxPurposes[] _countriesForFrom6Apr2020 = new[]
    {
        _defaultCountriesFrom6Apr2020,
        CountriesForTaxPurposes.Wales,
        CountriesForTaxPurposes.Scotland
    };

    public TaxYearEnding TaxYearEnding { get; init; }

    public DateOnly StartOfTaxYear { get; init; }
    public DateOnly EndOfTaxYear { get; init; }

    public static TaxYearEnding Current => FromDate(DateOnly.FromDateTime(DateTime.Now));

    public TaxYear(TaxYearEnding taxYearEnding)
    {
        TaxYearEnding = taxYearEnding;
        StartOfTaxYear = new DateOnly((int)TaxYearEnding - 1, 4, 6);
        EndOfTaxYear = new DateOnly((int)TaxYearEnding, 4, 5);
    }

    public TaxYear(DateOnly taxDate)
        : this(FromDate(taxDate))
    {
    }

    public static TaxYearEnding FromDate(DateOnly date)
    {
        var apr6 = new DateOnly(date.Year, date.Month, 6);

        var taxYear = date < apr6 ? date.Year : date.Year + 1;

        if (taxYear < (int)TaxYearEnding.MinValue || taxYear > (int)TaxYearEnding.MaxValue)
            throw new ArgumentOutOfRangeException(nameof(taxYear), $"Unsupported tax year; date must fall within range tax year ending 6 April {(int)TaxYearEnding.MinValue} to 6 April {(int)TaxYearEnding.MaxValue}");

        return (TaxYearEnding)taxYear;
    }

    public CountriesForTaxPurposes[] GetCountriesForYear()
    {
        return TaxYearEnding switch
        {
            TaxYearEnding.Unspecified => throw new InvalidOperationException("Unable to verify countries for uninitialised tax year"),
            TaxYearEnding.Apr5_2019 => _countriesForBefore6Apr2020,
            _ => _countriesForFrom6Apr2020
        };
    }

    public bool IsValidForYear(CountriesForTaxPurposes countries)
    {
        var countriesForYear = GetCountriesForYear();

        return countriesForYear.Where(c => c == countries).Any();
    }

    public int GetTaxPeriod(DateOnly payDate, PayFrequency payFrequency)
    {
        if (payDate < StartOfTaxYear || payDate > EndOfTaxYear)
            throw new ArgumentException($"Pay date of {payDate:d} is outside this tax year {StartOfTaxYear:d} - {EndOfTaxYear:d}", nameof(payDate));

        switch (payFrequency)
        {
            case PayFrequency.Annually:
                return 1;

            case PayFrequency.Monthly:
                var startOfCalendarYear = new DateOnly((int)TaxYearEnding, 1, 1);
                var monthNumber = payDate.Month + (payDate >= startOfCalendarYear && payDate <= EndOfTaxYear ? 12 : 0) - 3;
                var dayOfMonth = payDate.Day;
                return monthNumber - (dayOfMonth >= 1 && dayOfMonth <= 5 ? 1 : 0);

            default:
                var dayNumber = payDate.DayNumber - StartOfTaxYear.DayNumber + 1;
                var dayCountPerPeriod = payFrequency switch
                {
                    PayFrequency.Weekly => 7,
                    PayFrequency.TwoWeekly => 14,
                    PayFrequency.FourWeekly => 28,
                    _ => throw new ArgumentException($"Invalid pay frequency value {payFrequency}", nameof(PayFrequency))
                };
                return (int)Math.Ceiling((float)dayNumber / dayCountPerPeriod);
        }
    }

    public CountriesForTaxPurposes GetDefaultCountriesForYear()
    {
        return TaxYearEnding switch
        {
            TaxYearEnding.Unspecified => throw new InvalidOperationException("Unable to retrieve default countries for uninitialised tax year"),
            TaxYearEnding.Apr5_2019 => _defaultCountriesBefore6Apr2020,
            _ => _defaultCountriesFrom6Apr2020
        };
    }
}