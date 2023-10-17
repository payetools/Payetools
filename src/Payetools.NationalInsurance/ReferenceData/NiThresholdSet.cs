// Copyright (c) 2023 Payetools Foundation.
//
// Paytools Foundation licenses this file to you under one of the following licenses:
//
//   * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//   * Paytools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

using Payetools.NationalInsurance.Model;
using System.Collections;
using System.Collections.Immutable;

namespace Payetools.NationalInsurance.ReferenceData;

/// <summary>
/// Represents a set of National Insurance thresholds as defined by HMRC for a given tax year or portion of a tax year.
/// </summary>
public class NiThresholdSet : INiThresholdSet
{
    private readonly ImmutableList<INiThresholdEntry> _niThresholds;

    /// <summary>
    /// Gets the number of threshold value this threshold set contains.
    /// </summary>
    public int Count => _niThresholds.Count;

    /// <summary>
    /// Gets the <see cref="INiThresholdEntry"/> at the specified index.
    /// </summary>
    /// <param name="index">Zero-based index into list.</param>
    /// <returns>The <see cref="INiThresholdEntry"/> for the specified index.</returns>
    public INiThresholdEntry this[int index]
    {
        get
        {
            if (index < 0 || index > _niThresholds.Count - 1)
                throw new ArgumentOutOfRangeException(nameof(index), "Invalid index value for retrieving NI threshold");

            return _niThresholds[index];
        }
    }

    /// <summary>
    /// Initialises a new instance of <see cref="NiThresholdSet"/>.
    /// </summary>
    /// <param name="niThresholds">Immutable input list of thresholds.</param>
    /// <exception cref="ArgumentException">Thrown if the number of thresholds supplied does not match the expected
    /// number of possible thresholds.</exception>
    public NiThresholdSet(ImmutableList<INiThresholdEntry> niThresholds)
    {
        int entryCount = (int)NiThresholdType.Count;

        if (niThresholds.Count != entryCount)
            throw new ArgumentException($"Expected {entryCount} threshold entries but only {niThresholds.Count} supplied in input list", nameof(niThresholds));

        _niThresholds = niThresholds;
    }

    /// <summary>
    /// Initialises a new instance of <see cref="NiThresholdSet"/> adjusted by the supplied prorata factor.  Used in director's NI
    /// calculation when an employee is a director for part of the year.
    /// </summary>
    /// <param name="originalAnnualNiThresholds">Original set of thresholds.</param>
    /// <param name="proRataFactor">Pro-rata factor to apply to each threshold.</param>
    public NiThresholdSet(INiThresholdSet originalAnnualNiThresholds, decimal proRataFactor)
    {
        // NB All pro-rata thresholds should be rounded up to the next whole pound.
        _niThresholds = originalAnnualNiThresholds
            .Select(t => new NiThresholdEntry()
            {
                ThresholdType = t.ThresholdType,
                ThresholdValuePerYear = decimal.Round(t.ThresholdValuePerYear * proRataFactor, MidpointRounding.ToPositiveInfinity)
            })
            .ToImmutableList<INiThresholdEntry>();
    }

    /// <summary>
    /// Gets the annual threshold for the period for the specified threshold type.
    /// </summary>
    /// <param name="thresholdType">Applicable threshold (e.g., LEL, UEL, PT).</param>
    /// <returns>Annual threshold value applicable to threshold type.</returns>
    public decimal GetThreshold(NiThresholdType thresholdType) =>
        _niThresholds[thresholdType.GetIndex()].ThresholdValuePerYear;

    /// <summary>
    /// Gets a string representation of this <see cref="NiPeriodThresholdSet"/>.
    /// </summary>
    /// <returns>String representation of this instance.</returns>
    public override string ToString() =>
        string.Join(" | ", _niThresholds.Select(te => te.ToString()).ToArray());

    /// <summary>
    /// Returns an enumerator that iterates through the thresholds (of type <see cref="INiThresholdEntry"/>).
    /// </summary>
    /// <returns>An enumerator that can be used to iterate through the collection of thresholds.</returns>
    /// <exception cref="InvalidOperationException">Throw if the enumerator cannot be obtained.  (Should never be thrown).</exception>
    public IEnumerator<INiThresholdEntry> GetEnumerator() =>
        (_niThresholds as IEnumerable<INiThresholdEntry>)?.GetEnumerator() ??
        throw new InvalidOperationException("Unexpected type mismatch when obtaining enumerator");

    /// <summary>
    /// Returns an enumerator that iterates through the thresholds (of type <see cref="INiThresholdEntry"/>).
    /// </summary>
    /// <returns>An enumerator that can be used to iterate through the collection of thresholds.</returns>
    /// <exception cref="InvalidOperationException">Throw if the enumerator cannot be obtained.  (Should never be thrown).</exception>
    IEnumerator IEnumerable.GetEnumerator() =>
        (_niThresholds as IEnumerable)?.GetEnumerator() ??
        throw new InvalidOperationException("Unexpected type mismatch when obtaining enumerator");
}