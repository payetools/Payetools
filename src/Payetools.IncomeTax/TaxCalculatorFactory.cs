// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Diagnostics;
using Payetools.Common.Model;
using Payetools.IncomeTax.ReferenceData;

namespace Payetools.IncomeTax;

/// <summary>
/// Factory to generate <see cref="ITaxCalculator"/> implementations that are for a given pay date and specific tax regime.
/// </summary>
public class TaxCalculatorFactory : ITaxCalculatorFactory
{
    private readonly ITaxReferenceDataProvider _taxBandProvider;

    /// <summary>
    /// Initialises a new instance of <see cref="TaxCalculatorFactory"/> using the supplied tax band provider.
    /// </summary>
    /// <param name="taxBandProvider">Tax band provider for providing access to tax bands for given tax years.</param>
    public TaxCalculatorFactory(ITaxReferenceDataProvider taxBandProvider)
    {
        _taxBandProvider = taxBandProvider;
    }

    /// <summary>
    /// Gets an instance of an <see cref="ITaxCalculator"/> for the specified tax regime and pay date.
    /// </summary>
    /// <param name="applicableCountries">Applicable tax regime.</param>
    /// <param name="payDate">Pay date.</param>
    /// <returns>Instance of <see cref="ITaxCalculator"/> for the specified tax regime and pay date.</returns>
    /// <exception cref="InvalidReferenceDataException">Thrown if it was not possible to find a tax bandwidth set for the specified
    /// tax regime and tax year combination.</exception>
    public ITaxCalculator GetCalculator(CountriesForTaxPurposes applicableCountries, PayDate payDate) =>
        GetCalculator(applicableCountries, payDate.TaxYear, payDate.PayFrequency, payDate.TaxPeriod);

    /// <summary>
    /// Gets an instance of an <see cref="ITaxCalculator"/> for the specified tax regime, tax year, tax period and pay frequency.
    /// </summary>
    /// <param name="applicableCountries">Applicable tax regime.</param>
    /// <param name="taxYear">Relevant tax year.</param>
    /// <param name="payFrequency">Applicable pay frequency.</param>
    /// <param name="taxPeriod">Applicable tax period.</param>
    /// <returns>Instance of <see cref="ITaxCalculator"/> for the specified tax regime, tax year and period and pay frequency.</returns>
    /// <exception cref="InvalidReferenceDataException">Thrown if it was not possible to find a tax bandwidth set for the specified
    /// tax regime and tax year combination.</exception>
    public ITaxCalculator GetCalculator(CountriesForTaxPurposes applicableCountries, TaxYear taxYear, PayFrequency payFrequency, int taxPeriod)
    {
        var taxBandwidthSets = _taxBandProvider.GetTaxBandsForTaxYearAndPeriod(taxYear, payFrequency, taxPeriod);

        if (!taxBandwidthSets.TryGetValue(applicableCountries, out var taxBandwidthSet))
            throw new InvalidReferenceDataException($"Unable to find unique tax bands for countries/tax year combination [{applicableCountries}] {taxYear}");

        return new TaxCalculator(taxYear, applicableCountries, taxBandwidthSet, payFrequency, taxPeriod);
    }
}