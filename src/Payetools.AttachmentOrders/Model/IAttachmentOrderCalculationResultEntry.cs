// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

namespace Payetools.AttachmentOrders.Model;

/// <summary>
/// Interface that represents an entry in the results of an attachment of earnings calculation.
/// </summary>
public interface IAttachmentOrderCalculationResultEntry
{
    /// <summary>
    /// Gets the attachment order used to calculate this entry.
    /// </summary>
    IAttachmentOrder AttachmentOrder { get; }

    /// <summary>
    /// Gets the deduction amount calculated for this entry.
    /// </summary>
    decimal Deduction { get; }
}