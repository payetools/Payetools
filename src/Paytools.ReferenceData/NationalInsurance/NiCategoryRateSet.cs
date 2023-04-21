// Copyright (c) 2023 Paytools Foundation.
//
// Paytools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Paytools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

using Paytools.Common.Model;
using Paytools.NationalInsurance.ReferenceData;

namespace Paytools.ReferenceData.NationalInsurance;

/// <summary>
/// Represents a set of National Insurance rates across all NI categories.  Each NI
/// category specifies its own set rate of rates across the various NI thresholds.
/// </summary>
public record NiCategoryRateSet
{
    private readonly Dictionary<NiCategory, INiCategoryRatesEntry> _categories;

    /// <summary>
    /// Initialises a new instance of <see cref="NiCategoryRateSet"/>.
    /// </summary>
    public NiCategoryRateSet()
    {
        _categories = new Dictionary<NiCategory, INiCategoryRatesEntry>();
    }

    /// <summary>
    /// Gets the set of NI rates applicable to the specified <see cref="NiCategory"/>.
    /// </summary>
    /// <param name="category">NI category to retrieve the applicable rates for.</param>
    /// <returns>Set of rates applicable to the specified NI category.</returns>
    public INiCategoryRatesEntry GetRatesForCategory(NiCategory category)
    {
        return _categories[category];
    }

    internal void Add(NiCategory category, NiCategoryRatesEntry rates)
    {
        _categories.TryAdd(category, rates);
    }
}
