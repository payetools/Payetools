// Copyright (c) 2023-2025, Payetools Ltd.
//
// Payetools Ltd licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.AttachmentOrders.Model;
using Payetools.Common.Model;

namespace Payetools.AttachmentOrders.Factories;

/// <summary>
/// Interface that represents factories that can generate <see cref="IAttachmentOrdersCalculator"/> implementations.
/// </summary>
public interface IAttachmentOrdersCalculatorFactory
{
    /// <summary>
    /// Gets a new <see cref="IAttachmentOrdersCalculator"/> based on the specified pay date and ...
    /// The pay date is provided in order to determine which ... to use, noting that these may change
    /// in-year.
    /// </summary>
    /// <param name="payDate">Applicable pay date.</param>
    /// <returns>A new calculator instance.</returns>
    IAttachmentOrdersCalculator GetCalculator(PayDate payDate);
}