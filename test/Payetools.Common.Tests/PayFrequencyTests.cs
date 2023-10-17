// Copyright (c) 2023 Payetools Foundation.
//
// Payetools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Payetools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

using FluentAssertions;
using Payetools.Common.Model;

namespace Payetools.Common.Tests;

public class PayFrequencyTests
{
    [Fact]
    public void TestTaxYearPeriodCounts()
    {
        PayFrequency.Weekly.GetStandardTaxPeriodCount().Should().Be(52);
        PayFrequency.TwoWeekly.GetStandardTaxPeriodCount().Should().Be(26);
        PayFrequency.FourWeekly.GetStandardTaxPeriodCount().Should().Be(13);
        PayFrequency.Monthly.GetStandardTaxPeriodCount().Should().Be(12);
        PayFrequency.Quarterly.GetStandardTaxPeriodCount().Should().Be(4);
        PayFrequency.BiAnnually.GetStandardTaxPeriodCount().Should().Be(2);
        PayFrequency.Annually.GetStandardTaxPeriodCount().Should().Be(1);

        Action action = () => { PayFrequency.Unspecified.GetStandardTaxPeriodCount(); };

        action.Should().Throw<ArgumentException>()
            .WithMessage($"Invalid pay frequency value Unspecified (Parameter 'payFrequency')");
    }
}