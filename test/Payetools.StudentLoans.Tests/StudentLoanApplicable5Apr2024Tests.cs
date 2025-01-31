// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using FluentAssertions;
using Payetools.Common.Model;
using Payetools.NationalMinimumWage.Tests;

namespace Payetools.StudentLoans.Tests;

public class StudentLoanApplicable5Apr2024Tests : IClassFixture<StudentLoanCalculatorFactoryDataFixture>
{
    private readonly TaxYear _taxYear = new TaxYear(TaxYearEnding.Apr5_2024);
    private readonly StudentLoanCalculatorFactoryDataFixture _factoryProviderFixture;

    public StudentLoanApplicable5Apr2024Tests(StudentLoanCalculatorFactoryDataFixture factoryProviderFixture)
    {
        _factoryProviderFixture = factoryProviderFixture;
    }

    [Fact]
    public async Task TestPlan1StudentLoanAsync()
    {
        var calculator = await GetCalculator(_taxYear, PayFrequency.Monthly, 1);

        RunTest(calculator, 1500.00m, StudentLoanType.Plan1, false, 0.0m, 0.0m, 1834.58m, null);

        RunTest(calculator, 1845.69m, StudentLoanType.Plan1, false, 0.0m, 0.0m, 1834.58m, null);

        RunTest(calculator, 1845.70m, StudentLoanType.Plan1, false, 1.0m, 0.0m, 1834.58m, null);

        RunTest(calculator, 8179.03m, StudentLoanType.Plan1, false, 571.0m, 0.0m, 1834.58m, null);
    }

    [Fact]
    public async Task TestPlan2StudentLoanAsync()
    {
        var calculator = await GetCalculator(_taxYear, PayFrequency.Weekly, 52);

        RunTest(calculator, 399.48m, StudentLoanType.Plan2, false, 0.0m, 0.0m, 524.90m, null);

        RunTest(calculator, 536.01m, StudentLoanType.Plan2, false, 0.0m, 0.0m, 524.90m, null);

        RunTest(calculator, 536.02m, StudentLoanType.Plan2, false, 1.0m, 0.0m, 524.90m, null);

        RunTest(calculator, 2113.89m, StudentLoanType.Plan2, false, 143.0m, 0.0m, 524.90m, null);
    }

    [Fact]
    public async Task TestPlan4StudentLoan()
    {
        var calculator = await GetCalculator(_taxYear, PayFrequency.Fortnightly, 10);

        RunTest(calculator, 187.06m, StudentLoanType.Plan4, false, 0.0m, 0.0m, 1063.84m, null);

        RunTest(calculator, 1074.94m, StudentLoanType.Plan4, false, 0.0m, 0.0m, 1063.84m, null);

        RunTest(calculator, 1074.95m, StudentLoanType.Plan4, false, 0.0m, 0.0m, 1063.84m, null);

        RunTest(calculator, 1074.96m, StudentLoanType.Plan4, false, 1.0m, 0.0m, 1063.84m, null);

        RunTest(calculator, 1075.06m, StudentLoanType.Plan4, false, 1.0m, 0.0m, 1063.84m, null);        

        RunTest(calculator, 1174.96m, StudentLoanType.Plan4, false, 10.0m, 0.0m, 1063.84m, null);

        RunTest(calculator, 5252.73m, StudentLoanType.Plan4, false, 377.0m, 0.0m, 1063.84m, null);
    }

    [Fact]
    public async Task TestPostGradLoan()
    {
        var calculator = await GetCalculator(_taxYear, PayFrequency.FourWeekly, 2);

        RunTest(calculator, 120.50m, null, true, 0.0m, 0.0m, null, 1615.38m);

        RunTest(calculator, 1632.04m, null, true, 0.0m, 0.0m, null, 1615.38m);

        RunTest(calculator, 1632.05m, null, true, 0.0m, 1.0m, null, 1615.38m);

        RunTest(calculator, 1632.21m, null, true, 0.0m, 1.0m, null, 1615.38m);

        RunTest(calculator, 11182.05m, null, true, 0.0m, 574.0m, null, 1615.38m);
    }

    [Fact]
    public async Task TestPlan1PlusPostGradLoan()
    {
        var calculator = await GetCalculator(_taxYear, PayFrequency.Monthly, 4);

        RunTest(calculator, 2334.69m, StudentLoanType.Plan1, true, 45.0m, 35.0m, 1834.58m, 1750.0m);
    }

    private void RunTest(IStudentLoanCalculator calculator, decimal grossSalary, StudentLoanType? studentLoanType, bool hasPostGradLoan,
        decimal expectedStudentLoanDeduction, decimal expectedPostGradLoanDeduction, decimal? expectedStudentLoanThreshold,
        decimal? expectedPostGradLoanThreshold)
    {
        calculator.Calculate(grossSalary, studentLoanType, hasPostGradLoan, out var result);

        result.StudentLoanType.Should().Be(studentLoanType);
        result.HasPostGradLoan.Should().Be(hasPostGradLoan, $"gross salary of £{grossSalary}");
        result.StudentLoanThresholdUsed.Should().Be(expectedStudentLoanThreshold, $"gross salary of £{grossSalary}");
        result.PostGradLoanThresholdUsed.Should().Be(expectedPostGradLoanThreshold, $"gross salary of £{grossSalary}");
        result.StudentLoanDeduction.Should().Be(expectedStudentLoanDeduction, $"gross salary of £{grossSalary}");
        result.PostgraduateLoanDeduction.Should().Be(expectedPostGradLoanDeduction, $"gross salary of £{grossSalary}");
        result.TotalDeduction.Should().Be(expectedStudentLoanDeduction + expectedPostGradLoanDeduction, $"gross salary of £{grossSalary}");
    }

    private async Task<IStudentLoanCalculator> GetCalculator(TaxYear taxYear, PayFrequency payFrequency, int taxPeriod)
    {
        var provider = await _factoryProviderFixture.GetFactory();
        var payDate = new PayDate(taxYear.GetLastDayOfTaxPeriod(payFrequency, taxPeriod), payFrequency);

        return provider.GetCalculator(payDate);
    }
}