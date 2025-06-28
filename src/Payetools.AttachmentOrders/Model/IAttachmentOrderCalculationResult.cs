// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

namespace Payetools.AttachmentOrders.Model;

/// <summary>
/// Interface that represents the results of an attachment of earnings calculation.
/// </summary>
public interface IAttachmentOrderCalculationResult
{
    /// <summary>
    /// Gets the total deduction applicable as a result of any attachment of earnings orders.
    /// </summary>
    public decimal TotalDeduction { get; }
}