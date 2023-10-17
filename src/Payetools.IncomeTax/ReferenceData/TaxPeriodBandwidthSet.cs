// Copyright (c) 2023 Payetools Foundation.
//
// Payetools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Payetools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

using Payetools.Common.Extensions;
using Payetools.Common.Model;

namespace Payetools.IncomeTax.ReferenceData;

/// <summary>
/// Represents a set of tax bandwidths for a given tax regime for a given tax year, pro-rated for a given tax period.  For
/// Example, for weekly payrolls and relating to tax period one, the cumulative thresholds and tax values are calculated as
/// 1/52th of the annual amount.
/// </summary>
public record TaxPeriodBandwidthSet
{
    /// <summary>
    /// Gets the set of tax bandwidth entries for the given tax regime, tax year and tax period/pay frequency
    /// combination, as an array of <see cref="TaxPeriodBandwidthEntry"/>'s.
    /// </summary>
    public TaxPeriodBandwidthEntry[] TaxBandwidthEntries { get; }

    /// <summary>
    /// Gets the relevant tax period for this <see cref="TaxPeriodBandwidthSet"/>.
    /// </summary>
    public int TaxPeriod { get; }

    /// <summary>
    /// Gets the applicable pay frequency for this <see cref="TaxPeriodBandwidthSet"/>.
    /// </summary>
    public PayFrequency PayFrequency { get; }

    /// <summary>
    /// Initialises a new instance of <see cref="TaxPeriodBandwidthSet"/> with the supplied parameters.
    /// </summary>
    /// <param name="annualTaxBandwidthSet">Tax bandwidth set for the full tax year.</param>
    /// <param name="payFrequency">Applicable pay frequency.</param>
    /// <param name="taxPeriod">Relevant tax period.</param>
    public TaxPeriodBandwidthSet(TaxBandwidthSet annualTaxBandwidthSet, PayFrequency payFrequency, int taxPeriod)
    {
        PayFrequency = payFrequency;
        TaxPeriod = taxPeriod;

        var annualEntries = annualTaxBandwidthSet.TaxBandwidthEntries;
        TaxBandwidthEntries = new TaxPeriodBandwidthEntry[annualEntries.Length];

        annualEntries.WithIndex().ToList().ForEach(tbe =>
        {
            var tbeBelow = tbe.Index > 0 ? TaxBandwidthEntries[tbe.Index - 1] : null;

            TaxBandwidthEntries[tbe.Index] = new TaxPeriodBandwidthEntry(tbe.Index, tbe.Value, payFrequency, taxPeriod, tbeBelow);
        });
    }
}