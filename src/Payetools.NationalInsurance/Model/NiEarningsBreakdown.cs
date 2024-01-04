// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

namespace Payetools.NationalInsurance.Model;

/// <summary>
/// Represents the breakdown of earnings against each of the relevant National Insurance thresholds.
/// </summary>
public struct NiEarningsBreakdown
{
    /// <summary>
    /// Gets the earnings up to and including the Lower Earnings Limit for this record.
    /// </summary>
    public decimal EarningsUpToAndIncludingLEL { get; init; }

    /// <summary>
    /// Gets the earnings up above the Lower Earnings Limit and up to and including the Secondary Threshold
    /// for this record.
    /// </summary>
    public decimal EarningsAboveLELUpToAndIncludingST { get; init; }

    /// <summary>
    /// Gets the earnings up above the Secondary Threshold and up to and including the Primary Threshold
    /// for this record.
    /// </summary>
    public decimal EarningsAboveSTUpToAndIncludingPT { get; init; }

    /// <summary>
    /// Gets the earnings up above the Primary Threshold and up to and including the Freeport Upper Secondary
    /// Threshold for this record.
    /// </summary>
    public decimal EarningsAbovePTUpToAndIncludingFUST { get; init; }

    /// <summary>
    /// Gets the earnings up above the Freeport Upper Secondary Threshold and up to and including the Upper
    /// Earnings Limit for this record.
    /// </summary>
    public decimal EarningsAboveFUSTUpToAndIncludingUEL { get; init; }

    /// <summary>
    /// Gets the earnings up above the Upper Earnings Limit for this record.
    /// </summary>
    public decimal EarningsAboveUEL { get; init; }

    /// <summary>
    /// Gets the earnings up above the Secondary Threshold and up to and including the Upper Earnings Limit
    /// for this record.
    /// </summary>
    public decimal EarningsAboveSTUpToAndIncludingUEL { get; init; }

    /// <summary>
    /// Adds the supplied breakdown to the current state and returns a new instance of <see cref="NiEarningsBreakdown"/>
    /// with the updated values.
    /// </summary>
    /// <param name="addedBreakdown">NI breakdown to be added.</param>
    /// <returns>Combined breakdown as a new <see cref="NiEarningsBreakdown"/> instance.</returns>
    public NiEarningsBreakdown Add(in NiEarningsBreakdown addedBreakdown) =>
        new NiEarningsBreakdown()
        {
            EarningsUpToAndIncludingLEL = this.EarningsUpToAndIncludingLEL + addedBreakdown.EarningsUpToAndIncludingLEL,
            EarningsAboveLELUpToAndIncludingST = this.EarningsAboveLELUpToAndIncludingST + addedBreakdown.EarningsAboveLELUpToAndIncludingST,
            EarningsAboveSTUpToAndIncludingPT = this.EarningsAboveSTUpToAndIncludingPT + addedBreakdown.EarningsAboveSTUpToAndIncludingPT,
            EarningsAbovePTUpToAndIncludingFUST = this.EarningsAbovePTUpToAndIncludingFUST + addedBreakdown.EarningsAbovePTUpToAndIncludingFUST,
            EarningsAboveFUSTUpToAndIncludingUEL = this.EarningsAboveFUSTUpToAndIncludingUEL + addedBreakdown.EarningsAboveFUSTUpToAndIncludingUEL,
            EarningsAboveUEL = this.EarningsAboveUEL + addedBreakdown.EarningsAboveUEL,
            EarningsAboveSTUpToAndIncludingUEL = this.EarningsAboveSTUpToAndIncludingUEL + addedBreakdown.EarningsAboveSTUpToAndIncludingUEL
        };
}