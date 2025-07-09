// Copyright (c) 2023-2025, Payetools Ltd.
//
// Payetools Ltd licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;
using System.Collections.Immutable;

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
    /// <returns>An immutable array of reference data entries.</returns>
    ImmutableArray<AttachmentOrderReferenceDataEntry> GetAllAttachmentOrderEntries(TaxYear taxYear);
}