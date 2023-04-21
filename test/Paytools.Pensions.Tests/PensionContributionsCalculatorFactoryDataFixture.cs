// Copyright (c) 2023 Paytools Foundation.
//
// Paytools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Paytools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

using Paytools.ReferenceData;
using Paytools.Testing.Utils;

namespace Paytools.Pensions.Tests;

public class PensionContributionsCalculatorFactoryDataFixture : CalculatorFactoryDataFixture<IPensionContributionCalculatorFactory>
{

    protected override IPensionContributionCalculatorFactory MakeFactory(IHmrcReferenceDataProvider provider) =>
        new PensionContributionCalculatorFactory(provider);
}