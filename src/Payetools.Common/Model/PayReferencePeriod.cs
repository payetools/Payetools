// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

namespace Payetools.Common.Model;

/// <summary>
/// Represents a pay reference period, i.e. the range of dates from the first day an individual is being paid for,
/// to the last day, inclusive.
/// </summary>
public readonly struct PayReferencePeriod
{
    /// <summary>
    /// Gets the first day of the pay period.
    /// </summary>
    public DateOnly StartOfPayPeriod { get; init; }

    /// <summary>
    /// Gets the last day of the pay period.
    /// </summary>
    public DateOnly EndOfPayPeriod { get; init; }

    /// <summary>
    /// Initialises a new instance of <see cref="PayReferencePeriod"/> with the supplied start and end dates.
    /// </summary>
    /// <param name="startOfPayPeriod">First day of pay period.</param>
    /// <param name="endOfPayPeriod">Last day of pay period.</param>
    public PayReferencePeriod(DateOnly startOfPayPeriod, DateOnly endOfPayPeriod)
    {
        StartOfPayPeriod = startOfPayPeriod;
        EndOfPayPeriod = endOfPayPeriod;
    }
}