using Paytools.Common;
using Paytools.Common.Model;

namespace Paytools.IncomeTax;

public interface ITaxCalculatorFactory
{
    ITaxCalculator GetCalculator(CountriesForTaxPurposes applicableCountries, 
        PayDate payDate, PayFrequency payFrequency);
}
