// Copyright (c) 2023 Paytools Foundation.  All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License") ~
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

using Paytools.Common.Diagnostics;
using Paytools.Common.Model;
using Paytools.IncomeTax.ReferenceData;

namespace Paytools.IncomeTax;

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

        return new TaxCalculator(taxBandwidthSet, payFrequency, taxPeriod);
    }
}