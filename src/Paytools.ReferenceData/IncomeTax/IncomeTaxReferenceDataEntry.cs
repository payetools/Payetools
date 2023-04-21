// Copyright (c) 2023 Paytools Foundation.
//
// Paytools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Paytools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

using Paytools.Common.Model;
using System.Collections.Immutable;

namespace Paytools.ReferenceData.IncomeTax;

/// <summary>
/// Represents a set of tax bands for a given tax regime for a period, typically a full tax year.
/// </summary>
public record IncomeTaxReferenceDataEntry : IApplicableFromTill
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
    /// Gets a read-only list of applicable tax bands.
    /// </summary>
    public ImmutableList<IncomeTaxBandEntry> TaxEntries { get; init; } = default!;
}