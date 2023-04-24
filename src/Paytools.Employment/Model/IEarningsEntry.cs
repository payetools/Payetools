// Copyright (c) 2023 Paytools Foundation.
//
// Paytools Foundation licenses this file to you under one of the following licenses:
//
//   * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//   * Paytools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

namespace Paytools.Employment.Model;

/// <summary>
/// Represents an element of an employee's pay.
/// </summary>
public interface IEarningsEntry
{
    /// <summary>
    /// Gets the type of deduction.
    /// </summary>
    IEarningsDetails EarningsDetails { get; }

    /// <summary>
    /// Gets the quantity of this earnings entry that when multiplied by the <see cref="ValuePerUnit"/> gives the total earnings.
    /// </summary>
    decimal? QuantityInUnits { get; }

    /// <summary>
    /// Gets the GBP value per unit that when multiplied by the <see cref="QuantityInUnits"/> gives the total earnings.
    /// </summary>
    decimal? ValuePerUnit { get; }

    /// <summary>
    /// Gets the total earnings to be applied.
    /// </summary>
    decimal TotalEarnings { get; }
}
