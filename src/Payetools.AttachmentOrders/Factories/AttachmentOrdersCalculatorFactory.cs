// Copyright (c) 2023-2025, Payetools Ltd.
//
// Payetools Ltd licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.AttachmentOrders.ReferenceData;
using Payetools.Common.Model;

namespace Payetools.AttachmentOrders.Factories;

/// <summary>
/// Factory that can generate <see cref="IAttachmentOrdersCalculator"/> implementations.
/// </summary>
public class AttachmentOrdersCalculatorFactory : IAttachmentOrdersCalculatorFactory
{
    private readonly IAttachmentOrdersReferenceDataProvider _referenceDataProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="AttachmentOrdersCalculatorFactory"/> class.
    /// </summary>
    /// <param name="referenceDataProvider">Reference data provider for attachment order calculations.</param>
    public AttachmentOrdersCalculatorFactory(IAttachmentOrdersReferenceDataProvider referenceDataProvider)
    {
        _referenceDataProvider = referenceDataProvider;
    }

    /// <summary>
    /// Gets a new <see cref="IAttachmentOrdersCalculator"/> based on the specified pay date and ...
    /// The pay date is provided in order to determine which ... to use, noting that these may change
    /// in-year.
    /// </summary>
    /// <param name="payDate">Applicable pay date.</param>
    /// <returns>A new calculator instance.</returns>
    public IAttachmentOrdersCalculator GetCalculator(PayDate payDate)
    {
        var referenceDataEntries = _referenceDataProvider.GetAllAttachmentOrderEntries(payDate.TaxYear);

        return new AttachmentOrdersCalculator(referenceDataEntries);
    }
}