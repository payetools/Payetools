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

    public ITaxCalculator GetCalculator(CountriesForTaxPurposes applicableCountries, PayDate payDate, PayFrequency payFrequency) =>
        GetCalculator(applicableCountries, payDate.TaxYear, payDate.TaxPeriod, payFrequency);

    public ITaxCalculator GetCalculator(CountriesForTaxPurposes applicableCountries, TaxYear taxYear, int taxPeriod, PayFrequency payFrequency)
    {
        var taxBandwidthSets = _taxBandProvider.GetBandsForTaxYear(taxYear);

        if (!taxBandwidthSets.TryGetValue(applicableCountries, out var taxBandwidthSet))
            throw new InvalidReferenceDataException($"Unable to find unique tax bands for countries/tax year combination [{applicableCountries}] {taxYear}");

        return new TaxCalculator(taxBandwidthSet, payFrequency, taxPeriod);
    }
}