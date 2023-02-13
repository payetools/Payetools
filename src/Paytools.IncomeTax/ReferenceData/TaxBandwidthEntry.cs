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

namespace Paytools.IncomeTax.ReferenceData;

public record TaxBandwidthEntry
{
    public string Description { get; init; }
    public decimal CumulativeBandwidth { get; init; }
    public decimal Rate { get; init; }
    public decimal TaxForBand { get; init; }
    public decimal CumulativeTax { get; init; }
    public bool IsTopBand { get; init; }
    public TaxBandwidthEntry? BandWidthEntryBelow { get; init; }

    public TaxBandwidthEntry(string bandDescription, 
        decimal taxBandRate, 
        decimal? taxBandTo,
        bool isTopRate,
        TaxBandwidthEntry? entryBelow)
    {
        Description = bandDescription;

        Rate = taxBandRate;
        CumulativeBandwidth = taxBandTo ?? 0.0m;

        var bandwith = CumulativeBandwidth - (entryBelow?.CumulativeBandwidth ?? 0.0m);
        TaxForBand = bandwith * taxBandRate;

        CumulativeTax = isTopRate ? 0.0m : TaxForBand + (entryBelow?.CumulativeTax ?? 0.0m);
        IsTopBand = isTopRate;

        BandWidthEntryBelow = entryBelow;
    }
}