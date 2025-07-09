// Copyright (c) 2023-2025, Payetools Ltd.
//
// Payetools Ltd licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

namespace Payetools.AttachmentOrders.Model;

/// <summary>
/// Represents the deduction information for an attachment order, including minimum and fixed amounts,
/// if applicable, and the rate to be applied.
/// </summary>
public readonly struct AttachmentOrderDeductionInfo
{
    /// <summary>
    /// Gets the minimum deduction amount to apply. Used in the scenario where a percentage or
    /// an amount is to be applied, whichever the greater.
    /// </summary>
    public decimal? MinimumAmount { get; init; }

    /// <summary>
    /// Gets the fixed deduction amount to apply for this band, if applicable.
    /// </summary>
    public decimal? FixedAmount { get; init; }

    /// <summary>
    /// Gets the rate to be applied for this deduction.
    /// </summary>
    public decimal Rate { get; init; }
}