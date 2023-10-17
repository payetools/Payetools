// Copyright (c) 2023 Payetools Foundation.
//
// Payetools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Payetools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

using Payetools.Common.Model;
using System.Collections.ObjectModel;

namespace Payetools.NationalInsurance.ReferenceData;

/// <summary>
/// Interface that classes implement in order to provide access to National Insurance reference data, i.e.,
/// rates and thresholds.
/// </summary>
public interface INiReferenceDataProvider
{
    /// <summary>
    /// Gets the NI thresholds for the specified pay date.
    /// </summary>
    /// <param name="payDate">Applicable pay date.</param>
    /// <returns>An instance of <see cref="INiThresholdSet"/> containing the thresholds for the specified point
    /// in time.</returns>
    INiThresholdSet GetNiThresholdsForPayDate(PayDate payDate);

    /// <summary>
    /// Gets a read-only dictionary that maps <see cref="NiCategory"/> values to the set of rates to be applied
    /// for the specified pay date.
    /// </summary>
    /// <param name="payDate">Applicable pay date.</param>
    /// <returns>Read-only dictionary that maps <see cref="NiCategory"/> values to the appropriate set of rates for
    /// the specified point in time.</returns>
    ReadOnlyDictionary<NiCategory, INiCategoryRatesEntry> GetNiRatesForPayDate(PayDate payDate);

    /// <summary>
    /// Gets a read-only dictionary that maps <see cref="NiCategory"/> values to the set of rates to be applied
    /// for the specified pay date, for directors.  (For most tax years, this method returns null, but if
    /// there have been in-year changes, specific directors' rates may apply.)
    /// </summary>
    /// <param name="payDate">Applicable pay date.</param>
    /// <returns>Read-only dictionary that maps <see cref="NiCategory"/> values to the appropriate set of rates for
    /// the specified point in time.  If specific rates apply for directors, theses are returned, otherwise the
    /// regular employee/employer rates are returned.</returns>
    ReadOnlyDictionary<NiCategory, INiCategoryRatesEntry> GetDirectorsNiRatesForPayDate(PayDate payDate);
}