// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.AttachmentOrders.Model;

namespace Payetools.AttachmentOrders.Tests;

internal class AttachmentOrder : IAttachmentOrder
{
    public AttachmentOrderCalculationType CalculationType { get; init; }

    public DateOnly IssueDate { get; init; }

    public DateOnly EffectiveDate { get; init; }

    public AttachmentOrderRateType? RateType { get; init;  }

    public AttachmentOrderPayFrequency EmployeePayFrequency { get; init; }
}
