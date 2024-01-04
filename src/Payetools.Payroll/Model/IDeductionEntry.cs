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
}