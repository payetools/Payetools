// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.AttachmentOrders.Factories;
using Payetools.ReferenceData;
using Payetools.Testing.Utils;

namespace Payetools.AttachmentOrders.Tests;

public class AttachmentOrdersCalculatorFactoryDataFixture : CalculatorFactoryDataFixture<IAttachmentOrdersCalculatorFactory>
{
    protected override IAttachmentOrdersCalculatorFactory MakeFactory(IHmrcReferenceDataProvider provider) =>
        new AttachmentOrdersCalculatorFactory(provider);
}
