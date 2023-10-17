// Copyright (c) 2023 Payetools Foundation.
//
// Payetools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Payetools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

using Payetools.Common.Model;
using Payetools.NationalMinimumWage.ReferenceData;

namespace Payetools.ReferenceData.NationalMinimumWage;

/// <summary>
/// Represents the set of NMW/NLW levels for a given tax year (and potentially pay frequency/pay period combination.
/// </summary>
public class NmwReferenceDataEntry : INmwLevelSet, IApplicableFromTill
{
    /// <summary>
    /// Gets the start date (i.e., the first full day) for applicability.
    /// </summary>
    public DateOnly ApplicableFrom { get; init; }

    /// <summary>
    /// Gets the end date (i.e., the last full day) for applicability.
    /// </summary>
    public DateOnly ApplicableTill { get; init; }

    /// <summary>
    /// Gets the NMW level for apprentices under 19 or apprentices aged 19 and over in the first year of their
    /// apprenticeship.
    /// </summary>
    public decimal ApprenticeLevel { get; init; }

    /// <summary>
    /// Gets the NMW level for employees under the age of 18 (but over the school leaving age).
    /// </summary>
    public decimal Under18Level { get; init; }

    /// <summary>
    /// Gets the NMW level for employees aged between 18 and 20.
    /// </summary>
    public decimal Age18To20Level { get; init; }

    /// <summary>
    /// Gets the NMW level for employees aged between 21 and 22.
    /// </summary>
    public decimal Age21To22Level { get; init; }

    /// <summary>
    /// Gets the NLW (rather than NMW) level for employees aged 23 and over.
    /// </summary>
    public decimal Age23AndAboveLevel { get; init; }
}
