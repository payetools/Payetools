﻿// Copyright (c) 2023-2025, Payetools Ltd.
//
// Payetools Ltd licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Extensions;

namespace Payetools.Common.Model;

/// <summary>
/// Represents a specific pay date for a specific pay frequency.
/// </summary>
public readonly struct PayDate
{
    /// <summary>
    /// Gets the date of this <see cref="PayDate"/>.
    /// </summary>
    public DateOnly Date { get; init; }

    /// <summary>
    /// Gets the <see cref="TaxYear"/> for this <see cref="PayDate"/>.
    /// </summary>
    public TaxYear TaxYear { get; init; }

    /// <summary>
    /// Gets the tax period for this <see cref="PayDate"/>, for example, a pay date of 20th May for a monthly
    /// payroll would be tax period 2.
    /// </summary>
    public int TaxPeriod { get; init; }

    /// <summary>
    /// Gets the pay frequency for this <see cref="PayDate"/>.
    /// </summary>
    public PayFrequency PayFrequency { get; init; }

    /// <summary>
    /// Initialises a new <see cref="PayDate"/> based on the supplied date and pay frequency.
    /// </summary>
    /// <param name="date">Pay date.</param>
    /// <param name="payFrequency">Pay frequency.</param>
    public PayDate(in DateOnly date, in PayFrequency payFrequency)
    {
        Date = date;
        TaxYear = new TaxYear(date);
        TaxPeriod = TaxYear.GetTaxPeriod(date, payFrequency);
        PayFrequency = payFrequency;
    }

    /// <summary>
    /// Initialises a new <see cref="PayDate"/> based on the supplied date and pay frequency.
    /// </summary>
    /// <param name="year">Year.</param>
    /// <param name="month">Month (1-12).</param>
    /// <param name="day">Day.</param>
    /// <param name="payFrequency">Pay frequency.</param>
    public PayDate(in int year, in int month, in int day, in PayFrequency payFrequency)
        : this(new DateOnly(year, month, day), payFrequency)
    {
    }

    /// <summary>
    /// Gets the equivalent <see cref="DateTime"/> for this paydate, with the time portion set
    /// to midnight (00:00:00) and the DateTimeKind set to Unspecified.
    /// </summary>
    /// <param name="payDate"><see cref="PayDate"/> to get the DateTime for.</param>
    public static implicit operator DateTime(in PayDate payDate) => payDate.Date.ToDateTimeUnspecified();

    /// <summary>
    /// Provides a string representation of this pay date in the form 'dd/mm/yyyy (frequency, period)'.
    /// </summary>
    /// <returns>String representation of this <see cref="PayDate"/>.</returns>
    public override string ToString() =>
        $"{Date.ToShortDateString()} ({PayFrequency}, period {TaxPeriod})";

    /// <summary>
    /// Gets either the week number or the month number for this pay date, with
    /// the second out parameter indicating which.
    /// </summary>
    /// <param name="periodNumber">Week number or month number for this pay date.</param>
    /// <param name="isWeekly">True if the first out parameter is a week number,
    /// false indicates it is a month number.</param>
    public void GetWeekOrMonthNumber(out int periodNumber, out bool isWeekly)
    {
        isWeekly = PayFrequency == PayFrequency.Weekly |
            PayFrequency == PayFrequency.Fortnightly |
            PayFrequency == PayFrequency.FourWeekly;

        periodNumber = isWeekly ? TaxYear.GetWeekNumber(Date, PayFrequency) :
            TaxYear.GetMonthNumber(Date, PayFrequency);
    }
}