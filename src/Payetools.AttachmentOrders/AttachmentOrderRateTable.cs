// Copyright (c) 2023-2025, Payetools Ltd.
//
// Payetools Ltd licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.AttachmentOrders.Model;
using Payetools.AttachmentOrders.ReferenceData;

namespace Payetools.AttachmentOrders;

/// <summary>
/// Represents a table of rates for attachment orders, used to determine the deduction rate
/// based on earnings and pay frequency, and optionally fixed and/or minimum amounts.
/// </summary>
public class AttachmentOrderRateTable
{
    private readonly IEnumerable<AttachmentOrderRateTableEntry> _rateTable;

    /// <summary>
    /// Initializes a new instance of the <see cref="AttachmentOrderRateTable"/> class.
    /// </summary>
    /// <param name="rateTable">Set of rate table entries to use for this rate table.</param>
    public AttachmentOrderRateTable(IEnumerable<AttachmentOrderRateTableEntry> rateTable)
    {
        _rateTable = rateTable;
    }

    /// <summary>
    /// Gets the applicable rate table entry for the given earnings and pay frequency.
    /// </summary>
    /// <param name="earnings">Attachable earnings.</param>
    /// <param name="payFrequency">Applicable pay frequency. Note that this is not the same
    /// enumeration as found in Payetools.Common.Model, as that covers more values than applicable
    /// here.</param>
    /// <param name="rateType">Applicable rate type. Defaults to Standard.</param>
    /// <returns><see cref="AttachmentOrderDeductionInfo"/> instance containing the applicable deduction
    /// information.</returns>
    /// <exception cref="NotSupportedException">Thrown if an invalid pay frequency or rate type is supplied.
    /// Also thrown if a higher rate is requested but no higher rate is available.</exception>
    public AttachmentOrderDeductionInfo GetApplicableDeductionInfo(
        decimal earnings,
        AttachmentOrderPayFrequency payFrequency,
        AttachmentOrderRateType rateType = AttachmentOrderRateType.Standard)
    {
        // Search from the highest band downwards to find the first applicable entry.
        foreach (var entry in _rateTable.Reverse())
        {
            var band = payFrequency switch
            {
                AttachmentOrderPayFrequency.Daily => entry.Daily,
                AttachmentOrderPayFrequency.Weekly => entry.Weekly,
                AttachmentOrderPayFrequency.Monthly => entry.Monthly,
                _ => throw new NotSupportedException($"Pay frequency {payFrequency} is not supported")
            };

            if (earnings >= band.From && (band.To == null || earnings <= band.To))
            {
                return new AttachmentOrderDeductionInfo
                {
                    FixedAmount = band.FixedAmount,
                    MinimumAmount = band.MinimumAmount,
                    Rate = rateType switch
                    {
                        AttachmentOrderRateType.Standard => entry.StandardRate,
                        AttachmentOrderRateType.Higher => entry.HigherRate
                            ?? throw new NotSupportedException($"Higher rate is not defined for rate type {rateType}"),
                        _ => throw new NotSupportedException($"Rate type {rateType} is not supported")
                    }
                };
            }
        }

        throw new InvalidOperationException($"No applicable information found for earnings {earnings} and pay frequency {payFrequency}");
    }
}