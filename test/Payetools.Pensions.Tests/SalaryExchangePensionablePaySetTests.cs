// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using FluentAssertions;
using Payetools.Common.Model;
using Payetools.Pensions.Model;

namespace Payetools.Pensions.Tests;

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

    private static void TestCalculation(
        IPensionContributionCalculator calculator,
        decimal pensionableSalary,
        decimal employerContributionPct,
        decimal? employeeContributionPct,
        decimal? employeeContributionAmount,
        decimal avc,
        bool employeeContributionIsAmount,
        decimal employersNiSaving,
        decimal expectedEmployerContribution,
        decimal expectedEmployerContributionBeforeSE,
        decimal expectedEmployerNiSaving)
    {
        calculator.CalculateUnderSalaryExchange(
            pensionableSalary,
            employerContributionPct,
            false,
            employersNiSaving,
            100.0m,
            (employeeContributionIsAmount ? employeeContributionAmount : employeeContributionPct) ?? 0.0m,
            employeeContributionIsAmount,
            avc,
            null,
            out var result);

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