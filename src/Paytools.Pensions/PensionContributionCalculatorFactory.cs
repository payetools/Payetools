// Copyright (c) 2023 Paytools Foundation.
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

using Paytools.Common.Model;
using Paytools.Pensions.Model;
using Paytools.Pensions.ReferenceData;

namespace Paytools.Pensions;

/// <summary>
/// Factory to generate <see cref="IPensionContributionCalculator"/> implementations that are for a given pay date,
/// pay frequency, earnings basis and tax treatment.
/// </summary>
public class PensionContributionCalculatorFactory : IPensionContributionCalculatorFactory
{
    private readonly IPensionsReferenceDataProvider _provider;

    /// <summary>
    /// Initialises a new instance of <see cref="PensionContributionCalculatorFactory"/> with the supplied reference data provider.
    /// </summary>
    /// <param name="provider">Reference data provider for pensions.</param>
    public PensionContributionCalculatorFactory(IPensionsReferenceDataProvider provider)
    {
        _provider = provider;
    }

    /// <summary>
    /// Gets a new <see cref="IPensionContributionCalculator"/> based on the specified pay date and number of tax periods.  The pay date
    /// is provided in order to determine which set of thresholds (Qualifying Earnings only) and rates to use, noting that these may change
    /// in-year.
    /// </summary>
    /// <param name="earningsBasis">Earnings basis for pension calculation (Qualifying Earnings vs Pensionable Pay Set x.</param>
    /// <param name="taxTreatment">Tax treatment (net pay arrangement vs relief at source).</param>
    /// <param name="payDate">Applicable pay date.</param>
    /// <param name="basicRateOfTax">Basic rate of tax.  Optional.  [NOT YET IMPLEMENTED].</param>
    /// <returns>A new calculator instance.</returns>
    /// <exception cref="ArgumentException">Thrown if an unsupported earnings basis is provided.</exception>
    public IPensionContributionCalculator GetCalculator(
        EarningsBasis earningsBasis,
        PensionTaxTreatment taxTreatment,
        PayDate payDate,
        decimal? basicRateOfTax = null) =>
        GetCalculator(earningsBasis, taxTreatment, payDate.TaxYear, payDate.PayFrequency, payDate.TaxPeriod, basicRateOfTax);

    /// <summary>
    /// Gets a new <see cref="IPensionContributionCalculator"/> based on the specified tax year, pay frequency and pay period, along with the
    /// applicable number of tax periods.  The tax year, pay frequency and pay period are provided in order to determine which
    /// set of thresholds and rates to use, noting that these may change in-year.
    /// </summary>
    /// <param name="earningsBasis">Earnings basis for pension calculation (Qualifying Earnings vs Pensionable Pay Set x.</param>
    /// <param name="taxTreatment">Tax treatment (net pay arrangement vs relief at source).</param>
    /// <param name="taxYear">Applicable tax year.</param>
    /// <param name="payFrequency">Applicable pay frequency.</param>
    /// <param name="taxPeriod">Applicable tax period.</param>
    /// <param name="basicRateOfTax">Basic rate of tax.  Optional.  [NOT YET IMPLEMENTED].</param>
    /// <returns>A new calculator instance.</returns>
    /// <exception cref="ArgumentException">Thrown if an unsupported earnings basis is provided.</exception>
    public IPensionContributionCalculator GetCalculator(
        EarningsBasis earningsBasis,
        PensionTaxTreatment taxTreatment,
        TaxYear taxYear,
        PayFrequency payFrequency,
        int taxPeriod,
        decimal? basicRateOfTax = null)
    {
        switch (earningsBasis)
        {
            case EarningsBasis.QualifyingEarnings:
                var thresholdsForPeriod = _provider.GetThresholdsForQualifyingEarnings(taxYear, payFrequency, taxPeriod);
                return new QualifyingEarningsContributionsCalculator(taxTreatment, thresholdsForPeriod.LowerLimit, thresholdsForPeriod.UpperLimit, basicRateOfTax);

            case EarningsBasis.PensionablePaySet1:
            case EarningsBasis.PensionablePaySet2:
            case EarningsBasis.PensionablePaySet3:
                return new PensionablePaySetCalculator(earningsBasis, taxTreatment, basicRateOfTax);

            default:
                throw new ArgumentException($"Unrecognised value for earnings basis '{earningsBasis}'", nameof(earningsBasis));
        }
    }
}