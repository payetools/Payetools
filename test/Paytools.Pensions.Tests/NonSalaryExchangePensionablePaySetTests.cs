using FluentAssertions;

namespace Paytools.Pensions.Tests;

public class NonSalaryExchangePensionablePaySetTests
{
    [Fact]
    public void TestPensionablePay_NPA()
    {
        PensionablePaySetCalculator calculator = new(EarningsBasis.PensionablePaySet1, PensionTaxTreatment.NetPayArrangement);

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
    public void TestPensionablePay_RAS()
    {
        PensionablePaySetCalculator calculator = new(EarningsBasis.PensionablePaySet2, PensionTaxTreatment.ReliefAtSource, 0.2m);

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
        var result = calculator.Calculate(pensionableSalary, employerContributionPct,
            (employeeContributionIsAmount ? employeeContributionAmount : employeeContributionPct) ?? 0.0m,
            employeeContributionIsAmount, avc);

        result.PensionableSalaryInPeriod.Should().Be(pensionableSalary);
        result.EmployeeContributionPercentage.Should().Be(employeeContributionPct);
        result.EmployeeContributionFixedAmount.Should().Be(employeeContributionAmount);
        result.EmployerContributionPercentage.Should().Be(employerContributionPct);
        result.EmployeeContributionAmount.Should().Be(expectedEmployeeContribution);
        result.EmployerContributionAmount.Should().Be(expectedEmployerContribution);
        result.SalaryExchangeApplied.Should().Be(false);
        result.BandedEarnings.Should().BeNull();
        result.EarningsBasis.Should().BeOneOf(new[] { EarningsBasis.PensionablePaySet1, EarningsBasis.PensionablePaySet2, EarningsBasis.PensionablePaySet3 });
        result.EmployeeAvcAmount.Should().Be(avc);
        result.EmployerContributionAmountBeforeSalaryExchange.Should().BeNull();
        result.EmployerNiSavings.Should().BeNull();
    }
}