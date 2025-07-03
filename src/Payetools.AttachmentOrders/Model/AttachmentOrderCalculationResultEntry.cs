// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

namespace Payetools.AttachmentOrders.Model;

/// <summary>
/// Entity that represents an entry in the results of an attachment of earnings calculation.
/// </summary>
public readonly struct AttachmentOrderCalculationResultEntry
{
    /// <summary>
    /// Gets the attachment order used to calculate this entry.
    /// </summary>
    public required IAttachmentOrder AttachmentOrder { get; init; }

    /// <summary>
    /// Gets the deduction amount calculated for this entry.
    /// </summary>
    public decimal Deduction { get; init; }

    /// <summary>
    /// Gets a value indicating whether the deduction amount either exceeds or would exceed
    /// the attachable earnings. Used to indicate whether any further attachment orders should be
    /// applied to the attachable earnings amount in the current pay period.
    /// </summary>
    public bool DeductionExceedsAttachableEarnings { get; init; }
}