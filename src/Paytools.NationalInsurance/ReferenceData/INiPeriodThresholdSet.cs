// Copyright (c) 2023 Paytools Foundation.
//
// Paytools Foundation licenses this file to you under one of the following licenses:
//
//   * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//   * Paytools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

using Paytools.NationalInsurance.Model;

namespace Paytools.NationalInsurance.ReferenceData;

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
