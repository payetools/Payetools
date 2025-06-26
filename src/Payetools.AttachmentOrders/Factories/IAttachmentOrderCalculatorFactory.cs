// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.AttachmentOrders.Model;
using Payetools.Common.Model;

namespace Payetools.AttachmentOrders.Factories;

/// <summary>
/// Interface that represents factories that can generate <see cref="IAttachmentOrderCalculator"/> implementations.
/// </summary>
public interface IAttachmentOrderCalculatorFactory
{
    /// <summary>
    /// Gets a new <see cref="IAttachmentOrderCalculator"/> based on the specified pay date and ...
    /// The pay date is provided in order to determine which ... to use, noting that these may change
    /// in-year.
    /// </summary>
    /// <param name="payDate">Applicable pay date.</param>
    /// <param name="attachmentOfEarningsType">Attachment of earnings order type.</param>
    /// <returns>A new calculator instance.</returns>
    IAttachmentOrderCalculator GetCalculator(PayDate payDate, AttachmentOrderType attachmentOfEarningsType);
}
