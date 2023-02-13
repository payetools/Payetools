using Paytools.IncomeTax.ReferenceData;

namespace Paytools.ReferenceData;
public class Class1
{
    //public TaxBandwidthSet(TaxYearEntry taxYearEntry)
    //{
    //    TaxBandwidthEntries = new TaxBandwidthEntry[taxYearEntry.TaxBands.Length];

    //    for (int i = 0; i < taxYearEntry.TaxBands.Length; i++)
    //        TaxBandwidthEntries[i] = new TaxBandwidthEntry(taxYearEntry.TaxBands[i], i > 0 ? TaxBandwidthEntries[i - 1] : null);
    //}

    //public TaxBandwidthEntry(HmrcDeductionBand taxBand, TaxBandwidthEntry? entryBelow)
    //{
    //    Description = taxBand.Description;

    //    Rate = taxBand.Rate;
    //    CumulativeBandwidth = taxBand.To ?? 0.0m;

    //    var bandwith = CumulativeBandwidth - (entryBelow?.CumulativeBandwidth ?? 0.0m);
    //    TaxForBand = bandwith * taxBand.Rate;

    //    CumulativeTax = taxBand.IsTopRate ? 0.0m : TaxForBand + (entryBelow?.CumulativeTax ?? 0.0m);
    //    IsTopBand = taxBand.IsTopRate;

    //    BandWidthEntryBelow = entryBelow;
    //}

}
