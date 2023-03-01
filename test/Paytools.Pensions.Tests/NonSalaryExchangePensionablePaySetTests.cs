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

public class NonSalaryExchangePensionablePaySetTests : IClassFixture<PensionContributionsCalculatorFactoryDataFixture>
{
    private readonly PayDate _payDate = new PayDate(2022, 4, 6, PayFrequency.Monthly);
    private readonly PensionContributionsCalculatorFactoryDataFixture _factoryProviderFixture;

    public NonSalaryExchangePensionablePaySetTests(PensionContributionsCalculatorFactoryDataFixture factoryProviderFixture)
    {
        _factoryProviderFixture = factoryProviderFixture;
    }

    [Fact]
    public async Task TestPensionablePay_NPAAsync()
    {
        var calculator = await GetCalculator(EarningsBasis.PensionablePaySet1, PensionTaxTreatment.NetPayArrangement);

        var pensionableSalary = 4129.52m;
        var employerContributionPct = 3.0m;
        decimal? employerContributionAmount = null;
        decimal? employeeContributionPct = 5.0m;
        decimal? employeeContributionAmount = null;
        var avc = 0.0m;
        var employeeContributionIsAmount = false;

        TestCalculation(calculator, pensionableSalary, employerContributionPct, employerContributionAmount,
            employeeContributionPct, employeeContributionAmount, avc, employeeContributionIsAmount,
            123.89m, 206.48m);
    }

    [Fact]
    public async Task TestPensionablePay_RASAsync()
    {
        var calculator = await GetCalculator(EarningsBasis.PensionablePaySet2, PensionTaxTreatment.ReliefAtSource, 0.2m);

        var pensionableSalary = 3769.42m;
        var employerContributionPct = 3.0m;
        decimal? employerContributionAmount = null;
        decimal? employeeContributionPct = 5.0m;
        decimal? employeeContributionAmount = null;
        var avc = 0.0m;
        var employeeContributionIsAmount = false;

        TestCalculation(calculator, pensionableSalary, employerContributionPct, employerContributionAmount,
            employeeContributionPct, employeeContributionAmount, avc, employeeContributionIsAmount,
            113.08m, 150.78m);
    }

    private static void TestCalculation(IPensionContributionCalculator calculator, decimal pensionableSalary,
        decimal employerContributionPct, decimal? employerContributionAmount,
        decimal? employeeContributionPct, decimal? employeeContributionAmount, decimal avc,
        bool employeeContributionIsAmount, decimal expectedEmployerContribution, decimal expectedEmployeeContribution)
    {
        calculator.Calculate(pensionableSalary, employerContributionPct,
            (employeeContributionIsAmount ? employeeContributionAmount : employeeContributionPct) ?? 0.0m,
            employeeContributionIsAmount, avc, null, out var result);

        result.PensionableSalaryInPeriod.Should().Be(pensionableSalary);
        result.EmployeeContributionPercentage.Should().Be(employeeContributionPct);
        result.EmployeeContributionFixedAmount.Should().Be(employeeContributionAmount);
        result.EmployerContributionPercentage.Should().Be(employerContributionPct);
        result.CalculatedEmployeeContributionAmount.Should().Be(expectedEmployeeContribution);
        result.CalculatedEmployerContributionAmount.Should().Be(expectedEmployerContribution);
        result.SalaryExchangeApplied.Should().Be(false);
        result.BandedEarnings.Should().BeNull();
        result.EarningsBasis.Should().BeOneOf(new[] { EarningsBasis.PensionablePaySet1, EarningsBasis.PensionablePaySet2, EarningsBasis.PensionablePaySet3 });
        result.EmployeeAvcAmount.Should().Be(avc);
        result.EmployerContributionAmountBeforeSalaryExchange.Should().BeNull();
        result.EmployerNiSavingsToReinvest.Should().BeNull();
    }

    private async Task<IPensionContributionCalculator> GetCalculator(EarningsBasis earningsBasis, PensionTaxTreatment taxTreatment, decimal? basicRateOfTax = null)
    {
        var provider = await _factoryProviderFixture.GetFactory();

        return provider.GetCalculator(earningsBasis, taxTreatment, _payDate, basicRateOfTax);
    }
}