﻿// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;

namespace Payetools.Statutory.AttachmentOfEarnings;

/// <summary>
/// Interface that represents factories that can generate <see cref="IAttachmentOfEarningsCalculator"/> implementations.
/// </summary>
public interface IAttachmentOfEarningsCalculatorFactory
{
    /// <summary>
    /// Gets a new <see cref="IAttachmentOfEarningsCalculator"/> based on the specified pay date and ...
    /// The pay date is provided in order to determine which ... to use, noting that these may change
    /// in-year.
    /// </summary>
    /// <param name="payDate">Applicable pay date.</param>
    /// <param name="attachmentOfEarningsType">Attachment of earnings order type.</param>
    /// <returns>A new calculator instance.</returns>
    IAttachmentOfEarningsCalculator GetCalculator(PayDate payDate, AttachmentOfEarningsType attachmentOfEarningsType);
}
