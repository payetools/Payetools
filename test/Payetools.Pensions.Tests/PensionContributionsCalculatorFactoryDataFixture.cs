// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Payetools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

using Payetools.ReferenceData;
using Payetools.Testing.Utils;

namespace Payetools.Pensions.Tests;

public class PensionContributionsCalculatorFactoryDataFixture : CalculatorFactoryDataFixture<IPensionContributionCalculatorFactory>
{

    protected override IPensionContributionCalculatorFactory MakeFactory(IHmrcReferenceDataProvider provider) =>
        new PensionContributionCalculatorFactory(provider);
}