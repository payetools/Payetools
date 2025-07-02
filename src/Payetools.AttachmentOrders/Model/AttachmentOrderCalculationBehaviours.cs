// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

namespace Payetools.AttachmentOrders.Model;

/// <summary>
/// Enumeration of all the supported types of attachment of earnings orders.
/// </summary>
[Flags]
public enum AttachmentOrderCalculationBehaviours
{
    /// <summary>Used when an attachment order specifies a fixed amount per pay period.</summary>
    UseFixedPerPeriodAmount = 0b0000_0001,

    /// <summary>Used when an attachment order specifies a fixed percentage of attachable earnings
    /// per pay period.</summary>
    UseFixedPercentageOfEarnings = 0b0000_0010,

    /// <summary>Used when an attachment order requires use of a percentage amount based on the level
    /// of attachable earnings.</summary>
    UseTableBasedPercentageOfEarnings = 0b0000_0100,

    /// <summary>Used when an attachment order requires use of a fixed amount plus a percentage amount
    /// based on the level of attachable earnings.</summary>
    UsePercentageWithFixedOrMinimumAmount = 0b0000_1000,

    /// <summary>Used when an attachment order specifies a fixed amount based on the level of
    /// attachable earnings.</summary>
    PercentageTableBasedHasFixedAmount = 0b0001_0000,

    /// <summary>
    /// Gets a value indicating whether this attachment order uses the full attachable earnings amount,
    /// or if prior deductions should be subtracted from the attachable earnings amount before calculating.
    /// </summary>
    UseFullAttachableEarnings = 0b0010_0000,

    /// <summary>
    /// x.
    /// </summary>
    TreatAsPriorityOrder = 0b0100_0000
}