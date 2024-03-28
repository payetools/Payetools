// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

namespace Payetools.Statutory.AttachmentOfEarnings;

/// <summary>
/// Calculator that provide calculation of attachment of earnings orders.
/// </summary>
public class AttachmentOfEarningsCalculator : IAttachmentOfEarningsCalculator
{
    /// <summary>
    /// Calculates the appropriate employee deduction for the attachment of earnings that this calculator
    /// pertains to.
    /// </summary>
    /// <param name="earnings">Earnings.</param>
    /// <param name="result">Result.</param>
    public void Calculate(in decimal earnings, out IAttachmentOfEarningsCalculationResult result)
    {
        throw new NotImplementedException();
    }
}
