// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

namespace Payetools.Statutory.AttachmentOfEarnings;

/// <summary>
/// Interface that represents an attachment of earnings order.
/// </summary>
public interface IAttachmentOfEarnings
{
    /// <summary>
    /// Gets the type of attachment of earnings order.
    /// </summary>
    AttachmentOfEarningsType AttachmentOfEarningsType { get; }

    /// <summary>
    /// Gets the date from which this attachment of earnings order is effective.
    /// </summary>
    DateTime EffectiveDate { get; }
}