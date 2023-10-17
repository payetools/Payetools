// Copyright (c) 2023 Payetools Foundation.
//
// Paytools Foundation licenses this file to you under one of the following licenses:
//
//   * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//   * Paytools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

namespace Payetools.Employment.Model;

/// <summary>
/// Interface that types should implement to communicate an amount that is either fixed or calculated
/// as a product of quantity in units and per unit rate.
/// </summary>
public interface IPayrollAmount
{
    /// <summary>
    /// Gets the quantity of this deduction that when multiplied by the <see cref="ValuePerUnit"/> gives the total amount.
    /// </summary>
    public decimal? QuantityInUnits { get; init; }

    /// <summary>
    /// Gets the GBP value per unit that when multiplied by the <see cref="QuantityInUnits"/> gives the total deduction.
    /// </summary>
    public decimal? ValuePerUnit { get; init; }

    /// <summary>
    /// Gets the fixed amount of the deduction, if that is specified in place of quantity and value per unit.  Used for absolute
    /// amounts.
    /// </summary>
    public decimal? FixedAmount { get; init; }

    /// <summary>
    /// Gets the total amount to be applied (default implementation).
    /// </summary>
    public decimal TotalDeduction => FixedAmount ?? QuantityInUnits * ValuePerUnit ?? 0.0m;
}