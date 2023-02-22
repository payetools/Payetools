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

namespace Paytools.NationalMinimumWage;

/// <summary>
/// Interface that represents factories that can generate <see cref="INmwEvaluator"/> implementations.
/// </summary>
public interface INmwEvaluatorFactory
{
    /// <summary>
    /// Gets a new <see cref="INmwEvaluator"/> based on the specified pay date and number of tax periods.  The pay date
    /// is provided in order to determine which set of levels to use, noting that these may (but rarely do) change in-year.
    /// </summary>
    /// <param name="payDate">Applicable pay date.</param>
    /// <returns>A new evaluator instance.</returns>
    INmwEvaluator GetEvaluator(PayDate payDate);

    /// <summary>
    /// Gets a new <see cref="INmwEvaluator"/> based on the specified tax year, pay frequency and pay period.  The tax
    /// year, pay frequency and pay period are provided in order to determine which set of levels to use, noting that
    /// these may (but rarely do) change in-year.
    /// </summary>
    /// <param name="taxYear">Applicable tax year.</param>
    /// <param name="payFrequency">Applicable pay frequency.</param>
    /// <param name="taxPeriod">Applicable tax period.</param>
    /// <returns>A new evaluator instance.</returns>
    INmwEvaluator GetEvaluator(TaxYear taxYear, PayFrequency payFrequency, int taxPeriod);
}
