// Copyright (c) 2023-2025, Payetools Ltd.
//
// Payetools Ltd licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.NationalInsurance.Model;

namespace Payetools.NationalInsurance.ReferenceData;

/// <summary>
/// Record that represents a given National Insurance threshold across various pay frequencies.  Note
/// that HMRC guidance is that only the annual threshold should be used for calculations; weekly and
/// monthly thresholds are given for information only.
/// </summary>
public class NiThresholdEntry : INiThresholdEntry
{
    /// <summary>
    /// Gets the type of threshold this instance pertains to.
    /// </summary>
    public NiThresholdType ThresholdType { get; init; }

    /// <summary>
    /// Gets the per annum value of the threshold.
    /// </summary>
    public decimal ThresholdValuePerYear { get; init; }

    /// <summary>
    /// Gets the string representation of this <see cref="NiThresholdEntry"/> for debugging purposes.
    /// </summary>
    /// <returns>String representation of this instance.</returns>
    public override string ToString()
    {
        return $"{{ {ThresholdType}: {ThresholdValuePerYear} p.a. }}";
    }
}