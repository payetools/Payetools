// Copyright (c) 2023-2025, Payetools Ltd.
//
// Payetools Ltd licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;

namespace Payetools.NationalMinimumWage;

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