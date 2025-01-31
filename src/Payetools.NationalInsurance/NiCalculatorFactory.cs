// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;
using Payetools.NationalInsurance.ReferenceData;

namespace Payetools.NationalInsurance;

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
    /// <param name="applyWeek53Treatment">Flag that indicates whether to apply "week 53" treatment, i.e., where
    /// there are 53 weeks in a tax year (or 27 periods in a two-weekly pay cycle, etc.).  Must be false
    /// for monthly, quarterly and annual payrolls.  Optional, defaulting to false.</param>
    /// <returns>A new calculator instance.</returns>
    public INiCalculator GetCalculator(PayDate payDate, int numberOfTaxPeriods = 1, bool applyWeek53Treatment = false)
    {
        var annualThresholds = _niReferenceDataProvider.GetNiThresholdsForPayDate(payDate);
        var periodThresholds = new NiPeriodThresholdSet(annualThresholds, payDate.PayFrequency, numberOfTaxPeriods);
        var rates = _niReferenceDataProvider.GetNiRatesForPayDate(payDate);
        var directorsRates = _niReferenceDataProvider.GetDirectorsNiRatesForPayDate(payDate);

        return new NiCalculator(annualThresholds, periodThresholds, rates, directorsRates,
            payDate.PayFrequency.IsLastTaxPeriodInTaxYear(payDate.TaxPeriod, applyWeek53Treatment));
    }
}