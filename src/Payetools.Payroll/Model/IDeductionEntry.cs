// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

namespace Payetools.Payroll.Model;

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
    IDeductionEntry Add(IDeductionEntry deductionEntry);
}