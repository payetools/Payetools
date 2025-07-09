// Copyright (c) 2023-2025, Payetools Ltd.
//
// Payetools Ltd licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;

namespace Payetools.AttachmentOrders.Model;

/// <summary>
/// Enumerated value for payment frequency used in attachment orders.
/// </summary>
public enum AttachmentOrderPayFrequency
{
    /// <summary>Represents a daily pay frequency for attachment order purposes.</summary>
    Daily,

    /// <summary>Represents a weekly pay frequency for attachment order purposes.</summary>
    Weekly = PayFrequency.Weekly,

    /// <summary>Represents a monthly pay frequency for attachment order purposes.</summary>
    Monthly = PayFrequency.Monthly
}