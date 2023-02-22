// Copyright (c) 2023 Paytools Foundation.  All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License") ~
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paytools.Common.Tests;

public class PayDateTests
{
    [Fact]
    public void TestConstructors()
    {
        var date = new DateOnly(2022, 5, 5);
        var payDate = new PayDate(date, PayFrequency.Monthly);
        payDate.TaxPeriod.Should().Be(1);
        payDate.TaxYear.TaxYearEnding.Should().Be(TaxYearEnding.Apr5_2023);

        date = new DateOnly(2022, 4, 5);
        payDate = new PayDate(date, PayFrequency.Monthly);
        payDate.TaxPeriod.Should().Be(12);
        payDate.TaxYear.TaxYearEnding.Should().Be(TaxYearEnding.Apr5_2022);

        date = new DateOnly(2023, 4, 4);
        payDate = new PayDate(date, PayFrequency.Weekly);
        payDate.TaxPeriod.Should().Be(52);
        payDate.TaxYear.TaxYearEnding.Should().Be(TaxYearEnding.Apr5_2023);

        date = new DateOnly(2023, 4, 5);
        payDate = new PayDate(date, PayFrequency.Weekly);
        payDate.TaxPeriod.Should().Be(53);
        payDate.TaxYear.TaxYearEnding.Should().Be(TaxYearEnding.Apr5_2023);

        date = new DateOnly(2021, 12, 31);
        payDate = new PayDate(date, PayFrequency.Monthly);
        payDate.TaxPeriod.Should().Be(9);
        payDate.TaxYear.TaxYearEnding.Should().Be(TaxYearEnding.Apr5_2022);
    }

    [Fact]
    public void TestInvalidPayDates() 
    {
        var date = new DateOnly(2018, 4, 5);
        var action = () => new PayDate(date, PayFrequency.Monthly);

        action.Should().Throw<ArgumentOutOfRangeException>()
            .WithMessage("Unsupported tax year; date must fall within range tax year ending 6 April 2019 to 6 April 2024 (Parameter 'date')");

        date = new DateOnly(2024, 4, 6);
        action = () => new PayDate(date, PayFrequency.Monthly);

        action.Should().Throw<ArgumentOutOfRangeException>()
            .WithMessage("Unsupported tax year; date must fall within range tax year ending 6 April 2019 to 6 April 2024 (Parameter 'date')");

    }
}
