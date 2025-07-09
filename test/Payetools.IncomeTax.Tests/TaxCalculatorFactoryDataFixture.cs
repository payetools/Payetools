// Copyright (c) 2023-2025, Payetools Ltd.
//
// Payetools Ltd licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.ReferenceData;
using Payetools.Testing.Utils;

namespace Payetools.IncomeTax.Tests;

public class TaxCalculatorFactoryDataFixture : CalculatorFactoryDataFixture<ITaxCalculatorFactory>
{
    protected override ITaxCalculatorFactory MakeFactory(IHmrcReferenceDataProvider provider) =>
        new TaxCalculatorFactory(provider);
}