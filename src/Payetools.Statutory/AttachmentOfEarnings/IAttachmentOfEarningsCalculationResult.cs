// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

namespace Payetools.Statutory.AttachmentOfEarnings;

/// <summary>
/// Interface that represents the results of an attachment of earnings calculation.
/// </summary>
public interface IAttachmentOfEarningsCalculationResult
{
    /// <summary>
    /// Gets the total deduction applicable as a result of an attachment of earnings order.
    /// </summary>
    public decimal TotalDeduction { get; }
}
