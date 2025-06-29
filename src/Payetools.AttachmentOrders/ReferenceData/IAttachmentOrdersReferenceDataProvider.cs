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
/// Interface that represents a provider of attachment order information.
/// </summary>
public interface IAttachmentOrdersReferenceDataProvider
{
    /// <summary>
    /// Gets the rate table for the specified countries, calculation type, and applicable date range.
    /// </summary>
    /// <param name="taxYear">Applicable tax year.</param>
    /// <param name="jurisdiction">Jurisdiction that this set of rates applies to.</param>
    /// <param name="calculationType">Attachment order calculation type to match.</param>
    /// <param name="applicabilityDate">Date that the attachment order applies, typically the issue
    /// date.</param>
    /// <returns>The rate table for the specified jurisdiction, calculation type and attachment
    /// order issue date.</returns>
    ImmutableArray<AttachmentOrderRateTableEntry>? GetAttachmentOrderRateTable(
        TaxYear taxYear,
        CountriesForTaxPurposes jurisdiction,
        AttachmentOrderCalculationType calculationType,
        DateOnly applicabilityDate);
}