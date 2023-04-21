// Copyright (c) 2023 Paytools Foundation.
//
// Paytools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Paytools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

namespace Paytools.NationalMinimumWage.ReferenceData;

/// <summary>
/// Interface for types that detail the various HMRC-published National Minimum/Living Wage (NMW/NLW) levels.
/// </summary>
public interface INmwLevelSet
{
    /// <summary>
    /// Gets the NMW level for apprentices under 19 or apprentices aged 19 and over in the first year of their
    /// apprenticeship.
    /// </summary>
    decimal ApprenticeLevel { get; }

    /// <summary>
    /// Gets the NMW level for employees under the age of 18 (but over the school leaving age).
    /// </summary>
    decimal Under18Level { get; }

    /// <summary>
    /// Gets the NMW level for employees aged between 18 and 20.
    /// </summary>
    decimal Age18To20Level { get; }

    /// <summary>
    /// Gets the NMW level for employees aged between 21 and 22.
    /// </summary>
    decimal Age21To22Level { get; }

    /// <summary>
    /// Gets the NLW (rather than NMW) level for employees aged 23 and over.
    /// </summary>
    decimal Age23AndAboveLevel { get; }
}