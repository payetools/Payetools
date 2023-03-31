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

public class SalaryExchangePensionablePaySetTests : IClassFixture<PensionContributionsCalculatorFactoryDataFixture>
{
    private readonly PayDate _payDate = new PayDate(2022, 4, 6, PayFrequency.Monthly);
    private readonly PensionContributionsCalculatorFactoryDataFixture _factoryProviderFixture;

    public SalaryExchangePensionablePaySetTests(PensionContributionsCalculatorFactoryDataFixture factoryProviderFixture)
    {
        _factoryProviderFixture = factoryProviderFixture;
    }

    [Fact]
    public async Task TestPensionablePayAsync()
    {
        var calculator = await GetCalculator(PensionsEarningsBasis.PensionablePaySet1, PensionTaxTreatment.NetPayArrangement);

        var pensionableSalary = 5366.59m;
        var employerContributionPct = 3.0m;
        decimal? employeeContributionPct = 4.0m;
        decimal? employeeContributionAmount = null;
        var avc = 0.0m;
        var employeeContributionIsAmount = false;

        decimal employersNiSaving = 0.138m * (employeeContributionIsAmount ?
            employeeContributionAmount :
            (employeeContributionPct / 100.0m) * pensionableSalary) ?? 0.0m;

        TestCalculation(calculator, pensionableSalary, employerContributionPct,
            employeeContributionPct, employeeContributionAmount, avc, employeeContributionIsAmount,
            employersNiSaving, 405.28m, 161.0m, 29.62m);
    }

    private static void TestCalculation(IPensionContributionCalculator calculator, decimal pensionableSalary,
        decimal employerContributionPct, decimal? employeeContributionPct, decimal? employeeContributionAmount, decimal avc,
        bool employeeContributionIsAmount, decimal employersNiSaving, decimal expectedEmployerContribution, decimal expectedEmployerContributionBeforeSE,
        decimal expectedEmployerNiSaving)
    {
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
        result.BandedEarnings.Should().BeNull();
        result.EarningsBasis.Should().BeOneOf(new[] { PensionsEarningsBasis.PensionablePaySet1, PensionsEarningsBasis.PensionablePaySet2, PensionsEarningsBasis.PensionablePaySet3 });
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