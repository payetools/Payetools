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
using Paytools.Common.Extensions;
using Paytools.Common.Model;

namespace Paytools.Common.Tests;

public class TaxYearEndingTests
{
    [Fact]
    public void TestSpecificTaxYear()
    {
        var date = new DateOnly(2020, 4, 5);
        TaxYearEnding ending = date.ToTaxYearEnding();
        ending.Should().Be(TaxYearEnding.Apr5_2020);

        date = new DateOnly(2020, 4, 6);
        ending = date.ToTaxYearEnding();
        ending.Should().Be(TaxYearEnding.Apr5_2021);
    }

    [Fact]
    public void TestUnsupportedTaxYears()
    {
        Action action = () => (new DateOnly((int)TaxYearEnding.MinValue - 1, 1, 1)).ToTaxYearEnding();
        action.Should().Throw<ArgumentOutOfRangeException>()
            .WithMessage($"Unsupported tax year; date must fall within range tax year ending 6 April {(int)TaxYearEnding.MinValue} to 6 April {(int)TaxYearEnding.MaxValue} (Parameter 'date')");

        action = () => (new DateOnly((int)TaxYearEnding.MaxValue + 1, 1, 1)).ToTaxYearEnding();
        action.Should().Throw<ArgumentOutOfRangeException>()
            .WithMessage($"Unsupported tax year; date must fall within range tax year ending 6 April {(int)TaxYearEnding.MinValue} to 6 April {(int)TaxYearEnding.MaxValue} (Parameter 'date')");
    }

    [Fact]
    public void TestTaxYearEndingExtensions()
    {
        var taxYearEnding = TaxYearEnding.Apr5_2019;
        taxYearEnding.YearAsString().Should().Be("2019");
        
        taxYearEnding = TaxYearEnding.Apr5_2022;
        taxYearEnding.YearAsString().Should().Be("2022");
    }
}
