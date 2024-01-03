// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.ReferenceData;
using Payetools.Testing.Utils;

namespace Payetools.NationalMinimumWage.Tests;

public class NmwEvaluatorFactoryDataFixture : CalculatorFactoryDataFixture<INmwEvaluatorFactory>
{
    protected override INmwEvaluatorFactory MakeFactory(IHmrcReferenceDataProvider provider) =>
        new NmwEvaluatorFactory(provider);
}