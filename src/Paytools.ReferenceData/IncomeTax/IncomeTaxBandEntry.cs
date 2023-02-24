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

using Paytools.Common.Model;
using Paytools.IncomeTax.ReferenceData;
using System.Collections.Immutable;

namespace Paytools.ReferenceData.IncomeTax;

/// <summary>
/// Record that represents a set of tax bands for a given tax regime (as specified by the <see cref="ApplicableCountries"/> property).
/// </summary>
public record IncomeTaxBandEntry
{
    /// <summary>
    /// Gets the set of countries within the UK that this set of tax bands refer to.
    /// </summary>
    public CountriesForTaxPurposes ApplicableCountries { get; init; }

    /// <summary>
    /// Gets the set of personal allowances applicable to this tax regime.
    /// </summary>
    public ImmutableArray<PersonalAllowance> PersonalAllowances { get; init; } = ImmutableArray<PersonalAllowance>.Empty;

    /// <summary>
    /// Gets the set of tax bands applicable.
    /// </summary>
    public ImmutableArray<IncomeTaxDeductionBand> TaxBands { get; init; } = ImmutableArray<IncomeTaxDeductionBand>.Empty;

    /// <summary>
    /// Gets an array of <see cref="TaxBandwidthEntry"/>s that correspond to the elements of the <see cref="TaxBands"/> property.
    /// </summary>
    /// <returns>Tax bandwidth entries as an array of <see cref="TaxBandwidthEntry"/>s.</returns>
    public TaxBandwidthEntry[] GetTaxBandwidthEntries()
    {
        var taxBandwidthEntries = new TaxBandwidthEntry[TaxBands.Length];

        // NB Can't easily use LINQ due to the need to refer to the previous item (although see Jon Skeet's StackOverflow post:
        // https://stackoverflow.com/questions/3683105/calculate-difference-from-previous-item-with-linq/3683217#3683217)
        for (int i = 0; i < TaxBands.Length; i++)
        {
            taxBandwidthEntries[i] = new TaxBandwidthEntry(TaxBands[i].Description,
                TaxBands[i].Rate,
                TaxBands[i].To,
                TaxBands[i].IsTopRate,
                i > 0 ? taxBandwidthEntries[i - 1] : null);
        }

        return taxBandwidthEntries;
    }
}