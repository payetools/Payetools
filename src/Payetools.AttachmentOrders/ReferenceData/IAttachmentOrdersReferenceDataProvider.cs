// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;

namespace Payetools.AttachmentOrders.ReferenceData;

/// <summary>
/// Interface that represents a provider of attachment order reference data information.
/// </summary>
public interface IAttachmentOrdersReferenceDataProvider
{
    /// <summary>
    /// Gets a dictionary that keyed on lookup data (applicability date, jurisdiction, etc.)
    /// to attachment order reference data entries for a given tax year.
    /// </summary>
    /// <param name="taxYear">Applicable tax year.</param>
    /// <returns>A dictionary that maps from key lookup data to a given attachment order
    /// reference data entry.</returns>
    Dictionary<AttachmentOrderReferenceDataEntry.LookupKey, AttachmentOrderReferenceDataEntry> GetAllAttachmentOrderEntries(
        TaxYear taxYear);
}