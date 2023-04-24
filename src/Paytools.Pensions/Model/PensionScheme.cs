// Copyright (c) 2023 Paytools Foundation.
//
// Paytools Foundation licenses this file to you under one of the following licenses:
//
//   * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//   * Paytools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

using Paytools.Common.Model;

namespace Paytools.Pensions.Model;

/// <summary>
/// Represents a workplace pension scheme.
/// </summary>
public record PensionScheme : IPensionScheme
{
    /// <summary>
    /// Gets the earnings basis for this pension scheme.
    /// </summary>
    public PensionsEarningsBasis EarningsBasis { get; init; }

    /// <summary>
    /// Gets the tax treatment for this pension scheme.
    /// </summary>
    public PensionTaxTreatment TaxTreatment { get; init; }
}
