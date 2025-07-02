// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.AttachmentOrders.Model;
using Payetools.Common.Model;

namespace Payetools.Testing.Data.AttachmentOrders;

public interface IAttachmentOrderTestDataEntry
{
    AttachmentOrderCalculationBehaviours CalculationType { get; init; }

    string Jurisdiction { get; init; }

    AttachmentOrderRateType? Rate { get; init; }

    PayFrequency PayFrequency { get; init; }

    DateOnly? IssueDate { get; init; }

    DateOnly? ReceivedDate { get; init; }

    decimal AttachableEarnings { get; init; }

    decimal ExpectedDeduction { get; init; }
}