// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Payetools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

using Payetools.Common.Model;
using Payetools.NationalMinimumWage.ReferenceData;

namespace Payetools.NationalMinimumWage;

/// <summary>
/// Factory to generate <see cref="INmwEvaluator"/> implementations that are for a given pay date.
/// </summary>
public class NmwEvaluatorFactory : INmwEvaluatorFactory
{
    private readonly INmwReferenceDataProvider _provider;

    /// <summary>
    /// Initialises a new instance of <see cref="NmwEvaluatorFactory"/> with the supplied <see cref="INmwReferenceDataProvider"/>.
    /// </summary>
    /// <param name="provider">Reference data provider used to seed new NMW/NLW evaluators.</param>
    public NmwEvaluatorFactory(INmwReferenceDataProvider provider)
    {
        _provider = provider;
    }

    /// <summary>
    /// Gets a new <see cref="INmwEvaluator"/> based on the specified pay date.  The pay date is provided in order to determine
    /// which set of level to use, noting that these may (but rarely do) change in-year.
    /// </summary>
    /// <param name="payDate">Applicable pay date.</param>
    /// <returns>A new evaluator instance.</returns>
    public INmwEvaluator GetEvaluator(PayDate payDate) =>
        GetEvaluator(payDate.TaxYear, payDate.PayFrequency, payDate.TaxPeriod);

    /// <summary>
    /// Gets a new <see cref="INmwEvaluator"/> based on the specified tax year, pay frequency and pay period, along with the
    /// applicable number of tax periods.  The tax year, pay frequency and pay period are provided in order to determine which
    /// set of thresholds and rates to use, noting that these may change in-year.
    /// </summary>
    /// <param name="taxYear">Applicable tax year.</param>
    /// <param name="payFrequency">Applicable pay frequency.</param>
    /// <param name="taxPeriod">Applicable tax period.</param>
    /// <returns>A new evaluator instance.</returns>
    public INmwEvaluator GetEvaluator(TaxYear taxYear, PayFrequency payFrequency, int taxPeriod) =>
        new NmwEvaluator(_provider.GetNmwLevelsForTaxYearAndPeriod(taxYear, payFrequency, taxPeriod));
}