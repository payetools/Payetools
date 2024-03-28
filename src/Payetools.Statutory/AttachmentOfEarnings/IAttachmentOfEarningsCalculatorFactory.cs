// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    /// <returns>A new calculator instance.</returns>
    public IAttachmentOfEarningsCalculator GetCalculator(
        PayDate payDate);
}
