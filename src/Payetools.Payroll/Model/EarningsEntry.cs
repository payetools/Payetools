// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;

namespace Payetools.Payroll.Model;

/// <summary>
/// Represents an element of an employee's pay.
/// </summary>
public class EarningsEntry : IEarningsEntry
{
    /// <summary>
    /// Gets the type of deduction.
    /// </summary>
    public IEarningsDetails EarningsDetails { get; init; } = default!;

    /// <summary>
    /// Gets the quantity of this earnings entry that when multiplied by the <see cref="ValuePerUnit"/> gives the total earnings.
    /// </summary>
    public decimal? QuantityInUnits { get; init; }

    /// <summary>
    /// Gets the GBP value per unit that when multiplied by the <see cref="QuantityInUnits"/> gives the total earnings.
    /// </summary>
    public decimal? ValuePerUnit { get; init; }

    /// <summary>
    /// Gets the fixed amount of the earnings, if that is specified in place of quantity and value per unit.  Used for absolute
    /// amounts.
    /// </summary>
    public decimal? FixedAmount { get; init; }

    /// <summary>
    /// Gets the total earnings to be applied.
    /// </summary>
    public decimal TotalEarnings => FixedAmount ?? QuantityInUnits * ValuePerUnit ?? 0.0m;

    /// <summary>
    /// Returns a new earnings entry which is the sum of the existing entry and the supplied entry.
    /// </summary>
    /// <param name="earningsEntry">Earnings entry data to add.</param>
    /// <returns>A new instance that implements <see cref="IEarningsEntry"/> containing the sum of the
    /// original entry and the supplied entry.</returns>
    /// <remarks>As it is possible for a unit-based earnings entry to have a different <see cref="ValuePerUnit"/>, this
    /// method sets this property to null to avoid holding confusing/erroneous information.</remarks>
    /// <exception cref="ArgumentException">Thrown if the supplied <see cref="EarningsDetails"/> property does not
    /// exactly match the existing property value.</exception>
    public IEarningsEntry Add(IEarningsEntry earningsEntry)
    {
        if (!earningsEntry.EarningsDetails.Equals(this.EarningsDetails))
            throw new ArgumentException("Earnings details of supplied earnings entry must match existing earnings details", nameof(earningsEntry));

        return new EarningsEntry
        {
            EarningsDetails = EarningsDetails,

            // We have to use the fixed amount as we can't rely on the ValuePerUnit not changing
            FixedAmount = TotalEarnings + earningsEntry.TotalEarnings,

            // Keep track of the historical quantity as it may be needed later
            QuantityInUnits = QuantityInUnits + earningsEntry.QuantityInUnits,

            // Use null here as no single value makes sense
            ValuePerUnit = null
        };
    }
}
