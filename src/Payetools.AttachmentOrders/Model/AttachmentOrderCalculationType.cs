// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

namespace Payetools.AttachmentOrders.Model;

/// <summary>
/// Enumeration of all the supported types of attachment of earnings orders.
/// </summary>
public enum AttachmentOrderCalculationType
{
    /// <summary>Used when an attachment order specifies a fixed amount per pay period.</summary>
    FixedAmount,

    /// <summary>Used when an attachment order specifies a fixed percentage of attachable earnings
    /// per pay period.</summary>
    FixedPercentageOfEarnings,

    /// <summary>Used when an attachment order requires use of a percentage amount based on the level
    /// of attachable earnings.</summary>
    TableBasedPercentageOfEarnings,

    /// <summary>Used when an attachment order requires use of a fixed amount plus a percentage amount
    /// based on the level of attachable earnings.</summary>
    TableBasedPercentageOfEarningsPlusFixedAmount,

    /// <summary>Used when an attachment order specifies a fixed amount based on the level of
    /// attachable earnings.</summary>
    TableBasedFixedAmount
}