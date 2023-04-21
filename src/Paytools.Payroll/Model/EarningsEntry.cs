// Copyright (c) 2023 Paytools Foundation.
//
// Paytools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Paytools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

using Paytools.Employment.Model;

namespace Paytools.Payroll.Model;

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
}
