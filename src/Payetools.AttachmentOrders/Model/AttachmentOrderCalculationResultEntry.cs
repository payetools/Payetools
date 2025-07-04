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
    /// Gets any shortfall between the calculated deduction and the protected earnings; in some
    /// cases, shortfalls may be carried forward to the next pay period.
    /// </summary>
    public decimal? DeductionShortfall { get; }

    /// <summary>
    /// Gets any shortfall between the attachable earnings and protected earnings; used for audit/
    /// reporting purposes to explain why no deduction was made.
    /// </summary>
    public decimal? EarningsShortfall { get; }
}