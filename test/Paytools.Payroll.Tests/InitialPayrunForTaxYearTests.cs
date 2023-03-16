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

using Paytools.Common.Model;
using Paytools.Employment.Model;
using Paytools.NationalMinimumWage;
using Paytools.Payroll;
using Paytools.Payroll.Model;
using Paytools.Payroll.Payruns;
using System.Collections.Immutable;
using Paytools.Payroll.Extensions;
using Paytools.NationalInsurance.Model;
using Paytools.Pensions.Model;
using Paytools.IncomeTax.Model;
using Paytools.Testing.Data.EndToEnd;

namespace Paytools.Payroll.Tests;

public class InitialPayrunForTaxYearTests : IClassFixture<PayrollProcessorFactoryFixture>
{
    private readonly PayDate _payDate = new PayDate(2022, 5, 5, PayFrequency.Monthly);
    private readonly PayrollProcessorFactoryFixture _payrollProcessorFactoryFixture;

    public InitialPayrunForTaxYearTests(PayrollProcessorFactoryFixture payrollProcessorFactoryFixture)
    {
        _payrollProcessorFactoryFixture = payrollProcessorFactoryFixture;
    }
    [Fact]
    public async Task Test1Async()
    {
        IEndToEndTestDataSet testData = EndToEndTestDataSource.GetAllData();

        PayDate payDate = new PayDate(2022, 8, 20, PayFrequency.Monthly);
        PayReferencePeriod payPeriod = new PayReferencePeriod(new DateOnly(), new DateOnly());

        IEmployer employer = new Employer();

        var processor = await GetProcessor(employer, payDate, payPeriod);

        GetHistory(out var prevHistory);

        if (prevHistory == null)
            throw new InvalidOperationException("History can't be null");

        var entry = GetAugustEntry(employer, prevHistory);

        List<IEmployeePayrunInputEntry> entries = new List<IEmployeePayrunInputEntry>();


        entries.Add(entry);
        processor.Process(entries, out var result);

        IEmployeePayrollHistoryYtd historyYtd = prevHistory.Add(result.EmployeePayrunEntries[0]);

        Console.WriteLine(result.EmployeePayrunEntries[0].NiCalculationResult.ToString());
        Console.WriteLine();

        //IHtmlPayslipService service = new HtmlPayslipService(new RazorHtmlRenderingService());

        //IPayslip payslip = PayslipModelMapper.Map(employer, entry, result.EmployeePayrunEntries[0], historyYtd);

        //var html = await service.RenderAsync("Templates.Payslips.Default.cshtml", payslip);

        //Console.WriteLine(html);

        //File.WriteAllText(@"c:\temp\output.html", html);

        Console.WriteLine();

    }

    static void GetHistory(out IEmployeePayrollHistoryYtd history)
    {
        var niEntries = ImmutableList<IEmployeeNiHistoryEntry>.Empty;

        niEntries = niEntries.Add(new EmployeeNiHistoryEntry(NiCategory.A, new NiEarningsBreakdown(), 28333.32m - 1841.69m, 2070.55m, 3530.64m, 2070.55m + 3530.64m));

        NiYtdHistory niHistory = new NiYtdHistory(niEntries);

        history = new EmployeePayrollHistoryYtd()
        {
            EmployeeNiHistoryEntries = niHistory,
            TaxablePayYtd = 28333.32m - 1841.69m + 450.12m,
            NicablePayYtd = 28333.32m - 1841.69m,
            TaxPaidYtd = 6533.86m
        };
    }

    static EmployeePayrunInputEntry GetAugustEntry(in IEmployer employer, in IEmployeePayrollHistoryYtd history)
    {
        var employee = new Employee()
        {
            FirstName = "Stephen",
            LastName = "Wilkinson"
        };

        TaxCode.TryParse("1296L", out var taxCode);

        var employment = new Paytools.Payroll.Model.Employment(history)
        {
            TaxCode = taxCode,
            NiCategory = NiCategory.A,
            PensionScheme = new PensionScheme()
            {
                EarningsBasis = EarningsBasis.PensionablePaySet1,
                TaxTreatment = PensionTaxTreatment.ReliefAtSource
            },
            //IsDirector = true,
            //DirectorsNiCalculationMethod = DirectorsNiCalculationMethod.AlternativeMethod
        };

        var earnings = ImmutableList<EarningsEntry>.Empty;
        var deductions = ImmutableList<DeductionEntry>.Empty;
        var payrolledBenefits = ImmutableList<IPayrolledBenefitForPeriod>.Empty;

        earnings = earnings.Add(new EarningsEntry()
        {
            EarningsDetails = new GenericEarnings()
            {
                IsSubjectToTax = true,
                IsSubjectToNi = true,
                IsPensionable = true,
                IsNetToGross = false
            },
            FixedAmount = 6083.33m
        });

        earnings = earnings.Add(new EarningsEntry()
        {
            EarningsDetails = new GenericEarnings()
            {
                IsSubjectToTax = true,
                IsSubjectToNi = true,
                IsPensionable = true,
                IsNetToGross = false
            },
            FixedAmount = 1000.0m
        });

        payrolledBenefits = payrolledBenefits.Add(new PayrolledBenefitForPeriod(150.05m));

        var pensionContributionLevels = new PensionContributionLevels()
        {
            EmployeeContribution = 495.84m,
            EmployeeContributionIsFixedAmount = true,
            EmployerContributionPercentage = 3,
            EmployersNiReinvestmentPercentage = 100,
            SalaryExchangeApplied = true
        };

        return new EmployeePayrunInputEntry(employee,
            employment,
            earnings,
            deductions,
            payrolledBenefits,
            pensionContributionLevels);
    }

    private async Task<IPayrunProcessor> GetProcessor(IEmployer employer, PayDate payDate, PayReferencePeriod payPeriod)
    {
        var factory = await _payrollProcessorFactoryFixture.GetFactory();

        return await factory.GetProcessorAsync(employer, payDate, payPeriod);
    }
}


