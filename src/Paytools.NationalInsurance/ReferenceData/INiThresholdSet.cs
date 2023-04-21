// Copyright (c) 2023 Paytools Foundation.
//
// Paytools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Paytools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

using Paytools.NationalInsurance.Model;

namespace Paytools.NationalInsurance.ReferenceData;

/// <summary>
/// Interface for types that represent a list of National Insurance thresholds as defined by HMRC for a given
/// tax year or portion of a tax year.  The list isn't specifically ordered; the items within the list provide
/// their own mapping to <see cref="NiThresholdType"/> values.
/// </summary>
public interface INiThresholdSet : IEnumerable<INiThresholdEntry>
{
    /// <summary>
    /// Gets the <see cref="INiThresholdEntry"/> at the specified index.
    /// </summary>
    /// <param name="index">Zero-based index into list.</param>
    /// <returns>The <see cref="INiThresholdEntry"/> for the specified index.</returns>
    INiThresholdEntry this[int index] { get; }

    /// <summary>
    /// Gets the number of threshold value this threshold set contains.
    /// </summary>
    int Count { get; }

    /// <summary>
    /// Gets the annual threshold for the period for the specified threshold type.
    /// </summary>
    /// <param name="thresholdType">Applicable threshold (e.g., LEL, UEL, PT).</param>
    /// <returns>Annual threshold value applicable to threshold type.</returns>
    public decimal GetThreshold(NiThresholdType thresholdType);
}
