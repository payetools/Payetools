using FluentAssertions;

namespace Paytools.Pensions.Tests;

public class SalaryExchangeQualifyingEarningsTests
{
    [Fact]
    public void TestEarningsBelowLowerLimitForQE()
    {
        var lowerLimit = 520.0m;
        var upperLimit = 4189.0m;

        QualifyingEarningsCalculator calculator = new(PensionTaxTreatment.Unspecified, lowerLimit, upperLimit);

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
    public void TestEarningsAtLowerLimitForQE()
    {
        var lowerLimit = 520.0m;
        var upperLimit = 4189.0m;

        QualifyingEarningsCalculator calculator = new(PensionTaxTreatment.Unspecified, lowerLimit, upperLimit);

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
    public void TestEarningsJustBelowUpperLimitForQE()
    {
        var lowerLimit = 520.0m;
        var upperLimit = 4189.0m;

        QualifyingEarningsCalculator calculator = new(PensionTaxTreatment.Unspecified, lowerLimit, upperLimit);

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
    public void TestEarningsAtUpperLimitForQE()
    {
        var lowerLimit = 520.0m;
        var upperLimit = 4189.0m;

        QualifyingEarningsCalculator calculator = new(PensionTaxTreatment.Unspecified, lowerLimit, upperLimit);

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
    public void TestEarningsAboveUpperLimitForQE()
    {
        var lowerLimit = 520.0m;
        var upperLimit = 4189.0m;

        QualifyingEarningsCalculator calculator = new(PensionTaxTreatment.Unspecified, lowerLimit, upperLimit);

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
        var niSavingsCalculator = new TestEmployerNiSavingsCalculator(0.138m, 100.0m);

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
        result.BandedEarnings.Should().Be(expectedBandedEarnings);
        result.EarningsBasis.Should().Be(EarningsBasis.QualifyingEarnings);
        result.EmployeeAvcAmount.Should().Be(avc);
        result.EmployerContributionAmountBeforeSalaryExchange.Should().Be(expectedEmployerContributionBeforeSE);
        result.EmployerNiSavings.Should().Be(expectedEmployerNiSaving);
    }
}