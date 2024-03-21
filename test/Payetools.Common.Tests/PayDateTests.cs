// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

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

    [Fact]
    public void TestWeekOrMonthNumber()
    {
        var payDate = new PayDate(new DateOnly(2022, 5, 5), PayFrequency.Monthly);
        payDate.GetWeekOrMonthNumber(out var periodNumber, out var isWeekly);
        isWeekly.Should().BeFalse();
        periodNumber.Should().Be(1);

        payDate = new PayDate(new DateOnly(2022, 5, 6), PayFrequency.Monthly);
        payDate.GetWeekOrMonthNumber(out periodNumber, out isWeekly);
        isWeekly.Should().BeFalse();
        periodNumber.Should().Be(2);

        payDate = new PayDate(new DateOnly(2022, 5, 6), PayFrequency.Quarterly);
        payDate.GetWeekOrMonthNumber(out periodNumber, out isWeekly);
        isWeekly.Should().BeFalse();
        periodNumber.Should().Be(3);

        payDate = new PayDate(new DateOnly(2023, 4, 13), PayFrequency.Weekly);
        payDate.GetWeekOrMonthNumber(out periodNumber, out isWeekly);
        isWeekly.Should().BeTrue();
        periodNumber.Should().Be(2);

        payDate = new PayDate(new DateOnly(2024, 4, 5), PayFrequency.Weekly);
        payDate.GetWeekOrMonthNumber(out periodNumber, out isWeekly);
        isWeekly.Should().BeTrue();
        periodNumber.Should().Be(53);

        payDate = new PayDate(new DateOnly(2023, 5, 18), PayFrequency.TwoWeekly);
        payDate.GetWeekOrMonthNumber(out periodNumber, out isWeekly);
        isWeekly.Should().BeTrue();
        periodNumber.Should().Be(8);

        payDate = new PayDate(new DateOnly(2023, 11, 16), PayFrequency.FourWeekly);
        payDate.GetWeekOrMonthNumber(out periodNumber, out isWeekly);
        isWeekly.Should().BeTrue();
        periodNumber.Should().Be(36);
    }
}
