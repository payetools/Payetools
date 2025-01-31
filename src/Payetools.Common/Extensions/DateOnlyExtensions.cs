// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;
using System.Globalization;

namespace Payetools.Common.Extensions;

/// <summary>
/// Extension methods for instances of <see cref="DateOnly"/>.
/// </summary>
public static class DateOnlyExtensions
{
    /// <summary>
    /// Provides an equivalent <see cref="DateTime"/> to the supplied <see cref="DateOnly"/> with the
    /// time portion set to midnight (00:00:00) and the DateTimeKind set to Unspecified. This is used to
    /// minimise the possibility of DateTimes being misinterpreted as the next or previous day due to the
    /// use of non-UTC timezones.
    /// </summary>
    /// <param name="date">DateOnly to convert to DateTime.</param>
    /// <returns>DateTime instance with the same date as the supplied DateOnly and time portion set to 12:00:00 UTC.</returns>
    public static DateTime ToDateTimeUnspecified(in this DateOnly date) =>
        date.ToDateTime(TimeOnly.MinValue, DateTimeKind.Unspecified);

    /// <summary>
    /// Gets the <see cref="TaxYearEnding"/> value for the supplied <see cref="DateOnly"/>.
    /// </summary>
    /// <param name="date"><see cref="DateOnly"/> to get the TaxYearEnding for.</param>
    /// <returns>Relevant <see cref="TaxYearEnding"/> for the supplied date.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the supplied date is outside the supported
    /// set of dates for this library.</exception>
    public static TaxYearEnding ToTaxYearEnding(in this DateOnly date)
    {
        var apr6 = new DateOnly(date.Year, 4, 6);

        var taxYear = date.Year + (date < apr6 ? 0 : 1);

        if (taxYear < (int)TaxYearEnding.MinValue || taxYear > (int)TaxYearEnding.MaxValue)
            throw new ArgumentOutOfRangeException(nameof(date), $"Unsupported tax year; date must fall within range tax year ending 6 April {(int)TaxYearEnding.MinValue} to 6 April {(int)TaxYearEnding.MaxValue}");

        return (TaxYearEnding)taxYear;
    }

    /// <summary>
    /// Calculates the age of a person on a certain date based on the supplied date of birth.  Takes account of leap years,
    /// using the convention that someone born on 29th February in a leap year is not legally one year older until 1st March
    /// of a non-leap year.
    /// </summary>
    /// <param name="dateOfBirth">Individual's date of birth.</param>
    /// <param name="date">Date at which to evaluate age at.</param>
    /// <returns>Age of the individual in years (as an integer).</returns>
    /// <remarks>This code is not guaranteed to be correct for non-UK locales, as some countries have skipped certain dates
    /// within living memory.</remarks>
    public static int AgeAt(in this DateOnly dateOfBirth, in DateOnly date)
    {
        int age = date.Year - dateOfBirth.Year;

        return dateOfBirth > date.AddYears(-age) ? --age : age;
    }

    /// <summary>
    /// Converts the supplied <see cref="DateOnly"/> to a <see cref="PayDate"/> based on the specified <see cref="PayFrequency"/>.
    /// </summary>
    /// <param name="date">Date to obtain PayDate for.</param>
    /// <param name="payFrequency">Applicable pay frequency for this pay date.</param>
    /// <returns>PayDate for the supplied date and pay frequency.</returns>
    public static PayDate ToPayDate(in this DateOnly date, in PayFrequency payFrequency)
        => new PayDate(date, payFrequency);

    /// <summary>
    /// Returns the date as a string in UK format (dd/mm/yyyy).
    /// </summary>
    /// <param name="date">Date to get string representation for.</param>
    /// <returns>UK format date string.</returns>
    public static string ToUk(in this DateOnly date) => date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

    /// <summary>
    /// Returns the earlier of two dates.
    /// </summary>
    /// <param name="date">First date to compare.</param>
    /// <param name="other">Second date to compare.</param>
    /// <returns>The earlier of the two dates.</returns>
    public static DateOnly OrIfEarlier(in this DateOnly date, in DateOnly other) =>
        date > other ? other : date;

    /// <summary>
    /// Returns the later of two dates.
    /// </summary>
    /// <param name="date">First date to compare.</param>
    /// <param name="other">Second date to compare.</param>
    /// <returns>The later of the two dates.</returns>
    public static DateOnly OrIfLater(in this DateOnly date, in DateOnly other) =>
        other > date ? other : date;
}