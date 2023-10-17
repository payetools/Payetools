// Copyright (c) 2023 Payetools Foundation.
//
// Payetools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Payetools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

using FluentAssertions;
using Payetools.Common.Extensions;
using Payetools.Common.Model;

namespace Payetools.Common.Tests;

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

        date = new DateOnly(2022, 5, 5);
        ending = date.ToTaxYearEnding();
        ending.Should().Be(TaxYearEnding.Apr5_2023);
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
