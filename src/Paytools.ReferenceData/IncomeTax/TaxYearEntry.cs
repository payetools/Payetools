// Copyright (c) 2023 Paytools Foundation
//
// Licensed under the Apache License, Version 2.0 (the "License")~
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

public class TaxYearEntry
{
    public CountriesForTaxPurposes ApplicableCountries { get; init; }
    public ImmutableArray<PersonalAllowance> PersonalAllowances { get; init; } = ImmutableArray<PersonalAllowance>.Empty;
    public ImmutableArray<HmrcDeductionBand> TaxBands { get; init; } = ImmutableArray<HmrcDeductionBand>.Empty;

    public TaxBandwidthEntry[] GetTaxBandwidthEntries()
    {
        var taxBandwidthEntries = new TaxBandwidthEntry[TaxBands.Length];

        for (int i = 0; i < TaxBands.Length; i++)
            taxBandwidthEntries[i] = new TaxBandwidthEntry(TaxBands[i].Description,
                TaxBands[i].Rate,
                TaxBands[i].To,
                TaxBands[i].IsTopRate,
                i > 0 ? taxBandwidthEntries[i - 1] : null);

        return taxBandwidthEntries;
    }
}