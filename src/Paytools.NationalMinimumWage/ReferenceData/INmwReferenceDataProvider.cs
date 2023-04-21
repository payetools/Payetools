// Copyright (c) 2023 Paytools Foundation.
//
// Paytools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Paytools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

using Paytools.Common.Model;

namespace Paytools.NationalMinimumWage.ReferenceData;

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