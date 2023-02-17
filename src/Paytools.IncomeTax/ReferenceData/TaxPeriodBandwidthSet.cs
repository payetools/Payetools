// Copyright (c) 2023 Paytools Foundation.  All rights reserved.
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

using Paytools.Common.Extensions;
using Paytools.Common.Model;

namespace Paytools.IncomeTax.ReferenceData;

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