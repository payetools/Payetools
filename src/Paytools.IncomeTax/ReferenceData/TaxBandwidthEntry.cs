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

/// <summary>
/// Represents a single tax bandwidth entry for a full tax year.  For example, in the UK for 2021-2022, the starter rate of tax
/// for Scottish taxpayers was 19%, from zero taxable earnings up to £2,097.  This information constitutes a single <see cref="TaxBandwidthEntry"/>.
/// </summary>
public record TaxBandwidthEntry
{
    /// <summary>
    /// Gets the descriptive text associated with this TaxBandwidthEntry.
    /// </summary>
    public string Description { get;  }

    /// <summary>
    /// Gets the bandwidth of this TaxBandwidthEntry, i.e., the upper threshold less any lower threshold.
    /// </summary>
    public decimal Bandwidth => CumulativeBandwidth - (BandWidthEntryBelow?.CumulativeBandwidth ?? 0.0m);

    /// <summary>
    /// Gets or sets the cumulative bandwidth of this TaxBandwidthEntry, i.e., the sum of this bandwidth and all bandwidths below.
    /// </summary>
    public decimal CumulativeBandwidth { get; protected set; }

    /// <summary>
    /// Gets the applicable tax rate for this TaxBandwidthEntry.
    /// </summary>
    public decimal Rate { get; }

    /// <summary>
    /// Gets or sets the tax due in a single tax year for just this TaxBandwidthEntry if it is fully used.
    /// </summary>
    public decimal TaxForBand { get; protected set; }

    /// <summary>
    /// Gets or sets the tax due in a single tax year for this TaxBandwidthEntry and all bandwidths below, assuming they are all fully used.
    /// </summary>
    public decimal CumulativeTax { get; protected set; }

    /// <summary>
    /// Gets a value indicating whether this TaxBandwidthEntry represents the top band of tax for this tax regime.
    /// </summary>
    public bool IsTopBand { get; }

    /// <summary>
    /// Gets the <see cref="TaxBandwidthEntry"/> that is immediately below this TaxBandwidthEntry, or null if this is the lowest TaxBandwidthEntry
    /// for this tax regime.
    /// </summary>
    public TaxBandwidthEntry? BandWidthEntryBelow { get; }

    /// <summary>
    /// Initialises a new instance of <see cref="TaxBandwidthEntry"/> with the supplied parameters.
    /// </summary>
    /// <param name="bandDescription">Descriptive text associated with this TaxBandwidthEntry.</param>
    /// <param name="taxBandRate">Applicable tax rate for this TaxBandwidthEntry.</param>
    /// <param name="taxBandTo">Upper threshold for this TaxBandwidthEntry.</param>
    /// <param name="isTopRate">True if this is the top TaxBandwidthEntry for the tax regime in question.</param>
    /// <param name="entryBelow">TaxBandwidthEntry immediately below this TaxBandwidthEntry, if applicable; null otherwise.</param>
    public TaxBandwidthEntry(
        string bandDescription,
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