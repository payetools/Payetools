// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

namespace Payetools.Payroll.Model;

/// <summary>
/// Represents a deduction from payroll.
/// </summary>
public record DeductionEntry : IDeductionEntry
{
    /// <summary>
    /// Gets the type of deduction.
    /// </summary>
    public IDeductionDetails DeductionClassification { get; init; } = default!;

    /// <summary>
    /// Gets the quantity of this deduction that when multiplied by the <see cref="ValuePerUnit"/> gives the total deduction.
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
    /// Gets the total deduction to be applied.
    /// </summary>
    public decimal TotalDeduction => FixedAmount ?? QuantityInUnits * ValuePerUnit ?? 0.0m;
}
