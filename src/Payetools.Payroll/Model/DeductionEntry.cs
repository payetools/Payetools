// Copyright (c) 2023-2025, Payetools Ltd.
//
// Payetools Ltd licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;

namespace Payetools.Payroll.Model;

/// <summary>
/// Represents a deduction from payroll.
/// </summary>
public class DeductionEntry : IDeductionEntry
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

    /// <summary>
    /// Returns a new deductions entry which is the sum of the existing entry and the supplied entry.
    /// </summary>
    /// <param name="deductionEntry">Deductions entry data to add.</param>
    /// <returns>A new instance that implements <see cref="IDeductionEntry"/> containing the sum of the
    /// original entry and the supplied entry.</returns>
    /// <remarks>As it is possible for a unit-based earnings entry to have a different <see cref="ValuePerUnit"/>, this
    /// method sets this property to null to avoid holding confusing/erroneous information.</remarks>
    /// <exception cref="ArgumentException">Thrown if the supplied <see cref="DeductionClassification"/> property does not
    /// exactly match the existing property value.</exception>
    public IDeductionEntry Add(IDeductionEntry deductionEntry)
    {
        if (!deductionEntry.DeductionClassification.Equals(this.DeductionClassification))
            throw new ArgumentException("Deduction classification of supplied deduction entry must match existing deduction classification", nameof(deductionEntry));

        return new DeductionEntry
        {
            DeductionClassification = DeductionClassification,

            // We have to use the fixed amount as we can't rely on the ValuePerUnit not changing
            FixedAmount = TotalDeduction + deductionEntry.TotalDeduction,

            // Keep track of the historical quantity as it may be needed later
            QuantityInUnits = QuantityInUnits + deductionEntry.QuantityInUnits,

            // Use null here as no single value makes sense
            ValuePerUnit = null
        };
    }
}