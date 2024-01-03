// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Payetools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

using Payetools.Common.Model;

namespace Payetools.IncomeTax.ReferenceData;

/// <summary>
/// Represents a single tax bandwidth entry for a portion of a tax year.  For example, in the UK for 2021-2022, the starter rate of tax
/// for Scottish taxpayers was 19%, from zero taxable earnings up to £2,097.  In simple terms, this bandwidth is distributed evenly across
/// the tax year, so in tax period 1 for a monthly payroll, the bandwidth would be £2,097 / 12 = £174.75.  All other parameters are
/// adjusted accordingly in line with the tax period.  This information constitutes a single <see cref="TaxPeriodBandwidthEntry"/>.
/// </summary>
public record TaxPeriodBandwidthEntry : TaxBandwidthEntry
{
    /// <summary>
    /// Gets the numeric index of this <see cref="TaxPeriodBandwidthEntry"/> within its <see cref="TaxPeriodBandwidthSet"/>.  Zero-based,
    /// with 0 being the lowest tax band.
    /// </summary>
    public int EntryIndex { get; }

    /// <summary>
    /// Gets the cumulative bandwidth value for tax period 1.
    /// </summary>
    public decimal Period1CumulativeBandwidth { get; }

    /// <summary>
    /// Gets the cumulative tax value for tax period 1.
    /// </summary>
    public decimal Period1CumulativeTax { get; }

    /// <summary>
    /// Gets the <see cref="TaxPeriodBandwidthEntry"/> that is immediately below this TaxPeriodBandwidthEntry, or null if this is
    /// the lowest TaxPeriodBandwidthEntry for this tax regime.
    /// </summary>
    public new TaxPeriodBandwidthEntry? BandWidthEntryBelow { get; }

    /// <summary>
    /// Initialises a new instance of <see cref="TaxPeriodBandwidthEntry"/> with the supplied parameters.
    /// </summary>
    /// <param name="index">Zero-based numeric index of this <see cref="TaxPeriodBandwidthEntry"/> within its
    /// <see cref="TaxPeriodBandwidthSet"/>.</param>
    /// <param name="annualEntry">Corresponding annual tax bandwidth entry, i.e., <see cref="TaxBandwidthEntry"/>.</param>
    /// <param name="payFrequency">Pay frequency for this TaxPeriodBandwidthEntry.</param>
    /// <param name="taxPeriod">Tax period, e.g., 1-12 for monthly.</param>
    /// <param name="periodEntryBelow"><see cref="TaxPeriodBandwidthEntry"/> immediately below this one, or null if this is the lowest band.</param>
    public TaxPeriodBandwidthEntry(
        int index,
        TaxBandwidthEntry annualEntry,
        PayFrequency payFrequency,
        int taxPeriod,
        TaxPeriodBandwidthEntry? periodEntryBelow)
        : base(annualEntry)
    {
        EntryIndex = index;

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
