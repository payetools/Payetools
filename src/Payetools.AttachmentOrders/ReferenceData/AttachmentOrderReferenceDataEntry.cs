// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.AttachmentOrders.Model;
using Payetools.Common.Model;
using System.Collections.Immutable;

namespace Payetools.AttachmentOrders.ReferenceData;

/// <summary>
/// Represents a set of reference data for attachment orders, including the applicable dates.
/// </summary>
public class AttachmentOrderReferenceDataEntry : IApplicableFromTill
{
    /// <summary>
    /// Gets the start date (i.e., the first full day) for applicability of this set of reference data.
    /// </summary>
    /// <remarks>Unlike income tax, National Insurance, etc., this date relates to the first day the
    /// relevant legilsation came into force, for this data to be relevant.</remarks>
    public DateOnly ApplicableFrom { get; init; }

    /// <summary>
    /// Gets the end date (i.e., the last full day) for applicability of this set of reference data.
    /// </summary>
    /// <remarks>Unlike income tax, National Insurance, etc., this date relates to the last day the
    /// relevant legislation was in force, for this data to be relevant. If this legislation is the
    /// current (i.e., remains in force until any new legislation is introduced, this value is set
    /// to <see cref="DateOnly.MaxValue"/>.</remarks>
    public DateOnly ApplicableTill { get; init; }

    /// <summary>
    /// Gets the set of countries within the UK that this set of tax bands refer to.
    /// </summary>
    public CountriesForTaxPurposes ApplicableCountries { get; init; }

    /// <summary>
    /// Gets the calculation type for this set of reference data.
    /// </summary>
    public AttachmentOrderCalculationType CalculationType { get; init; }

    /// <summary>
    /// Gets the rate table for this set of reference data.
    /// </summary>
    public required ImmutableArray<AttachmentOrderRateTableEntry> RateTable { get; init; }
}