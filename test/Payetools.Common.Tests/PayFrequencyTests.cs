// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Shouldly;
using Payetools.Common.Model;

namespace Payetools.Common.Tests;

public class PayFrequencyTests
{
    [Fact]
    public void TestTaxYearPeriodCounts()
    {
        PayFrequency.Weekly.GetStandardTaxPeriodCount().ShouldBe(52);
        PayFrequency.Fortnightly.GetStandardTaxPeriodCount().ShouldBe(26);
        PayFrequency.FourWeekly.GetStandardTaxPeriodCount().ShouldBe(13);
        PayFrequency.Monthly.GetStandardTaxPeriodCount().ShouldBe(12);
        PayFrequency.Quarterly.GetStandardTaxPeriodCount().ShouldBe(4);
        PayFrequency.BiAnnually.GetStandardTaxPeriodCount().ShouldBe(2);
        PayFrequency.Annually.GetStandardTaxPeriodCount().ShouldBe(1);

        Action action = () => { PayFrequency.Unspecified.GetStandardTaxPeriodCount(); };

        action.ShouldThrow<ArgumentException>()
            .Message.ShouldBe($"Invalid pay frequency value Unspecified (Parameter 'payFrequency')");
    }
}