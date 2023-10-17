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
/// Interface that represents a deduction from payroll.
/// </summary>
public interface IDeductionEntry
{
    /// <summary>
    /// Gets the type of deduction.
    /// </summary>
    IDeductionDetails DeductionClassification { get; }

    /// <summary>
    /// Gets the quantity of this deduction that when multiplied by the <see cref="ValuePerUnit"/> gives the total deduction.  Optional.
    /// </summary>
    decimal? QuantityInUnits { get; }

    /// <summary>
    /// Gets the GBP value per unit that when multiplied by the <see cref="QuantityInUnits"/> gives the total deduction.  Optional.
    /// </summary>
    decimal? ValuePerUnit { get; }

    /// <summary>
    /// Gets the total deduction applied.
    /// </summary>
    decimal TotalDeduction { get; }
}