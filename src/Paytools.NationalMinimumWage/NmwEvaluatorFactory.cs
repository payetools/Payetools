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
using Paytools.NationalMinimumWage.ReferenceData;

namespace Paytools.NationalMinimumWage;

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