// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

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
    public static DateTime MiddayUtc(in this DateTime dateTime) =>
        new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 12, 0, 0, DateTimeKind.Utc);
}
