// Copyright (c) 2023 Payetools Foundation.
//
// Payetools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Payetools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

using Payetools.Common.Extensions;

namespace Payetools.Common.Model;

/// <summary>
/// Represents a given UK tax year, running from 6th April of a given year through to 5th April the following
/// year.
/// </summary>
/// <remarks><see cref="TaxYear"/> provides utility methods to access the relevant tax regimes (i.e., sub-countries
/// within the UK) which have changed over the period 2018 to date.  TaxYear also provides conversions from dates
/// to tax periods, based on payment frequency.</remarks>
public record TaxYear
{
    private static readonly CountriesForTaxPurposes DefaultCountriesBefore6Apr2020 =
        CountriesForTaxPurposes.England | CountriesForTaxPurposes.Wales | CountriesForTaxPurposes.NorthernIreland;

    private static readonly CountriesForTaxPurposes DefaultCountriesFrom6Apr2020 =
        CountriesForTaxPurposes.England | CountriesForTaxPurposes.NorthernIreland;

    private static readonly CountriesForTaxPurposes[] CountriesForBefore6Apr2020 = new[]
    {
        DefaultCountriesBefore6Apr2020,
        CountriesForTaxPurposes.Scotland
    };

    private static readonly CountriesForTaxPurposes[] CountriesForFrom6Apr2020 = new[]
    {
        DefaultCountriesFrom6Apr2020,
        CountriesForTaxPurposes.Wales,
        CountriesForTaxPurposes.Scotland
    };

    /// <summary>
    /// Gets the <see cref="TaxYearEnding"/> enumeration value for this <see cref="TaxYear"/>.
    /// </summary>
    public TaxYearEnding TaxYearEnding { get; init; }

    /// <summary>
    /// Gets the date of the start of this tax year.
    /// </summary>
    public DateOnly StartOfTaxYear { get; init; }

    /// <summary>
    /// Gets the date of the end of this tax year.
    /// </summary>
    public DateOnly EndOfTaxYear { get; init; }

    /// <summary>
    /// Gets the <see cref="TaxYearEnding"/> for the "current" tax year (based on today's date).
    /// </summary>
    public static TaxYearEnding Current => DateOnly.FromDateTime(DateTime.Now).ToTaxYearEnding();

    /// <summary>
    /// Initialises a new instance of <see cref="TaxYear"/> based on the supplied <see cref="TaxYearEnding"/> value.
    /// </summary>
    /// <param name="taxYearEnding">TaxYearEnding enum value for this tax year.</param>
    public TaxYear(TaxYearEnding taxYearEnding)
    {
        TaxYearEnding = taxYearEnding;
        StartOfTaxYear = new DateOnly((int)TaxYearEnding - 1, 4, 6);
        EndOfTaxYear = new DateOnly((int)TaxYearEnding, 4, 5);
    }

    /// <summary>
    /// Initialises a new instance of <see cref="TaxYear"/> based on the supplied date.
    /// </summary>
    /// <param name="taxDate">Date to create <see cref="TaxYear"/> for.</param>
    public TaxYear(DateOnly taxDate)
        : this(taxDate.ToTaxYearEnding())
    {
    }

    /// <summary>
    /// Gets the list (as an array) of the country groupings that are applicable for a given tax year.
    /// </summary>
    /// <returns>Array of <see cref="CountriesForTaxPurposes"/> values listing the tax regime groupings for this
    /// tax year.</returns>
    /// <exception cref="InvalidOperationException">Thrown if an invalid/unspecified tax year is provided.</exception>
    public CountriesForTaxPurposes[] GetCountriesForYear()
    {
        return TaxYearEnding switch
        {
            TaxYearEnding.Unspecified => throw new InvalidOperationException("Unable to verify countries for unspecified tax year"),
            TaxYearEnding.Apr5_2019 => CountriesForBefore6Apr2020,
            _ => CountriesForFrom6Apr2020
        };
    }

    /// <summary>
    /// Determines whether the supplied country groupings is valid for this tax year.
    /// </summary>
    /// <param name="countries">One or more <see cref="CountriesForTaxPurposes"/> values.</param>
    /// <returns>True if the supplied countries parameter is valid for this tax year; false otherwise.</returns>
    public bool IsValidForYear(CountriesForTaxPurposes countries)
    {
        var countriesForYear = GetCountriesForYear();

        return countriesForYear.Where(c => c == countries).Any();
    }

    /// <summary>
    /// Gets the tax period for the supplied pay date and payment frequencey.  For example, if the pay frequency
    /// is monthly and the pay date is, say, 20th May, then the tax pariod is 2, as in "Month 2".
    /// </summary>
    /// <param name="payDate">Pay date to determine tax period for.</param>
    /// <param name="payFrequency">Payment frequency applicable.</param>
    /// <returns>Relevant tax period.</returns>
    /// <exception cref="ArgumentException">Thrown if the pay date falls outside this tax year.</exception>
    public int GetTaxPeriod(DateOnly payDate, PayFrequency payFrequency)
    {
        if (payDate < StartOfTaxYear || payDate > EndOfTaxYear)
            throw new ArgumentException($"Pay date of {payDate.ToUk()} is outside this tax year {StartOfTaxYear.ToUk()} - {EndOfTaxYear.ToUk()}", nameof(payDate));

        switch (payFrequency)
        {
            case PayFrequency.Annually:
                return 1;

            case PayFrequency.Monthly:
                return GetMonthNumber(payDate);

            case PayFrequency.Quarterly:
                return ((GetMonthNumber(payDate) - 1) / 3) + 1;

            case PayFrequency.BiAnnually:
                return ((GetMonthNumber(payDate) - 1) / 6) + 1;

            default:
                var dayNumber = payDate.DayNumber - StartOfTaxYear.DayNumber + 1;
                var dayCountPerPeriod = payFrequency switch
                {
                    PayFrequency.Weekly => 7,
                    PayFrequency.TwoWeekly => 14,
                    PayFrequency.FourWeekly => 28,
                    _ => throw new ArgumentException($"Invalid pay frequency value {payFrequency}", nameof(payFrequency))
                };
                return (int)Math.Ceiling((float)dayNumber / dayCountPerPeriod);
        }
    }

    /// <summary>
    /// Gets the 'default' tax regime for this tax year as an <see cref="CountriesForTaxPurposes"/> value.  This is the
    /// regime that all other tax treatments fall into if they are not in a specific regime, e.g., Scotland.
    /// </summary>
    /// <returns>Default tax regime for this tax year.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the tax year is invalid or has not been specified.</exception>
    public CountriesForTaxPurposes GetDefaultCountriesForYear()
    {
        return TaxYearEnding switch
        {
            TaxYearEnding.Unspecified => throw new InvalidOperationException("Unable to retrieve default countries for uninitialised tax year"),
            TaxYearEnding.Apr5_2019 => DefaultCountriesBefore6Apr2020,
            _ => DefaultCountriesFrom6Apr2020
        };
    }

    /// <summary>
    /// Gets the very last day of the specified tax period based on the applicable pay frequency.
    /// </summary>
    /// <param name="payFrequency">Applicable pay frequency.</param>
    /// <param name="taxPeriod">Applicable tax period.</param>
    /// <returns>Last day of the tax period.</returns>
    /// <exception cref="ArgumentException">Thrown if the supplied pay frequency is not supported.</exception>
    public DateOnly GetLastDayOfTaxPeriod(PayFrequency payFrequency, int taxPeriod) =>
        payFrequency switch
        {
            PayFrequency.Weekly => StartOfTaxYear.AddDays((7 * taxPeriod) - 1),
            PayFrequency.TwoWeekly => StartOfTaxYear.AddDays((14 * taxPeriod) - 1),
            PayFrequency.FourWeekly => StartOfTaxYear.AddDays((28 * taxPeriod) - 1),
            PayFrequency.Monthly => StartOfTaxYear.AddMonths(taxPeriod).AddDays(-1),
            PayFrequency.Quarterly => StartOfTaxYear.AddMonths(taxPeriod * 3).AddDays(-1),
            PayFrequency.BiAnnually => StartOfTaxYear.AddMonths(6 * taxPeriod).AddDays(-1),
            PayFrequency.Annually => EndOfTaxYear,
            _ => throw new ArgumentException($"Invalid pay frequency value {payFrequency}", nameof(payFrequency))
        };

    private int GetMonthNumber(DateOnly payDate)
    {
        var startOfCalendarYear = new DateOnly((int)TaxYearEnding, 1, 1);
        var monthNumber = payDate.Month + (payDate >= startOfCalendarYear && payDate <= EndOfTaxYear ? 12 : 0) - 3;
        var dayOfMonth = payDate.Day;

        return monthNumber - (dayOfMonth >= 1 && dayOfMonth <= 5 ? 1 : 0);
    }
}