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
using Paytools.NationalMinimumWage.Tests;

namespace Paytools.StudentLoans.Tests;

public class StudentLoanApplicableTests : IClassFixture<StudentLoanCalculatorFactoryDataFixture>
{
    private readonly TaxYear _taxYear = new TaxYear(TaxYearEnding.Apr5_2023);
    private readonly StudentLoanCalculatorFactoryDataFixture _factoryProviderFixture;

    public StudentLoanApplicableTests(StudentLoanCalculatorFactoryDataFixture factoryProviderFixture)
    {
        _factoryProviderFixture = factoryProviderFixture;
    }

    [Fact]
    public async Task TestPlan1StudentLoanAsync()
    {
        var calculator = await GetCalculator(_taxYear, PayFrequency.Monthly, 1);

        RunTest(calculator, 1500.00m, StudentLoanType.Plan1, false, 0.0m, 0.0m, 1682.91m, null);

        RunTest(calculator, 1694.02m, StudentLoanType.Plan1, false, 0.0m, 0.0m, 1682.91m, null);

        RunTest(calculator, 1694.03m, StudentLoanType.Plan1, false, 1.0m, 0.0m, 1682.91m, null);

        RunTest(calculator, 8027.36m, StudentLoanType.Plan1, false, 571.0m, 0.0m, 1682.91m, null);
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
        var calculator = await GetCalculator(_taxYear, PayFrequency.TwoWeekly, 10);

        RunTest(calculator, 187.06m, StudentLoanType.Plan4, false, 0.0m, 0.0m, 975.96m, null);

        RunTest(calculator, 987.06m, StudentLoanType.Plan4, false, 0.0m, 0.0m, 975.96m, null);

        RunTest(calculator, 987.07m, StudentLoanType.Plan4, false, 0.0m, 0.0m, 975.96m, null);

        RunTest(calculator, 987.18m, StudentLoanType.Plan4, false, 1.0m, 0.0m, 975.96m, null);

        RunTest(calculator, 1087.07m, StudentLoanType.Plan4, false, 9.0m, 0.0m, 975.96m, null);

        RunTest(calculator, 5164.85m, StudentLoanType.Plan4, false, 377.0m, 0.0m, 975.96m, null);
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

        RunTest(calculator, 2350.12m, StudentLoanType.Plan1, true, 60.0m, 36.0m, 1682.91m, 1750.0m);
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
        result.PostGraduateLoanDeduction.Should().Be(expectedPostGradLoanDeduction, $"gross salary of £{grossSalary}");
        result.TotalDeduction.Should().Be(expectedStudentLoanDeduction + expectedPostGradLoanDeduction, $"gross salary of £{grossSalary}");
    }

    private async Task<IStudentLoanCalculator> GetCalculator(TaxYear taxYear, PayFrequency payFrequency, int taxPeriod)
    {
        var provider = await _factoryProviderFixture.GetFactory();
        var payDate = new PayDate(taxYear.GetLastDayOfTaxPeriod(payFrequency, taxPeriod), payFrequency);

        return provider.GetCalculator(payDate);
    }
}