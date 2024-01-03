// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Payetools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

namespace Payetools.Common.Extensions;

/// <summary>
/// Extension methods for instances of <see cref="DateTime"/>.
/// </summary>
public static class DateTimeExtensions
{
    /// <summary>
    /// Gets an instance of DateTime from the supplied DateTime but with the time portion set to midday (12:00:00) UTC.
    /// </summary>
    /// <param name="dateTime">Source DateTime instance.</param>
    /// <returns>DateTime with the same date but time portion set to 12:00:00 UTC.</returns>
    public static DateTime MiddayUtc(this DateTime dateTime) =>
        new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 12, 0, 0, DateTimeKind.Utc);
}
