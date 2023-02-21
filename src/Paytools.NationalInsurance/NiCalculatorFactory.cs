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
    public INiCalculator GetCalculator(TaxYear taxYear, PayFrequency payFrequency, int taxPeriod, int numberOfTaxPeriods = 1) =>
        new NiCalculator(GetPeriodThresholdsForTaxYearAndPeriod(taxYear, payFrequency, taxPeriod, numberOfTaxPeriods),
            _niReferenceDataProvider.GetNiRatesForTaxYearAndPeriod(taxYear, payFrequency, taxPeriod));

    private NiPeriodThresholdSet GetPeriodThresholdsForTaxYearAndPeriod(
        TaxYear taxYear,
        PayFrequency payFrequency,
        int taxPeriod,
        int numberOfTaxPeriods)
    {
        var annualThresholds = _niReferenceDataProvider.GetNiThresholdsForTaxYearAndPeriod(taxYear, payFrequency, taxPeriod);

        return new NiPeriodThresholdSet(annualThresholds, payFrequency, numberOfTaxPeriods);
    }
}
