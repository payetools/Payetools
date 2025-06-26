// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.AttachmentOrders.Model;

namespace Payetools.AttachmentOrders;

/// <summary>
/// Calculator that provide calculation of attachment of earnings orders.
/// </summary>
public class AttachmentOrderCalculator : IAttachmentOrderCalculator
{
    /// <summary>
    /// Calculates the appropriate employee deduction for the attachment of earnings that this calculator
    /// pertains to.
    /// </summary>
    /// <param name="earnings">Earnings.</param>
    /// <param name="result">Result.</param>
    public void Calculate(in decimal earnings, out IAttachmentOrderCalculationResult result)
    {
        throw new NotImplementedException();
    }
}
