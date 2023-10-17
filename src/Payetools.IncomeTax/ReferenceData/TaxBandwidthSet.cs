// Copyright (c) 2023 Payetools Foundation.
//
// Payetools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Payetools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

namespace Payetools.IncomeTax.ReferenceData;

/// <summary>
/// Represents a set of tax bandwidths for a given tax regime for a given tax year.
/// </summary>
public record TaxBandwidthSet
{
    /// <summary>
    /// Gets the set of <see cref="TaxBandwidthEntry"/>'s for this TaxBandwidthSet as an array.
    /// </summary>
    public TaxBandwidthEntry[] TaxBandwidthEntries { get; }

    /// <summary>
    /// Initialises a new instance of <see cref="TaxBandwidthSet"/>.
    /// </summary>
    /// <param name="taxBandwidthEntries">Array of <see cref="TaxBandwidthEntry"/>'s for this tax year/regime combination.</param>
    public TaxBandwidthSet(TaxBandwidthEntry[] taxBandwidthEntries)
    {
        TaxBandwidthEntries = taxBandwidthEntries;
    }
}