// Copyright (c) 2023 Paytools Foundation.
//
// Paytools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Paytools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

using Paytools.Common.Model;
using Paytools.NationalInsurance.ReferenceData;

namespace Paytools.NationalInsurance;

/// <summary>
/// Factory to generate <see cref="INiCalculatorFactory"/> implementations that are for a given pay date.
/// </summary>
public class NiCalculatorFactory : INiCalculatorFactory
{
    private INiReferenceDataProvider _niReferenceDataProvider;

    /// <summary>
    /// Initialises a new instance of <see cref="NiCalculatorFactory"/> with the supplied <see cref="INiReferenceDataProvider"/>.
    /// </summary>
    /// <param name="niReferenceDataProvider">Reference data provider used to seed new NI calculators.</param>
    public NiCalculatorFactory(INiReferenceDataProvider niReferenceDataProvider)
    {
        _niReferenceDataProvider = niReferenceDataProvider;
    }

    /// <summary>
    /// Gets a new <see cref="INiCalculator"/> based on the specified pay date and number of tax periods.  The pay date
    /// is provided in order to determine which set of thresholds and rates to use, noting that these may change in-year.
    /// </summary>
    /// <param name="payDate">Applicable pay date.</param>
    /// <param name="numberOfTaxPeriods">Number of tax periods applicable, usually 1.  Defaults to 1.</param>
    /// <returns>A new calculator instance.</returns>
    public INiCalculator GetCalculator(PayDate payDate, int numberOfTaxPeriods = 1) =>
        GetCalculator(payDate.TaxYear, payDate.PayFrequency, payDate.TaxPeriod, numberOfTaxPeriods);

    /// <summary>
    /// Gets a new <see cref="INiCalculator"/> based on the specified tax year, pay frequency and pay period, along with the
    /// applicable number of tax periods.  The tax year, pay frequency and pay period are provided in order to determine which
    /// set of thresholds and rates to use, noting that these may change in-year.
    /// </summary>
    /// <param name="taxYear">Applicable tax year.</param>
    /// <param name="payFrequency">Applicable pay frequency.</param>
    /// <param name="taxPeriod">Applicable tax period.</param>
    /// <param name="numberOfTaxPeriods">Number of tax periods applicable, usually 1.  Defaults to 1.</param>
    /// <returns>A new calculator instance.</returns>
    public INiCalculator GetCalculator(TaxYear taxYear, PayFrequency payFrequency, int taxPeriod, int numberOfTaxPeriods = 1)
    {
        var annualThresholds = _niReferenceDataProvider.GetNiThresholdsForTaxYearAndPeriod(taxYear, payFrequency, taxPeriod);
        var periodThresholds = new NiPeriodThresholdSet(annualThresholds, payFrequency, numberOfTaxPeriods);
        var rates = _niReferenceDataProvider.GetNiRatesForTaxYearAndPeriod(taxYear, payFrequency, taxPeriod);
        var directorsRates = _niReferenceDataProvider.GetDirectorsNiRatesForTaxYearAndPeriod(taxYear, payFrequency, taxPeriod);

        return new NiCalculator(annualThresholds, periodThresholds, rates, directorsRates);
    }
}