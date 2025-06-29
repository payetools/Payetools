// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

namespace Payetools.AttachmentOrders.ReferenceData;

/// <summary>
/// Represents a single entry in the rate table within the attachment order reference data.
/// </summary>
public class AttachmentOrderRateTableEntry
{
    /// <summary>
    /// Represents a band of rates for attachment orders, which apply to a specific range of attachable
    /// earnings, with no upper level represented by null. Bands can either have a fixed amount or a minimum
    /// deduction amount, but usually not both.
    /// </summary>
    public class Band
    {
        /// <summary>
        /// Gets the lower threshold for this band.
        /// </summary>
        public decimal From { get; init; }

        /// <summary>
        /// Gets the upper threshold for this band; may be null, meaning no upper threshold.
        /// </summary>
        public decimal? To { get; init; }

        /// <summary>
        /// Gets the minimum deduction amount to apply. Used in the scenario where a percentage or
        /// an amount is to be applied, whichever the greater.
        /// </summary>
        public decimal? MinimumAmount { get; init; }

        /// <summary>
        /// Gets the fixed deduction amount to apply for this band, if applicable.
        /// </summary>
        public decimal? FixedAmount { get; init; }
    }

    /// <summary>
    /// Gets the standard deduction rate to be applied. Also used if there is only one rate to apply.
    /// </summary>
    public decimal StandardRate { get; init; }

    /// <summary>
    /// Gets any higher deduction rate to be applied. Null if not applicable.
    /// </summary>
    public decimal? HigherRate { get; init; }

    /// <summary>
    /// Gets the daily rate band applicable to this rate table entry.
    /// </summary>
    public required Band Daily { get; init; }

    /// <summary>
    /// Gets the weekly rate band applicable to this rate table entry.
    /// </summary>
    public required Band Weekly { get; init; }

    /// <summary>
    /// Gets the monthly rate band applicable to this rate table entry.
    /// </summary>
    public required Band Monthly { get; init; }
}