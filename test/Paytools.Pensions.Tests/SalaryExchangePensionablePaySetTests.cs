using FluentAssertions;
using Paytools.Common.Model;

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
        var calculator = await GetCalculator(EarningsBasis.PensionablePaySet1, PensionTaxTreatment.NetPayArrangement);

        var pensionableSalary = 5366.59m;
        var employerContributionPct = 3.0m;
        decimal? employeeContributionPct = 4.0m;
        decimal? employeeContributionAmount = null;
        var avc = 0.0m;
        var employeeContributionIsAmount = false;

        TestCalculation(calculator, pensionableSalary, employerContributionPct,
            employeeContributionPct, employeeContributionAmount, avc, employeeContributionIsAmount,
            405.28m, 161.0m, 29.62m);
    }

    private static void TestCalculation(IPensionContributionCalculator calculator, decimal pensionableSalary,
        decimal employerContributionPct, decimal? employeeContributionPct, decimal? employeeContributionAmount, decimal avc,
        bool employeeContributionIsAmount, decimal expectedEmployerContribution, decimal expectedEmployerContributionBeforeSE,
        decimal expectedEmployerNiSaving)
    {
        var niSavingsCalculator = new EmployerNiSavingsCalculator(0.138m, 100.0m);

        var result = calculator.CalculateUnderSalaryExchange(pensionableSalary, employerContributionPct,
            niSavingsCalculator, (employeeContributionIsAmount ? employeeContributionAmount : employeeContributionPct) ?? 0.0m,
            employeeContributionIsAmount, avc);

        result.PensionableSalaryInPeriod.Should().Be(pensionableSalary);
        result.EmployeeContributionPercentage.Should().Be(employeeContributionPct);
        result.EmployeeContributionFixedAmount.Should().Be(employeeContributionAmount);
        result.EmployerContributionPercentage.Should().Be(employerContributionPct);
        result.EmployeeContributionAmount.Should().Be(avc);
        result.EmployerContributionAmount.Should().Be(expectedEmployerContribution);
        result.SalaryExchangeApplied.Should().Be(true);
        result.BandedEarnings.Should().BeNull();
        result.EarningsBasis.Should().BeOneOf(new[] { EarningsBasis.PensionablePaySet1, EarningsBasis.PensionablePaySet2, EarningsBasis.PensionablePaySet3 });
        result.EmployeeAvcAmount.Should().Be(avc);
        result.EmployerContributionAmountBeforeSalaryExchange.Should().Be(expectedEmployerContributionBeforeSE);
        result.EmployerNiSavingsToReinvest.Should().Be(expectedEmployerNiSaving);
    }

    private async Task<IPensionContributionCalculator> GetCalculator(EarningsBasis earningsBasis, PensionTaxTreatment taxTreatment, decimal? basicRateOfTax = null)
    {
        var provider = await _factoryProviderFixture.GetFactory();

        return provider.GetCalculator(earningsBasis, taxTreatment, _payDate, basicRateOfTax);
    }
}