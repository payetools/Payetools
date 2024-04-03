// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;
using System.Collections.Immutable;

namespace Payetools.ReferenceData.NationalInsurance;

/// <summary>
/// Record that represents the employee NI rates to be applied at various earnings thresholds.
/// </summary>
public class NiEmployeeRatesEntry
{
    /// <summary>
    /// Gets the list of applicable NI categories for this rates entry.
    /// </summary>
    public ImmutableArray<NiCategory> NiCategories { get; init; } = default!;

    /// <summary>
    /// Gets the employee rate applicable for earnings between the Lower Earnings
    /// Limit and the Primary Threshold.
    /// </summary>
    public decimal ForEarningsAtOrAboveLELUpTAndIncludingPT { get; init; }

    /// <summary>
    /// Gets the employee rate applicable for earnings between the Primary Threshold
    /// and the Upper Earnings Limit.
    /// </summary>
    public decimal ForEarningsAbovePTUpToAndIncludingUEL { get; init; }

    /// <summary>
    /// Gets the employee rate applicable for earnings above the Upper Earnings
    /// Limit.
    /// </summary>
    public decimal ForEarningsAboveUEL { get; init; }
}