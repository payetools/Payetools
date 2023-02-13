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

namespace Paytools.IncomeTax.ReferenceData;

public record TaxPeriodBandwidthSet
{
    public readonly PayFrequency PayFrequency;
    public readonly TaxPeriodBandwidthEntry[] TaxBandwidthEntries;
    public readonly int TaxPeriod;

    public TaxPeriodBandwidthSet(TaxBandwidthSet annualTaxBandwidthSet, PayFrequency payFrequency, int taxPeriod)
    {
        PayFrequency = payFrequency;
        TaxPeriod = taxPeriod;

        var annualEntries = annualTaxBandwidthSet.TaxBandwidthEntries;
        TaxBandwidthEntries = new TaxPeriodBandwidthEntry[annualEntries.Length];

        for (int index = 0; index < annualEntries.Length; index++)
            TaxBandwidthEntries[index] = new TaxPeriodBandwidthEntry(annualEntries[index], payFrequency, taxPeriod,
                index > 0 ? TaxBandwidthEntries[index - 1] : null);
    }
}
