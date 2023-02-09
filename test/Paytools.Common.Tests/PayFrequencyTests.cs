// Copyright (c) 2023 Paytools Foundation
//
// Licensed under the Apache License, Version 2.0 (the "License")~
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using FluentAssertions;
using Paytools.Common.Model;

namespace Paytools.Common.Tests;

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