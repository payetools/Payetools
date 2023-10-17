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
