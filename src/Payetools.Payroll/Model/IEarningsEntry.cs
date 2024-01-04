// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

namespace Payetools.Payroll.Model;

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
