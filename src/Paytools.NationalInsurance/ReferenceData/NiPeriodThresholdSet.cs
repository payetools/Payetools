// Copyright (c) 2023 Paytools Foundation.
//
// Licensed under the Apache License, Version 2.0 (the "License") ~
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using Paytools.Common.Model;

namespace Paytools.NationalInsurance.ReferenceData;

/// <summary>
/// Represents a set of NI thresholds that have been adjusted to a proportion of the tax year.
/// </summary>
public record NiPeriodThresholdSet
{
    private readonly NiPeriodThresholdEntry[] _thresholdEntries;

    /// <summary>
    /// Initialises a new instance of <see cref="NiPeriodThresholdSet"/> based on the supplied annual thresholds, the
    /// applicable pay frequency and the applicable number of tax periods.
    /// </summary>
    /// <param name="entries">Set of National Insurance thresholds as defined by HMRC for a given tax year (or portion
    /// of a tax year, when there are in-year changes).</param>
    /// <param name="payFrequency">Applicable pay frequency.</param>
    /// <param name="numberOfTaxPeriods">Number of tax periods, if applicable.  Defaults to 1.</param>
    public NiPeriodThresholdSet(INiThresholdSet entries, PayFrequency payFrequency, int numberOfTaxPeriods = 1)
    {
        int entryCount = entries.Count;

        _thresholdEntries = new NiPeriodThresholdEntry[entryCount];

        for (int index = 0; index < entryCount; index++)
        {
            _thresholdEntries[entries[index].ThresholdType.GetIndex()] =
                new NiPeriodThresholdEntry(entries[index], payFrequency, numberOfTaxPeriods);
        }
    }

    /// <summary>
    /// Gets the base threshold for the period, as distinct from the value returned by <see cref="GetThreshold1"/> (see below).
    /// </summary>
    /// <param name="thresholdType">Applicable threshold (e.g., LEL, UEL, PT).</param>
    /// <returns>Pro-rata threshold value applicable to the period and threshold type.</returns>
    public decimal GetThreshold(NiThresholdType thresholdType) =>
        _thresholdEntries[thresholdType.GetIndex()].ThresholdForPeriod;

    /// <summary>
    /// Gets the modified threshold for the period (as distinct from the value returned by <see cref="GetThreshold1"/>)
    /// where rounding is applied based on whether the pay frequency is weekly or monthly, or otherwise.  As detailed in
    /// HMRC's NI calculation documentation as 'p1'.
    /// </summary>
    /// <param name="thresholdType">Applicable threshold (e.g., LEL, UEL, PT).</param>
    /// <returns>Pro-rata threshold value applicable to the period and threshold type.</returns>
    public decimal GetThreshold1(NiThresholdType thresholdType) =>
        _thresholdEntries[thresholdType.GetIndex()].ThresholdForPeriod1;

    /// <summary>
    /// Gets a string representation of this <see cref="NiPeriodThresholdSet"/>.
    /// </summary>
    /// <returns>String representation of this instance.</returns>
    public override string ToString() =>
        string.Join(" | ", _thresholdEntries.Select(te => te.ToString()).ToArray());
}