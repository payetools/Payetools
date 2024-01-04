// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Payroll.Payruns;
using Payetools.ReferenceData;
using Payetools.Testing.Utils;

namespace Payetools.Payroll.Tests;

public class PayrollProcessorFactoryFixture : CalculatorFactoryDataFixture<IPayrunProcessorFactory>
{
    protected override IPayrunProcessorFactory MakeFactory(IHmrcReferenceDataProvider provider) =>
        new PayrunProcessorFactory(provider);
}