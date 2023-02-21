using FluentAssertions;

namespace Paytools.Pensions.Tests;

public class NonSalaryExchangeQualifyingEarningsTests
{
    [Fact]
    public void TestEarningsBelowLowerLimitForQE_NPA()
    {
        var lowerLimit = 520.0m;
        var upperLimit = 4189.0m;

        QualifyingEarningsCalculator calculator = new(PensionTaxTreatment.NetPayArrangement, lowerLimit, upperLimit);

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
            0.0m, 0.0m);
    }

    [Fact]
    public void TestEarningsAtLowerLimitForQE_NPA()
    {
        var lowerLimit = 520.0m;
        var upperLimit = 4189.0m;

        QualifyingEarningsCalculator calculator = new(PensionTaxTreatment.NetPayArrangement, lowerLimit, upperLimit);

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
            0.0m, 0.0m);
    }

    [Fact]
    public void TestEarningsJustBelowUpperLimitForQE_NPA()
    {
        var lowerLimit = 520.0m;
        var upperLimit = 4189.0m;

        QualifyingEarningsCalculator calculator = new(PensionTaxTreatment.NetPayArrangement, lowerLimit, upperLimit);

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
            110.04m, 183.40m);
    }

    [Fact]
    public void TestEarningsAtUpperLimitForQE_NPA()
    {
        var lowerLimit = 520.0m;
        var upperLimit = 4189.0m;

        QualifyingEarningsCalculator calculator = new(PensionTaxTreatment.NetPayArrangement, lowerLimit, upperLimit);

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
            110.07m, 183.45m);
    }

    [Fact]
    public void TestEarningsAboveUpperLimitForQE_NPA()
    {
        var lowerLimit = 520.0m;
        var upperLimit = 4189.0m;

        QualifyingEarningsCalculator calculator = new(PensionTaxTreatment.NetPayArrangement, lowerLimit, upperLimit);

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
            110.07m, 183.45m);
    }

    [Fact]
    public void TestEarningsBelowLowerLimitForQE_RAS()
    {
        var lowerLimit = 520.0m;
        var upperLimit = 4189.0m;

        QualifyingEarningsCalculator calculator = new(PensionTaxTreatment.ReliefAtSource, lowerLimit, upperLimit, 0.2m);

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
            0.0m, 0.0m);
    }

    [Fact]
    public void TestEarningsAtLowerLimitForQE_RAS()
    {
        var lowerLimit = 520.0m;
        var upperLimit = 4189.0m;

        QualifyingEarningsCalculator calculator = new(PensionTaxTreatment.ReliefAtSource, lowerLimit, upperLimit, 0.2m);

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
            0.0m, 0.0m);
    }

    [Fact]
    public void TestEarningsJustBelowUpperLimitForQE_RAS()
    {
        var lowerLimit = 520.0m;
        var upperLimit = 4189.0m;

        QualifyingEarningsCalculator calculator = new(PensionTaxTreatment.ReliefAtSource, lowerLimit, upperLimit, 0.2m);

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
            110.04m, 146.72m);
    }

    [Fact]
    public void TestEarningsAtUpperLimitForQE_RAS()
    {
        var lowerLimit = 520.0m;
        var upperLimit = 4189.0m;

        QualifyingEarningsCalculator calculator = new(PensionTaxTreatment.ReliefAtSource, lowerLimit, upperLimit, 0.2m);

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
            110.07m, 146.76m);
    }

    [Fact]
    public void TestEarningsAboveUpperLimitForQE_RAS()
    {
        var lowerLimit = 520.0m;
        var upperLimit = 4189.0m;

        QualifyingEarningsCalculator calculator = new(PensionTaxTreatment.ReliefAtSource, lowerLimit, upperLimit, 0.2m);

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
            110.07m, 146.76m);
    }

    private static void TestCalculation(IPensionContributionCalculator calculator, decimal pensionableSalary, 
        decimal expectedBandedEarnings, decimal employerContributionPct, decimal? employerContributionAmount, 
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
        result.BandedEarnings.Should().Be(expectedBandedEarnings);
        result.EarningsBasis.Should().Be(EarningsBasis.QualifyingEarnings);
        result.EmployeeAvcAmount.Should().Be(avc);
        result.EmployerContributionAmountBeforeSalaryExchange.Should().BeNull();
        result.EmployerNiSavings.Should().BeNull();
    }
}