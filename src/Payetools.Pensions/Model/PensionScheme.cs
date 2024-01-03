// Copyright (c) 2023-2024, Payetools Foundation.
//
// Paytools Foundation licenses this file to you under one of the following licenses:
//
//   * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//   * Paytools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

using Payetools.Common.Model;

namespace Payetools.Pensions.Model;

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
