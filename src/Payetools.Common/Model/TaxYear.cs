﻿// Copyright (c) 2023-2025, Payetools Ltd.
//
// Payetools Ltd licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Extensions;

namespace Payetools.Common.Model;

/// <summary>
/// Represents a given UK tax year, running from 6th April of a given year through to 5th April the following
/// year.
/// </summary>
/// <remarks><see cref="TaxYear"/> provides utility methods to access the relevant tax regimes (i.e., sub-countries
/// within the UK) which have changed over the period 2018 to date.  TaxYear also provides conversions from dates
/// to tax periods, based on payment frequency.</remarks>
public readonly struct TaxYear
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
    public TaxYear(in TaxYearEnding taxYearEnding)
    {
        TaxYearEnding = taxYearEnding;
        StartOfTaxYear = new DateOnly((int)TaxYearEnding - 1, 4, 6);
        EndOfTaxYear = new DateOnly((int)TaxYearEnding, 4, 5);
    }

    /// <summary>
    /// Initialises a new instance of <see cref="TaxYear"/> based on the supplied date.
    /// </summary>
    /// <param name="taxDate">Date to create <see cref="TaxYear"/> for.</param>
    public TaxYear(in DateOnly taxDate)
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
    public bool IsValidForYear(/* in */ CountriesForTaxPurposes countries)
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
    public int GetTaxPeriod(in DateOnly payDate, in PayFrequency payFrequency)
    {
        if (payDate < StartOfTaxYear || payDate > EndOfTaxYear)
            throw new ArgumentException($"Pay date of {payDate.ToUk()} is outside this tax year {StartOfTaxYear.ToUk()} - {EndOfTaxYear.ToUk()}", nameof(payDate));

        switch (payFrequency)
        {
            case PayFrequency.Annually:
                return 1;

            case PayFrequency.Monthly:
                return GetMonthNumber(payDate, PayFrequency.Monthly);

            case PayFrequency.Quarterly:
                return ((GetMonthNumber(payDate, PayFrequency.Monthly) - 1) / 3) + 1;

            case PayFrequency.BiAnnually:
                return ((GetMonthNumber(payDate, PayFrequency.Monthly) - 1) / 6) + 1;

            default:
                var dayCountPerPeriod = payFrequency switch
                {
                    PayFrequency.Weekly => 7,
                    PayFrequency.Fortnightly => 14,
                    PayFrequency.FourWeekly => 28,
                    _ => throw new ArgumentException($"Invalid pay frequency value {payFrequency}", nameof(payFrequency))
                };
                return (int)Math.Ceiling((float)GetDayNumber(payDate) / dayCountPerPeriod);
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
    public DateOnly GetLastDayOfTaxPeriod(in PayFrequency payFrequency, in int taxPeriod) =>
        payFrequency switch
        {
            PayFrequency.Weekly => StartOfTaxYear.AddDays((7 * taxPeriod) - 1),
            PayFrequency.Fortnightly => StartOfTaxYear.AddDays((14 * taxPeriod) - 1),
            PayFrequency.FourWeekly => StartOfTaxYear.AddDays((28 * taxPeriod) - 1),
            PayFrequency.Monthly => StartOfTaxYear.AddMonths(taxPeriod).AddDays(-1),
            PayFrequency.Quarterly => StartOfTaxYear.AddMonths(taxPeriod * 3).AddDays(-1),
            PayFrequency.BiAnnually => StartOfTaxYear.AddMonths(6 * taxPeriod).AddDays(-1),
            PayFrequency.Annually => EndOfTaxYear,
            _ => throw new ArgumentException($"Invalid pay frequency value {payFrequency}", nameof(payFrequency))
        };

    /// <summary>
    /// Gets the tax week number from the supplied pay date. Where the pay frequency is other than weekly,
    /// the pay period duration is taken into account, meaning, for example, that a 4-weekly payment made
    /// on 3rd March will have a week number of 4, but so would a similar payment roughly one week earlier,
    /// on say 25th February.
    /// </summary>
    /// <param name="payDate">Pay date.</param>
    /// <param name="payFrequency">Applicable pay frequency. Defaults to weekly.</param>
    /// <returns>Tax week number for the supplied pay date and (optional) pay frequency.</returns>
    public int GetWeekNumber(in DateOnly payDate, in PayFrequency payFrequency = PayFrequency.Weekly)
    {
        var multiplier = payFrequency switch
        {
            PayFrequency.FourWeekly => 4,
            PayFrequency.Fortnightly => 2,
            PayFrequency.Weekly => 1,
            _ => 0
        };

        return multiplier > 0 ?
            GetTaxPeriod(payDate, payFrequency) * multiplier :
            (int)Math.Ceiling((float)GetDayNumber(payDate) / 7);
    }

    /// <summary>
    /// Gets the tax month number from the supplied pay date. Where the pay frequency is other than monthly,
    /// the pay period duration is taken into account, meaning, for example, that a quarterly payment made
    /// on 5th July will have a month number of 3, but so would a similar payment roughly one month earlier,
    /// on say 5th June.
    /// </summary>
    /// <param name="payDate">Pay date.</param>
    /// <param name="payFrequency">Applicable pay frequency.</param>
    /// <returns>Tax month number.</returns>
    public int GetMonthNumber(in DateOnly payDate, in PayFrequency payFrequency)
    {
        if (payDate < StartOfTaxYear || payDate > EndOfTaxYear)
            throw new ArgumentException($"Pay date of {payDate.ToUk()} is outside this tax year {StartOfTaxYear.ToUk()} - {EndOfTaxYear.ToUk()}", nameof(payDate));

        var startOfCalendarYear = new DateOnly((int)TaxYearEnding, 1, 1);
        var monthNumber = payDate.Month + (payDate >= startOfCalendarYear && payDate <= EndOfTaxYear ? 12 : 0) - 3;
        var dayOfMonth = payDate.Day;

        var taxMonthNumber = monthNumber - (dayOfMonth >= 1 && dayOfMonth <= 5 ? 1 : 0);

        return payFrequency switch
        {
            PayFrequency.Annually => 12,
            PayFrequency.BiAnnually => 6 + ((taxMonthNumber - 1) / 6 * 6),
            PayFrequency.Quarterly => 3 + ((taxMonthNumber - 1) / 3 * 3),
            _ => taxMonthNumber
        };
    }

    /// <summary>
    /// Determines whether the supplied date is within this tax year.
    /// </summary>
    /// <param name="date">Date to test.</param>
    /// <returns>True if within tax year; false otherwise.</returns>
    public bool IsWithin(in DateOnly date) => date >= StartOfTaxYear && date <= EndOfTaxYear;

    /// <summary>
    /// Gets the tax month number from the supplied tax year and pay date. Ignores pay frequency as this static overload is
    /// primarily intended for establishing the tax month of a particular pay date for reporting purposes.
    /// </summary>
    /// <param name="taxYear">Applicable tax year.</param>
    /// <param name="payDate">Pay date.</param>
    /// <returns>Tax month number.</returns>
    public static int GetMonthNumber(in TaxYear taxYear, in DateOnly payDate) =>
        taxYear.GetMonthNumber(payDate, PayFrequency.Monthly);

    /// <summary>
    /// Gets the tax month number from the supplied tax year and pay date. Ignores pay frequency as this static overload is
    /// primarily intended for establishing the tax month of a particular pay date for reporting purposes.
    /// </summary>
    /// <param name="taxYear">Applicable tax year.</param>
    /// <param name="payDate">Pay date.</param>
    /// <returns>Tax month number.</returns>
    public static int GetMonthNumber(in TaxYear taxYear, in PayDate payDate) =>
        taxYear.GetMonthNumber(payDate.Date, PayFrequency.Monthly);

    private int GetDayNumber(in DateOnly payDate) =>
        IsWithin(payDate) ?
            payDate.DayNumber - StartOfTaxYear.DayNumber + 1 :
            throw new ArgumentException($"Pay date of {payDate.ToUk()} is outside this tax year {StartOfTaxYear.ToUk()} - {EndOfTaxYear.ToUk()}", nameof(payDate));
}