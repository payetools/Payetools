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

public record TaxPeriodBandwidthEntry : TaxBandwidthEntry
{
    public int EntryIndex { get; init; }
    public PayFrequency PayFrequency { get; init; }
    public int TaxPeriod { get; init; }
    public decimal Period1CumulativeBandwidth { get; init; }
    public decimal Period1CumulativeTax { get; init; }
    public new TaxPeriodBandwidthEntry? BandWidthEntryBelow { get; init; }

    public TaxPeriodBandwidthEntry(int index,
        TaxBandwidthEntry annualEntry, 
        PayFrequency payFrequency, 
        int taxPeriod, 
        TaxPeriodBandwidthEntry? periodEntryBelow)
        : base(annualEntry)
    {
        EntryIndex = index;
        PayFrequency = payFrequency;
        TaxPeriod = taxPeriod;

        var payPeriodCount = payFrequency.GetStandardTaxPeriodCount();

        decimal periodFactor = (decimal)taxPeriod / payFrequency.GetStandardTaxPeriodCount();

        CumulativeBandwidth = ApplyRounding(annualEntry.CumulativeBandwidth * periodFactor);
        CumulativeTax = ApplyRounding(annualEntry.CumulativeTax * periodFactor);
        TaxForBand = ApplyRounding(annualEntry.TaxForBand * periodFactor);

        Period1CumulativeBandwidth = ApplyRounding(annualEntry.CumulativeBandwidth / payPeriodCount);
        Period1CumulativeTax = ApplyRounding(annualEntry.CumulativeTax / payPeriodCount);

        BandWidthEntryBelow = periodEntryBelow;
    }

    // The HMRC specification calls for rounding down to 4 dp.  However, due to decimal arithmetic precision
    // limits, results like 1.9999999999 arise which should really be treated as 2, so the inner Round function
    // ensures this treatment.
    private static decimal ApplyRounding(decimal value) =>
        decimal.Round(decimal.Round(value, 10), 4, MidpointRounding.ToZero);
}
