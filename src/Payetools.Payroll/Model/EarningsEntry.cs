﻿// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

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
}
