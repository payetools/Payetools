// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Payetools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

using Payetools.Common.Model;
using System.Collections.Immutable;

namespace Payetools.ReferenceData.NationalInsurance;

/// <summary>
/// Record that represents the employee NI rates to be applied at various earnings thresholds.
/// </summary>
public record NiEmployerRatesEntry
{
    /// <summary>
    /// Gets the list of applicable NI categories for this rates entry.
    /// </summary>
    public ImmutableList<NiCategory> NiCategories { get; init; } = default!;

    /// <summary>
    /// Gets the employer rate applicable for earnings between the Lower Earnings
    /// Limit and the Secondary Threshold.
    /// </summary>
    public decimal ForEarningsAtOrAboveLELUpToAndIncludingST { get; init; }

    /// <summary>
    /// Gets the employer rate applicable for earnings between the Secondary Threshold
    /// and the Freeport Upper Secondary Threshold.
    /// </summary>
    public decimal ForEarningsAboveSTUpToAndIncludingFUST { get; init; }

    /// <summary>
    /// Gets the employer rate applicable for earnings between the Freeport Upper
    /// Secondary Threshold and the Upper Earnings Limit or any applicable Upper
    /// Secondary Threshold.
    /// </summary>
    public decimal ForEarningsAboveFUSTUpToAndIncludingUELOrUST { get; init; }

    /// <summary>
    /// Gets the employer rate applicable for earnings above the Upper Earnings Limit.
    /// </summary>
    public decimal ForEarningsAboveUELOrUST { get; init; }
}
