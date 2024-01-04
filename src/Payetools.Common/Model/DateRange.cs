// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Extensions;
using System.Collections;

namespace Payetools.Common.Model;

/// <summary>
/// Represents a date range.
/// </summary>
public readonly struct DateRange : IEnumerable<DateOnly>, IEquatable<DateRange>
{
    /// <summary>
    /// Gets the start of this date range.
    /// </summary>
    public DateOnly Start { get; }

    /// <summary>
    /// Gets the end of this date range.
    /// </summary>
    public DateOnly End { get; }

    /// <summary>
    /// Initialises a new <see cref="DateRange"/> from the supplied start and end dates.
    /// </summary>
    /// <param name="startDate">Start of date range.</param>
    /// <param name="endDate">End of date range.</param>
    /// <exception cref="ArgumentException">Thrown if endDate is before startDate.</exception>
    public DateRange(DateOnly startDate, DateOnly endDate)
    {
        if (endDate < startDate)
            throw new ArgumentException("End date must be on or after start date", nameof(endDate));

        Start = startDate;
        End = endDate;
    }

    /// <summary>
    /// Initialises a new <see cref="DateRange"/> from the supplied start date and duration,
    /// ensuring that the end date is never later than the supplied latestAllowableDate.
    /// </summary>
    /// <param name="startDate">Start of date range.</param>
    /// <param name="duration">Duration of the DateRange, in days.</param>
    /// <param name="latestAllowableDate">Latest allowable end date for the DateRange.</param>
    /// <exception cref="ArgumentException">Thrown if duration is less than a day.</exception>
    public DateRange(DateOnly startDate, int duration, DateOnly? latestAllowableDate = null)
    {
        if (duration < 1)
            throw new ArgumentException("Duration must be at least one day", nameof(duration));

        Start = startDate;
        End = latestAllowableDate is DateOnly latest ?
            startDate.AddDays(duration - 1).OrIfEarlier(latest) :
            startDate.AddDays(duration - 1);
    }

    /// <summary>
    /// Determines whether two <see cref="DateRange"/> instances are equal.
    /// </summary>
    /// <param name="other">DateRange to compare for equality.</param>
    /// <returns>True if the two DateRanges are equivalent; false otherwise.</returns>
    public readonly bool Equals(DateRange other) => Start.Equals(other.Start) && End.Equals(other.End);

    /// <summary>
    /// Gets an enumerator across this DateRange.  Enables use of foreach.
    /// </summary>
    /// <returns>Enumerator for enumerating across all the dates in between the Start and End dates.</returns>
    public readonly IEnumerator<DateOnly> GetEnumerator()
    {
        var date = Start;

        while (date <= End)
        {
            yield return date;

            date = date.AddDays(1);
        }
    }

    /// <summary>
    /// Gets an enumerator across this DateRange.  Enables use of foreach.
    /// </summary>
    /// <returns>Enumerator for enumerating across all the dates in between the Start and End dates.</returns>
    readonly IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    /// <summary>
    /// Determines whether two <see cref="DateRange"/> instances are equal.
    /// </summary>
    /// <param name="obj">Object to compare for equality.</param>
    /// <returns>True if the two DateRanges are equivalent; false otherwise.</returns>
    public override readonly bool Equals(object? obj) => obj is DateRange d && Equals(d);

    /// <summary>
    /// Returns the hash code for this object.
    /// </summary>
    /// <returns>A hash code for the current object.</returns>
    public override readonly int GetHashCode() => HashCode.Combine(Start, End);

    /// <summary>
    /// Evaluates whether two range ranges are equivalent.
    /// </summary>
    /// <param name="left">First DateRange to compare.</param>
    /// <param name="right">Second DateRange to compare.</param>
    /// <returns>True if the two DateRanges are equivalent; false otherwise.</returns>
    public static bool operator ==(DateRange left, DateRange right) => left.Equals(right);

    /// <summary>
    /// Evaluates whether two range ranges are not equivalent.
    /// </summary>
    /// <param name="left">First DateRange to compare.</param>
    /// <param name="right">Second DateRange to compare.</param>
    /// <returns>True if the two DateRanges are not equivalent; false otherwise.</returns>
    public static bool operator !=(DateRange left, DateRange right) => !(left == right);

    /// <summary>
    /// Returns a string that represents the current object.  Intended mainly for debug purposes.
    /// </summary>
    /// <returns>String representation of the DateRange in the format: start - end.</returns>
    public override string ToString() =>
        $"{Start} - {End}";
}
