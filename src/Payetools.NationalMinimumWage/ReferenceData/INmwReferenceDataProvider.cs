// Copyright (c) 2023-2025, Payetools Ltd.
//
// Payetools Ltd licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;

namespace Payetools.NationalMinimumWage.ReferenceData;

/// <summary>
/// Interface that classes implement in order to provide access to National Minimum/Living Wage reference data.
/// </summary>
public interface INmwReferenceDataProvider
{
    /// <summary>
    /// Gets the NMW/NLW levels for the specified tax year and tax period, as denoted by the supplied pay frequency
    /// and pay period.
    /// </summary>
    /// <param name="taxYear">Applicable tax year.</param>
    /// <param name="payFrequency">Applicable pay frequency.</param>
    /// <param name="taxPeriod">Applicable tax period.</param>
    /// <returns>An instance of <see cref="INmwLevelSet"/> containing the levels for the specified point
    /// in time.</returns>
    INmwLevelSet GetNmwLevelsForTaxYearAndPeriod(TaxYear taxYear, PayFrequency payFrequency, int taxPeriod);
}