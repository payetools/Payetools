// Copyright (c) 2023-2025, Payetools Ltd.
//
// Payetools Ltd licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.AttachmentOrders.Model;
using Payetools.Common.Model;

namespace Payetools.Testing.Data.AttachmentOrders;

public class AttachmentOrderTestDataEntry : IAttachmentOrderTestDataEntry
{
    public AttachmentOrderCalculationTraits CalculationType { get; init; }

    public required string Jurisdiction { get; init; }

    public AttachmentOrderRateType? Rate { get; init; }

    public PayFrequency PayFrequency { get; init; }

    public DateOnly? IssueDate { get; init; }

    public DateOnly? ReceivedDate { get; init; }

    public decimal AttachableEarnings { get; init; }

    public decimal ExpectedDeduction { get; init; }
}