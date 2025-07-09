// Copyright (c) 2023-2025, Payetools Ltd.
//
// Payetools Ltd licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.NationalInsurance.Model;

namespace Payetools.NationalInsurance.ReferenceData;

/// <summary>
/// Interface that represents a set of NI thresholds that have been adjusted to a proportion of the tax year.
/// </summary>
public interface INiPeriodThresholdSet : INiThresholdSet
{
    /// <summary>
    /// Gets the modified threshold for the period (as distinct from the value returned by <see cref="GetThreshold1"/>)
    /// where rounding is applied based on whether the pay frequency is weekly or monthly, or otherwise.  As detailed in
    /// HMRC's NI calculation documentation as 'p1'.
    /// </summary>
    /// <param name="thresholdType">Applicable threshold (e.g., LEL, UEL, PT).</param>
    /// <returns>Pro-rata threshold value applicable to the period and threshold type.</returns>
    decimal GetThreshold1(NiThresholdType thresholdType);
}