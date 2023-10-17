// Copyright (c) 2023 Payetools Foundation.
//
// Payetools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Payetools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

using Payetools.ReferenceData;
using Payetools.Testing.Utils;

namespace Payetools.IncomeTax.Tests;

public class TaxCalculatorFactoryDataFixture : CalculatorFactoryDataFixture<ITaxCalculatorFactory>
{
    protected override ITaxCalculatorFactory MakeFactory(IHmrcReferenceDataProvider provider) =>
        new TaxCalculatorFactory(provider);
}