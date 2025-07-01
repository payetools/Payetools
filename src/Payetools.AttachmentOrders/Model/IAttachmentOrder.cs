// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

namespace Payetools.AttachmentOrders.Model;

/// <summary>
/// Interface that represents an attachment order.
/// </summary>
public interface IAttachmentOrder
{
    /// <summary>
    /// Gets the type of attachment of earnings order.
    /// </summary>
    AttachmentOrderCalculationType CalculationType { get; }

    /// <summary>
    /// Gets the date on which the attachment order was issued; this may be used to determine the rates and
    /// thresholds that apply to the order.
    /// </summary>
    DateOnly IssueDate { get; }

    /// <summary>
    /// Gets the date from which this attachment of earnings order is effective.
    /// </summary>
    DateOnly EffectiveDate { get; }

    /// <summary>
    /// Gets the optional rate type that applies to this attachment order, if applicable.
    /// </summary>
    AttachmentOrderRateType? RateType { get; }

    /// <summary>
    /// Gets the employee's pay frequency as it applies to this order.
    /// </summary>
    AttachmentOrderPayFrequency EmployeePayFrequency { get; }
}