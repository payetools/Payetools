// Copyright (c) 2023 Paytools Foundation.
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
using Paytools.Pensions.Model;

namespace Paytools.Pensions.Tests;

public class SalaryExchangeQualifyingEarningsTests : IClassFixture<PensionContributionsCalculatorFactoryDataFixture>
{
    private readonly PayDate _payDate = new PayDate(2022, 4, 6, PayFrequency.Monthly);
    private readonly PensionContributionsCalculatorFactoryDataFixture _factoryProviderFixture;

    public SalaryExchangeQualifyingEarningsTests(PensionContributionsCalculatorFactoryDataFixture factoryProviderFixture)
    {
        _factoryProviderFixture = factoryProviderFixture;
    }

    [Fact]
    public async Task TestEarningsBelowLowerLimitForQEAsync()
    {
        var lowerLimit = 520.0m;
        var upperLimit = 4189.0m;

        var calculator = await GetCalculator(PensionsEarningsBasis.QualifyingEarnings, PensionTaxTreatment.Unspecified);

        var pensionableSalary = 519.0m;
        var expectedBandedEarnings = pensionableSalary > lowerLimit ? Math.Min(pensionableSalary, upperLimit) - lowerLimit : 0.0m;
        var employerContributionPct = 3.0m;
        decimal? employerContributionAmount = null;
        decimal? employeeContributionPct = 5.0m;
        decimal? employeeContributionAmount = null;
        var avc = 0.0m;
        var employeeContributionIsAmount = false;

        TestCalculation(calculator, pensionableSalary, expectedBandedEarnings, employerContributionPct,
            employerContributionAmount, employeeContributionPct, employeeContributionAmount, avc, employeeContributionIsAmount,
            0.0m, 0.0m, 0.0m);
    }

    [Fact]
    public async Task TestEarningsAtLowerLimitForQEAsync()
    {
        var lowerLimit = 520.0m;
        var upperLimit = 4189.0m;

        var calculator = await GetCalculator(PensionsEarningsBasis.QualifyingEarnings, PensionTaxTreatment.Unspecified);

        var pensionableSalary = 520.0m;
        var expectedBandedEarnings = pensionableSalary > lowerLimit ? Math.Min(pensionableSalary, upperLimit) - lowerLimit : 0.0m;
        var employerContributionPct = 3.0m;
        decimal? employerContributionAmount = null;
        decimal? employeeContributionPct = 5.0m;
        decimal? employeeContributionAmount = null;
        var avc = 0.0m;
        var employeeContributionIsAmount = false;

        TestCalculation(calculator, pensionableSalary, expectedBandedEarnings, employerContributionPct,
            employerContributionAmount, employeeContributionPct, employeeContributionAmount, avc, employeeContributionIsAmount,
            0.0m, 0.0m, 0.0m);
    }

    [Fact]
    public async Task TestEarningsJustBelowUpperLimitForQEAsync()
    {
        var lowerLimit = 520.0m;
        var upperLimit = 4189.0m;

        var calculator = await GetCalculator(PensionsEarningsBasis.QualifyingEarnings, PensionTaxTreatment.Unspecified);

        var pensionableSalary = 4188.0m;
        var expectedBandedEarnings = pensionableSalary > lowerLimit ? Math.Min(pensionableSalary, upperLimit) - lowerLimit : 0.0m;
        var employerContributionPct = 3.0m;
        decimal? employerContributionAmount = null;
        decimal? employeeContributionPct = 5.0m;
        decimal? employeeContributionAmount = null;
        var avc = 0.0m;
        var employeeContributionIsAmount = false;

        TestCalculation(calculator, pensionableSalary, expectedBandedEarnings, employerContributionPct,
            employerContributionAmount, employeeContributionPct, employeeContributionAmount, avc, employeeContributionIsAmount,
            318.75m, 110.04m, 25.31m);
    }

    [Fact]
    public async Task TestEarningsAtUpperLimitForQEAsync()
    {
        var lowerLimit = 520.0m;
        var upperLimit = 4189.0m;

        var calculator = await GetCalculator(PensionsEarningsBasis.QualifyingEarnings, PensionTaxTreatment.Unspecified);

        var pensionableSalary = 4189.0m;
        var expectedBandedEarnings = pensionableSalary > lowerLimit ? Math.Min(pensionableSalary, upperLimit) - lowerLimit : 0.0m;
        var employerContributionPct = 3.0m;
        decimal? employerContributionAmount = null;
        decimal? employeeContributionPct = 5.0m;
        decimal? employeeContributionAmount = null;
        var avc = 0.0m;
        var employeeContributionIsAmount = false;

        TestCalculation(calculator, pensionableSalary, expectedBandedEarnings, employerContributionPct,
            employerContributionAmount, employeeContributionPct, employeeContributionAmount, avc, employeeContributionIsAmount,
            318.84m, 110.07m, 25.32m);
    }

    [Fact]
    public async Task TestEarningsAboveUpperLimitForQEAsync()
    {
        var lowerLimit = 520.0m;
        var upperLimit = 4189.0m;

        var calculator = await GetCalculator(PensionsEarningsBasis.QualifyingEarnings, PensionTaxTreatment.Unspecified);

        var pensionableSalary = 5000.0m;
        var expectedBandedEarnings = pensionableSalary > lowerLimit ? Math.Min(pensionableSalary, upperLimit) - lowerLimit : 0.0m;
        var employerContributionPct = 3.0m;
        decimal? employerContributionAmount = null;
        decimal? employeeContributionPct = 5.0m;
        decimal? employeeContributionAmount = null;
        var avc = 0.0m;
        var employeeContributionIsAmount = false;

        TestCalculation(calculator, pensionableSalary, expectedBandedEarnings, employerContributionPct,
            employerContributionAmount, employeeContributionPct, employeeContributionAmount, avc, employeeContributionIsAmount,
            318.84m, 110.07m, 25.32m);
    }

    private static void TestCalculation(IPensionContributionCalculator calculator, decimal pensionableSalary,
        decimal expectedBandedEarnings, decimal employerContributionPct, decimal? employerContributionAmount,
        decimal? employeeContributionPct, decimal? employeeContributionAmount, decimal avc,
        bool employeeContributionIsAmount, decimal expectedEmployerContribution, decimal expectedEmployerContributionBeforeSE,
        decimal expectedEmployerNiSaving)
    {
        decimal employersNiSaving = 0.138m * (employeeContributionIsAmount ?
            employeeContributionAmount :
            (employeeContributionPct / 100.0m) * expectedBandedEarnings) ?? 0.0m;

        calculator.CalculateUnderSalaryExchange(pensionableSalary, employerContributionPct,
            employersNiSaving, 100.0m, (employeeContributionIsAmount ? employeeContributionAmount : employeeContributionPct) ?? 0.0m,
            employeeContributionIsAmount, avc, null, out var result);

        result.PensionableSalaryInPeriod.Should().Be(pensionableSalary);
        result.EmployeeContributionPercentage.Should().Be(employeeContributionPct);
        result.EmployeeContributionFixedAmount.Should().Be(employeeContributionAmount);
        result.EmployerContributionPercentage.Should().Be(employerContributionPct);
        result.CalculatedEmployeeContributionAmount.Should().Be(avc);
        result.CalculatedEmployerContributionAmount.Should().Be(expectedEmployerContribution);
        result.SalaryExchangeApplied.Should().Be(true);
        result.BandedEarnings.Should().Be(expectedBandedEarnings);
        result.EarningsBasis.Should().Be(PensionsEarningsBasis.QualifyingEarnings);
        result.EmployeeAvcAmount.Should().Be(avc);
        result.EmployerContributionAmountBeforeSalaryExchange.Should().Be(expectedEmployerContributionBeforeSE);
        result.EmployerNiSavingsToReinvest.Should().Be(expectedEmployerNiSaving);
    }

    private async Task<IPensionContributionCalculator> GetCalculator(PensionsEarningsBasis earningsBasis, PensionTaxTreatment taxTreatment)
    {
        var provider = await _factoryProviderFixture.GetFactory();

        return provider.GetCalculator(earningsBasis, taxTreatment, _payDate);
    }
}