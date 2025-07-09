// Copyright (c) 2023-2025, Payetools Ltd.
//
// Payetools Ltd licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using FluentAssertions;
using Payetools.Common.Model;
using Payetools.Pensions.Model;

namespace Payetools.Pensions.Tests;

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
        var calculator = await GetCalculator(PensionsEarningsBasis.PensionablePaySet1, PensionTaxTreatment.NetPayArrangement);

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
        var calculator = await GetCalculator(PensionsEarningsBasis.PensionablePaySet2, PensionTaxTreatment.ReliefAtSource);

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
        calculator.Calculate(pensionableSalary,
            employerContributionPct,
            false,
            (employeeContributionIsAmount ? employeeContributionAmount : employeeContributionPct) ?? 0.0m,
            employeeContributionIsAmount,
            avc,
            null,
            out var result);

        result.PensionableSalaryInPeriod.Should().Be(pensionableSalary);
        result.EmployeeContributionPercentage.Should().Be(employeeContributionPct);
        result.EmployeeContributionFixedAmount.Should().Be(employeeContributionAmount);
        result.EmployerContributionPercentage.Should().Be(employerContributionPct);
        result.CalculatedEmployeeContributionAmount.Should().Be(expectedEmployeeContribution);
        result.CalculatedEmployerContributionAmount.Should().Be(expectedEmployerContribution);
        result.SalaryExchangeApplied.Should().Be(false);
        result.BandedEarnings.Should().BeNull();
        result.EarningsBasis.Should().BeOneOf(new[] { PensionsEarningsBasis.PensionablePaySet1, PensionsEarningsBasis.PensionablePaySet2, PensionsEarningsBasis.PensionablePaySet3 });
        result.EmployeeAvcAmount.Should().Be(avc);
        result.EmployerContributionAmountBeforeSalaryExchange.Should().BeNull();
        result.EmployerNiSavingsToReinvest.Should().BeNull();
    }

    private async Task<IPensionContributionCalculator> GetCalculator(PensionsEarningsBasis earningsBasis, PensionTaxTreatment taxTreatment)
    {
        var provider = await _factoryProviderFixture.GetFactory();

        return provider.GetCalculator(earningsBasis, taxTreatment, _payDate);
    }
}