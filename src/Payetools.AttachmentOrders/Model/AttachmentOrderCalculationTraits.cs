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
public enum AttachmentOrderCalculationTraits
{
    /// <summary>Used when an attachment order specifies a fixed amount per pay period.  Mutually exclusive
    /// with all other Use... values.</summary>
    UseFixedPerPeriodAmount = 0b0000_0000_0001,

    /// <summary>Used when an attachment order specifies a fixed percentage of attachable earnings
    /// per pay period. Mutually exclusive with all other Use... values.</summary>
    UseFixedPercentage = 0b0000_0000_0010,

    /// <summary>Used when an attachment order requires use of a percentage amount based on the level
    /// of attachable earnings as defined in an external table. Mutually exclusive with all other
    /// Use... values.</summary>
    UseTableBasedPercentages = 0b0000_0000_0100,

    /// <summary>Used alongside on of the Use... values to indicate that a minimum amount should be applied.
    /// Typically this amount is table-driven.</summary>
    SupplementWithMinimumAmount = 0b0000_0000_1000,

    /// <summary>Used alongside on of the Use... values to indicate that fixed amounts should be applied on
    /// top of any percentage-based amount. Typically this amount is table-driven.</summary>
    SupplementWithFixedAmounts = 0b0000_0001_0000,

    /// <summary>
    /// Used when an attachment order specifies a fixed amount per pay period based on a
    /// table of fixed amounts driven by the level of attachable earnings.
    /// </summary>
    UseTableBasedFixedAmounts = 0b0000_0010_0000,

    /// <summary>
    /// Indicates whether this attachment order uses the full attachable earnings amount, or if prior
    /// deductions should be subtracted from the attachable earnings amount before calculating.
    /// </summary>
    UseFullAttachableEarnings = 0b0000_0100_0000,

    /// <summary>
    /// Indicates whether this attachment order should be treated as a priority order.
    /// </summary>
    TreatAsPriorityOrder = 0b0000_1000_0000,

    /// <summary>
    /// Indicates whether this attachment order allows partial deductions. If this value is set to false,
    /// then if the entire amount of the attachment order cannot be deducted in full, zero deduction is
    /// made for this attachment order in the pay period.
    /// </summary>
    AllowPartialDeductions = 0b0001_0000_0000
}