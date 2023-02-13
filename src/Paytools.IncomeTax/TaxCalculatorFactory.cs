using Paytools.Common;
using Paytools.Common.Diagnostics;
using Paytools.Common.Model;
using Paytools.IncomeTax.ReferenceData;

namespace Paytools.IncomeTax;

public class TaxCalculatorFactory : ITaxCalculatorFactory
{
    private readonly ITaxBandProvider _taxBandProvider;

    public TaxCalculatorFactory(ITaxBandProvider taxBandProvider)
    {
        _taxBandProvider = taxBandProvider;
    }

    public ITaxCalculator GetCalculator(CountriesForTaxPurposes applicableCountries, PayDate payDate, PayFrequency payFrequency)
    {
        var taxBandwidthSets = _taxBandProvider.GetBandsForTaxYear(payDate.TaxYear);

        if (!taxBandwidthSets.TryGetValue(applicableCountries, out var taxBandwidthSet))
            throw new InvalidReferenceDataException($"Unable to find unique tax bands for countries/tax year combination [{applicableCountries}] {payDate.TaxYear}");

        return GetTaxCalculator(taxBandwidthSet, payFrequency, payDate.TaxPeriod);
    }

    public ITaxCalculator GetTaxCalculator(TaxBandwidthSet annualTaxBandwidthSet, PayFrequency payFrequency, int taxPeriod)
    {
        return new TaxCalculator(annualTaxBandwidthSet, payFrequency, taxPeriod);
    }

    //public ITaxCalculator GetTaxCalculator(TaxBandwidthSet annualTaxBandwidthSet, int taxPeriod, int taxPeriodCount)
    //{
    //    return new TaxCalculator(annualTaxBandwidthSet, taxPeriod, taxPeriodCount);
    //}
}
