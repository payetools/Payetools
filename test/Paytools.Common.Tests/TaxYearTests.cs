// Copyright (c) 2023 Paytools Foundation
//
// Licensed under the Apache License, Version 2.0 (the "License");
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
        RunTaxPeriodTest(TaxYearEnding.Apr5_2022, new DateOnly(2021, 4, 6), PayFrequency.Monthly, 1);
        RunTaxPeriodTest(TaxYearEnding.Apr5_2023, new DateOnly(2022, 5, 5), PayFrequency.Monthly, 1);
        RunTaxPeriodTest(TaxYearEnding.Apr5_2019, new DateOnly(2018, 5, 6), PayFrequency.Monthly, 2);
        RunTaxPeriodTest(TaxYearEnding.Apr5_2022, new DateOnly(2021, 12, 31), PayFrequency.Monthly, 9);
        RunTaxPeriodTest(TaxYearEnding.Apr5_2020, new DateOnly(2020, 1, 5), PayFrequency.Monthly, 9);
        RunTaxPeriodTest(TaxYearEnding.Apr5_2021, new DateOnly(2021, 1, 6), PayFrequency.Monthly, 10);
        RunTaxPeriodTest(TaxYearEnding.Apr5_2021, new DateOnly(2021, 2, 15), PayFrequency.Monthly, 11);
        RunTaxPeriodTest(TaxYearEnding.Apr5_2022, new DateOnly(2022, 3, 20), PayFrequency.Monthly, 12);
        RunTaxPeriodTest(TaxYearEnding.Apr5_2022, new DateOnly(2022, 4, 5), PayFrequency.Monthly, 12);
    }

    [Fact]
    public void TestWeeklyTaxPeriod()
    {
        RunTaxPeriodTest(TaxYearEnding.Apr5_2022, new DateOnly(2021, 4, 6), PayFrequency.Weekly, 1);
        RunTaxPeriodTest(TaxYearEnding.Apr5_2023, new DateOnly(2022, 5, 5), PayFrequency.Weekly, 5);
        RunTaxPeriodTest(TaxYearEnding.Apr5_2019, new DateOnly(2018, 5, 15), PayFrequency.Weekly, 6);
        RunTaxPeriodTest(TaxYearEnding.Apr5_2022, new DateOnly(2021, 12, 31), PayFrequency.Weekly, 39);
        RunTaxPeriodTest(TaxYearEnding.Apr5_2020, new DateOnly(2020, 1, 5), PayFrequency.Weekly, 40);
        RunTaxPeriodTest(TaxYearEnding.Apr5_2021, new DateOnly(2021, 1, 6), PayFrequency.Weekly, 40);
        RunTaxPeriodTest(TaxYearEnding.Apr5_2021, new DateOnly(2021, 2, 15), PayFrequency.Weekly, 46);
        RunTaxPeriodTest(TaxYearEnding.Apr5_2022, new DateOnly(2022, 3, 20), PayFrequency.Weekly, 50);
        RunTaxPeriodTest(TaxYearEnding.Apr5_2022, new DateOnly(2022, 4, 3), PayFrequency.Weekly, 52);
        RunTaxPeriodTest(TaxYearEnding.Apr5_2022, new DateOnly(2022, 4, 5), PayFrequency.Weekly, 53);
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

    private static void RunTaxPeriodTest(TaxYearEnding taxYearEnding, DateOnly payDate, PayFrequency payFrequency, int expectedPeriodNumber)
    {
        var taxYear = new TaxYear(taxYearEnding);
        var periodNumber = taxYear.GetTaxPeriod(payDate, payFrequency);

        periodNumber.Should().Be(expectedPeriodNumber);
    }
}