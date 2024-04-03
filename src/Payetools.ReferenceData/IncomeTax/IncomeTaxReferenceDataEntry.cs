// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;
using System.Collections.Immutable;

namespace Payetools.ReferenceData.IncomeTax;

/// <summary>
/// Represents a set of tax bands for a given tax regime for a period, typically a full tax year.
/// </summary>
public class IncomeTaxReferenceDataEntry : IApplicableFromTill
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
    public ImmutableArray<IncomeTaxBandEntry> TaxEntries { get; init; } = default!;
}