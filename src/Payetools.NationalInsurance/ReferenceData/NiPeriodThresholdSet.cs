// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;
using Payetools.NationalInsurance.Model;
using System.Collections;

namespace Payetools.NationalInsurance.ReferenceData;

/// <summary>
/// Represents a set of NI thresholds that have been adjusted to a proportion of the tax year.
/// </summary>
public class NiPeriodThresholdSet : INiPeriodThresholdSet
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

    /// <inheritdoc/>
    public INiThresholdEntry this[int index] => throw new NotImplementedException();

    /// <summary>
    /// Gets the number of threshold value this threshold set contains.
    /// </summary>
    public int Count => _thresholdEntries.Length;

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

    /// <summary>
    /// Returns an enumerator that iterates through the thresholds (of type <see cref="INiThresholdEntry"/>).
    /// </summary>
    /// <returns>An enumerator that can be used to iterate through the collection of thresholds.</returns>
    /// <exception cref="InvalidOperationException">Throw if the enumerator cannot be obtained.  (Should never be thrown).</exception>
    public IEnumerator<INiThresholdEntry> GetEnumerator() =>
        (_thresholdEntries as IEnumerable<INiThresholdEntry>)?.GetEnumerator() ??
        throw new InvalidOperationException("Unexpected type mismatch when obtaining enumerator");

    /// <summary>
    /// Returns an enumerator that iterates through the thresholds (of type <see cref="INiThresholdEntry"/>).
    /// </summary>
    /// <returns>An enumerator that can be used to iterate through the collection of thresholds.</returns>
    /// <exception cref="InvalidOperationException">Throw if the enumerator cannot be obtained.  (Should never be thrown).</exception>
    IEnumerator IEnumerable.GetEnumerator() =>
        (_thresholdEntries as IEnumerable)?.GetEnumerator() ??
        throw new InvalidOperationException("Unexpected type mismatch when obtaining enumerator");
}