// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using FluentAssertions;
using Payetools.Common.Model;

namespace Payetools.Common.Tests;

public class TaxYearTests
{
    [Fact]
    public void TestConstructFromDate()
    {
        var date = new DateOnly(2020, 5, 5);
        var taxYear = new TaxYear(date);
        taxYear.TaxYearEnding.Should().Be(TaxYearEnding.Apr5_2021);

        date = new DateOnly(2022, 4, 5);
        taxYear = new TaxYear(date);
        taxYear.TaxYearEnding.Should().Be(TaxYearEnding.Apr5_2022);

        date = new DateOnly(2022, 4, 6);
        taxYear = new TaxYear(date);
        taxYear.TaxYearEnding.Should().Be(TaxYearEnding.Apr5_2023);
    }

    [Fact]
    public void TestMonthlyTaxPeriod()
    {
        RunTaxPeriodTest(TaxYearEnding.Apr5_2022, new DateOnly(2021, 4, 6), PayFrequency.Monthly, 1, -1);
        RunTaxPeriodTest(TaxYearEnding.Apr5_2023, new DateOnly(2022, 5, 5), PayFrequency.Monthly, 1, -1);
        RunTaxPeriodTest(TaxYearEnding.Apr5_2019, new DateOnly(2018, 5, 6), PayFrequency.Monthly, 2, -1);
        RunTaxPeriodTest(TaxYearEnding.Apr5_2022, new DateOnly(2021, 12, 31), PayFrequency.Monthly, 9, -1);
        RunTaxPeriodTest(TaxYearEnding.Apr5_2020, new DateOnly(2020, 1, 5), PayFrequency.Monthly, 9, -1);
        RunTaxPeriodTest(TaxYearEnding.Apr5_2021, new DateOnly(2021, 1, 6), PayFrequency.Monthly, 10, -1);
        RunTaxPeriodTest(TaxYearEnding.Apr5_2021, new DateOnly(2021, 2, 15), PayFrequency.Monthly, 11, -1);
        RunTaxPeriodTest(TaxYearEnding.Apr5_2022, new DateOnly(2022, 3, 20), PayFrequency.Monthly, 12, -1);
        RunTaxPeriodTest(TaxYearEnding.Apr5_2022, new DateOnly(2022, 4, 5), PayFrequency.Monthly, 12, -1);
    }

    [Fact]
    public void TestWeeklyTaxPeriod()
    {
        RunTaxPeriodTest(TaxYearEnding.Apr5_2022, new DateOnly(2021, 4, 6), PayFrequency.Weekly, 1, 0, 1);
        RunTaxPeriodTest(TaxYearEnding.Apr5_2023, new DateOnly(2022, 5, 5), PayFrequency.Weekly, 5, 0, 1);
        RunTaxPeriodTest(TaxYearEnding.Apr5_2019, new DateOnly(2018, 5, 15), PayFrequency.Weekly, 6, 0, 2);
        RunTaxPeriodTest(TaxYearEnding.Apr5_2022, new DateOnly(2021, 12, 31), PayFrequency.Weekly, 39, 0, 9);
        RunTaxPeriodTest(TaxYearEnding.Apr5_2020, new DateOnly(2020, 1, 5), PayFrequency.Weekly, 40, 0, 9);
        RunTaxPeriodTest(TaxYearEnding.Apr5_2021, new DateOnly(2021, 1, 6), PayFrequency.Weekly, 40, 0, 10);
        RunTaxPeriodTest(TaxYearEnding.Apr5_2021, new DateOnly(2021, 2, 15), PayFrequency.Weekly, 46, 0, 11);
        RunTaxPeriodTest(TaxYearEnding.Apr5_2022, new DateOnly(2022, 3, 20), PayFrequency.Weekly, 50, 0, 12);
        RunTaxPeriodTest(TaxYearEnding.Apr5_2022, new DateOnly(2022, 4, 3), PayFrequency.Weekly, 52, 0, 12);
        RunTaxPeriodTest(TaxYearEnding.Apr5_2022, new DateOnly(2022, 4, 5), PayFrequency.Weekly, 53, 0, 12);
    }

    [Fact]
    public void TestTwoWeeklyTaxPeriod()
    {
        RunTaxPeriodTest(TaxYearEnding.Apr5_2022, new DateOnly(2021, 4, 6), PayFrequency.TwoWeekly, 1, 2, 1);
        RunTaxPeriodTest(TaxYearEnding.Apr5_2023, new DateOnly(2022, 5, 5), PayFrequency.TwoWeekly, 3, 6, 1);
        RunTaxPeriodTest(TaxYearEnding.Apr5_2019, new DateOnly(2018, 5, 15), PayFrequency.TwoWeekly, 3, 6, 2);
        RunTaxPeriodTest(TaxYearEnding.Apr5_2022, new DateOnly(2021, 12, 31), PayFrequency.TwoWeekly, 20, 40, 9);
        RunTaxPeriodTest(TaxYearEnding.Apr5_2020, new DateOnly(2020, 1, 5), PayFrequency.TwoWeekly, 20, 40, 9);
        RunTaxPeriodTest(TaxYearEnding.Apr5_2021, new DateOnly(2021, 1, 11), PayFrequency.TwoWeekly, 21, 42, 10);
        RunTaxPeriodTest(TaxYearEnding.Apr5_2020, new DateOnly(2020, 2, 29), PayFrequency.TwoWeekly, 24, 48, 11);
        RunTaxPeriodTest(TaxYearEnding.Apr5_2022, new DateOnly(2022, 3, 20), PayFrequency.TwoWeekly, 25, 50, 12);
        RunTaxPeriodTest(TaxYearEnding.Apr5_2022, new DateOnly(2022, 4, 3), PayFrequency.TwoWeekly, 26, 52, 12);
        RunTaxPeriodTest(TaxYearEnding.Apr5_2022, new DateOnly(2022, 4, 5), PayFrequency.TwoWeekly, 27, 54, 12);
    }

    [Fact]
    public void TestFourWeeklyTaxPeriod()
    {
        RunTaxPeriodTest(TaxYearEnding.Apr5_2022, new DateOnly(2021, 4, 6), PayFrequency.FourWeekly, 1, 4, 1);
        RunTaxPeriodTest(TaxYearEnding.Apr5_2023, new DateOnly(2022, 5, 5), PayFrequency.FourWeekly, 2, 8, 1);
        RunTaxPeriodTest(TaxYearEnding.Apr5_2019, new DateOnly(2018, 6, 1), PayFrequency.FourWeekly, 3, 12, 2);
        RunTaxPeriodTest(TaxYearEnding.Apr5_2022, new DateOnly(2021, 12, 31), PayFrequency.FourWeekly, 10, 40, 9);
        RunTaxPeriodTest(TaxYearEnding.Apr5_2020, new DateOnly(2020, 1, 5), PayFrequency.FourWeekly, 10, 40, 9);
        RunTaxPeriodTest(TaxYearEnding.Apr5_2021, new DateOnly(2021, 1, 11), PayFrequency.FourWeekly, 11, 44, 10);
        RunTaxPeriodTest(TaxYearEnding.Apr5_2020, new DateOnly(2020, 2, 29), PayFrequency.FourWeekly, 12, 48, 11);
        RunTaxPeriodTest(TaxYearEnding.Apr5_2022, new DateOnly(2022, 3, 20), PayFrequency.FourWeekly, 13, 52, 12);
        RunTaxPeriodTest(TaxYearEnding.Apr5_2022, new DateOnly(2022, 4, 3), PayFrequency.FourWeekly, 13, 52, 12);
        RunTaxPeriodTest(TaxYearEnding.Apr5_2022, new DateOnly(2022, 4, 5), PayFrequency.FourWeekly, 14, 56, 12);
    }

    [Fact]
    public void TestQuarterlyTaxPeriod()
    {
        RunTaxPeriodTest(TaxYearEnding.Apr5_2022, new DateOnly(2021, 4, 6), PayFrequency.Quarterly, 1, -1, 3);
        RunTaxPeriodTest(TaxYearEnding.Apr5_2023, new DateOnly(2022, 7, 5), PayFrequency.Quarterly, 1, -1, 3);
        RunTaxPeriodTest(TaxYearEnding.Apr5_2019, new DateOnly(2018, 7, 6), PayFrequency.Quarterly, 2, -1, 6);
        RunTaxPeriodTest(TaxYearEnding.Apr5_2022, new DateOnly(2021, 10, 5), PayFrequency.Quarterly, 2, -1, 6);
        RunTaxPeriodTest(TaxYearEnding.Apr5_2022, new DateOnly(2021, 10, 6), PayFrequency.Quarterly, 3, -1, 9);
        RunTaxPeriodTest(TaxYearEnding.Apr5_2020, new DateOnly(2020, 1, 5), PayFrequency.Quarterly, 3, -1, 9);
        RunTaxPeriodTest(TaxYearEnding.Apr5_2021, new DateOnly(2021, 1, 6), PayFrequency.Quarterly, 4, -1, 12);
        RunTaxPeriodTest(TaxYearEnding.Apr5_2022, new DateOnly(2022, 4, 5), PayFrequency.Quarterly, 4, -1, 12);
    }

    [Fact]
    public void TestBiAnnuallyTaxPeriod()
    {
        RunTaxPeriodTest(TaxYearEnding.Apr5_2022, new DateOnly(2021, 4, 6), PayFrequency.BiAnnually, 1, -1, 6);
        RunTaxPeriodTest(TaxYearEnding.Apr5_2022, new DateOnly(2021, 10, 5), PayFrequency.BiAnnually, 1, -1, 6);
        RunTaxPeriodTest(TaxYearEnding.Apr5_2022, new DateOnly(2021, 10, 6), PayFrequency.BiAnnually, 2, -1, 12);
        RunTaxPeriodTest(TaxYearEnding.Apr5_2020, new DateOnly(2020, 4, 5), PayFrequency.BiAnnually, 2, -1, 12);
    }

    [Fact]
    public void TestInvalidTaxPeriod()
    {
        Action action = () =>
        {
            var taxYear = new TaxYear(TaxYearEnding.Apr5_2022);
            var periodNumber = taxYear.GetTaxPeriod(new DateOnly(2022, 4, 6), PayFrequency.Monthly);
        };

        action.Should().Throw<ArgumentException>()
            .WithMessage("Pay date of 06/04/2022 is outside this tax year 06/04/2021 - 05/04/2022 (Parameter 'payDate')");
    }

    [Fact]
    public void TestLastDateOfTaxPeriod()
    {
        var taxYear = new TaxYear(TaxYearEnding.Apr5_2022);

        taxYear.GetLastDayOfTaxPeriod(PayFrequency.Weekly, 1).Should().Be(new DateOnly(2021, 4, 12));
        taxYear.GetLastDayOfTaxPeriod(PayFrequency.Weekly, 2).Should().Be(new DateOnly(2021, 4, 19));
        taxYear.GetLastDayOfTaxPeriod(PayFrequency.TwoWeekly, 1).Should().Be(new DateOnly(2021, 4, 19));
        taxYear.GetLastDayOfTaxPeriod(PayFrequency.TwoWeekly, 2).Should().Be(new DateOnly(2021, 5, 3));
        taxYear.GetLastDayOfTaxPeriod(PayFrequency.FourWeekly, 1).Should().Be(new DateOnly(2021, 5, 3));
        taxYear.GetLastDayOfTaxPeriod(PayFrequency.Monthly, 1).Should().Be(new DateOnly(2021, 5, 5));
        taxYear.GetLastDayOfTaxPeriod(PayFrequency.Monthly, 2).Should().Be(new DateOnly(2021, 6, 5));
        taxYear.GetLastDayOfTaxPeriod(PayFrequency.Monthly, 3).Should().Be(new DateOnly(2021, 7, 5));
        taxYear.GetLastDayOfTaxPeriod(PayFrequency.Monthly, 12).Should().Be(new DateOnly(2022, 4, 5));
        taxYear.GetLastDayOfTaxPeriod(PayFrequency.Quarterly, 1).Should().Be(new DateOnly(2021, 7, 5));
        taxYear.GetLastDayOfTaxPeriod(PayFrequency.BiAnnually, 1).Should().Be(new DateOnly(2021, 10, 5));
        taxYear.GetLastDayOfTaxPeriod(PayFrequency.Annually, 1).Should().Be(new DateOnly(2022, 4, 5));
    }

    private static void RunTaxPeriodTest(
        TaxYearEnding taxYearEnding,
        DateOnly payDate,
        PayFrequency payFrequency,
        int expectedPeriodNumber,
        int expectedWeekNumber = 0,
        int expectedMonthNumber = 0)
    {
        var taxYear = new TaxYear(taxYearEnding);
        var periodNumber = taxYear.GetTaxPeriod(payDate, payFrequency);

        periodNumber.Should().Be(expectedPeriodNumber);

        if (expectedWeekNumber != -1)
        {
            var weekNumber = taxYear.GetWeekNumber(payDate, payFrequency);

            var expWkNo = expectedWeekNumber != 0 ? expectedWeekNumber : expectedPeriodNumber;
            weekNumber.Should().Be(expWkNo);
        }

        var monthNumber = taxYear.GetMonthNumber(payDate, payFrequency);

        var expMnthNo = expectedMonthNumber != 0 ? expectedMonthNumber : expectedPeriodNumber;
        monthNumber.Should().Be(expMnthNo);
    }
}