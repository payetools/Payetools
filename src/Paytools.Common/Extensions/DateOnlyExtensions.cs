// Copyright (c) 2023 Paytools Foundation.  All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License") ~
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

using Paytools.Common.Model;

namespace Paytools.Common.Extensions;

/// <summary>
/// Extension methods for instances of <see cref="DateOnly"/>.
/// </summary>
public static class DateOnlyExtensions
{
    /// <summary>
    /// Provides an equivalent <see cref="DateTime"/> to the supplied <see cref="DateOnly"/> with the
    /// time portion set to midday (12:00:00) UTC.  This is used to minimise the possibility of dates being misinterpreted
    /// as the next or previous day due to the use of non-UTC timezones.
    /// </summary>
    /// <param name="date">DateOnly to convert to DateTime.</param>
    /// <returns>DateTime instance with the same date as the supplied DateOnly and time portion set to 12:00:00 UTC.</returns>
    public static DateTime ToMiddayUtcDateTime(this DateOnly date) =>
        date.ToDateTime(new TimeOnly(12, 0), DateTimeKind.Utc);

    /// <summary>
    /// Gets the <see cref="TaxYearEnding"/> value for the supplied <see cref="DateOnly"/>.
    /// </summary>
    /// <param name="date"><see cref="DateOnly"/> to get the TaxYearEnding for.</param>
    /// <returns>Relevant <see cref="TaxYearEnding"/> for the supplied date.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the supplied date is outside the supported
    /// set of dates for this library.</exception>
    public static TaxYearEnding ToTaxYearEnding(this DateOnly date)
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
    public static int AgeAt(this DateOnly dateOfBirth, DateOnly date)
    {
        int age = date.Year - dateOfBirth.Year;

        return dateOfBirth > date.AddYears(-age) ? --age : age;
    }
}