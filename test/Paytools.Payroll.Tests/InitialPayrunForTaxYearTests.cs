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
using System.Collections.Specialized;

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

        MakeEmployeePayrollHistory(testData.PreviousYtdInputs.Where(pyi => pyi.TestReference == "Pay1").First(),
            testData.NiYtdHistory.Where(nyh => nyh.TestReference == "Pay1").ToList(),
            out var employeePayrollHistory);

        var staticInput = testData.StaticInputs.Where(si => si.TestReference == "Pay1").First();

        IEmployer employer = new Employer();

        MakeEmployeePayrunInput(employer,
            staticInput,
            testData.PensionSchemes.Where(ps => ps.SchemeName == staticInput.PensionScheme).FirstOrDefault(),
            employeePayrollHistory,
            testData.PeriodInputs,
            out var payrunEntry);



        PayDate payDate = new PayDate(2022, 8, 20, PayFrequency.Monthly);
        PayReferencePeriod payPeriod = new PayReferencePeriod(new DateOnly(), new DateOnly());


        var processor = await GetProcessor(employer, payDate, payPeriod);

        //MakeEmployeePayrollHistory(out var prevHistory);

        if (employeePayrollHistory == null)
            throw new InvalidOperationException("History can't be null");

        var entry = GetAugustEntry(employer, employeePayrollHistory);

        List<IEmployeePayrunInputEntry> entries = new List<IEmployeePayrunInputEntry>();


        entries.Add(entry);
        processor.Process(entries, out var result);

        IEmployeePayrollHistoryYtd historyYtd = employeePayrollHistory.Add(result.EmployeePayrunEntries[0]);

        Console.WriteLine(result.EmployeePayrunEntries[0].NiCalculationResult.ToString());
        Console.WriteLine();

        //IHtmlPayslipService service = new HtmlPayslipService(new RazorHtmlRenderingService());

        //IPayslip payslip = PayslipModelMapper.Map(employer, entry, result.EmployeePayrunEntries[0], historyYtd);

        //var html = await service.RenderAsync("Templates.Payslips.Default.cshtml", payslip);

        //Console.WriteLine(html);

        //File.WriteAllText(@"c:\temp\output.html", html);

        Console.WriteLine();

    }

    //static void MakeEmployeePayrollHistory(out IEmployeePayrollHistoryYtd history)
    //{
    //    var niEntries = ImmutableList<IEmployeeNiHistoryEntry>.Empty;

    //    niEntries = niEntries.Add(new EmployeeNiHistoryEntry(NiCategory.A, new NiEarningsBreakdown(), 28333.32m - 1841.69m, 2070.55m, 3530.64m, 2070.55m + 3530.64m));

    //    NiYtdHistory niHistory = new NiYtdHistory(niEntries);

    //    history = new EmployeePayrollHistoryYtd()
    //    {
    //        EmployeeNiHistoryEntries = niHistory,
    //        TaxablePayYtd = 28333.32m - 1841.69m + 450.12m,
    //        NicablePayYtd = 28333.32m - 1841.69m,
    //        TaxPaidYtd = 6533.86m
    //    };
    //}


    static void MakeEmployeePayrollHistory(in IPreviousYtdTestDataEntry previousYtd, 
        in List<INiYtdHistoryTestDataEntry> niYtdHistory, out IEmployeePayrollHistoryYtd history)
    {
        var niHistoryEntries = niYtdHistory.Select(nih => new EmployeeNiHistoryEntry(
            nih.NiCategoryPertaining,
            new NiEarningsBreakdown()
            {
                EarningsUpToAndIncludingLEL = nih.EarningsUpToAndIncludingLEL,
                EarningsAboveLELUpToAndIncludingST = nih.EarningsAboveLELUpToAndIncludingST,
                EarningsAboveSTUpToAndIncludingPT = nih.EarningsAboveSTUpToAndIncludingPT,
                EarningsAbovePTUpToAndIncludingFUST = nih.EarningsAbovePTUpToAndIncludingFUST,
                EarningsAboveFUSTUpToAndIncludingUEL = nih.EarningsAboveFUSTUpToAndIncludingUEL,
                EarningsAboveSTUpToAndIncludingUEL = nih.EarningsAboveSTUpToAndIncludingUEL,
                EarningsAboveUEL = nih.EarningsAboveUEL
            },
            nih.GrossNicableEarnings,
            nih.EmployeeContribution,
            nih.EmployerContribution,
            nih.TotalContribution) as IEmployeeNiHistoryEntry)
            .ToImmutableList();

        history = new EmployeePayrollHistoryYtd()
        {
            EmployeeNiHistoryEntries = new NiYtdHistory(niHistoryEntries),
            GrossPayYtd = previousYtd.GrossPayYtd,
            NicablePayYtd = previousYtd.NicablePayYtd,
            TaxablePayYtd = previousYtd.TaxablePayYtd,
            TaxPaidYtd = previousYtd.TaxPaidYtd,
            TaxUnpaidDueToRegulatoryLimit = previousYtd.TaxUnpaidDueToRegulatoryLimit,
            PayrolledBenefitsYtd = previousYtd.PayrolledBenefitsYtd,
            StudentLoanRepaymentsYtd = previousYtd.StudentLoanRepaymentsYtd,
            GraduateLoanRepaymentsYtd = previousYtd.GraduateLoanRepaymentsYtd,
            SharedParentalPayYtd = previousYtd.SharedParentalPayYtd,
            StatutoryMaternityPayYtd = previousYtd.StatutoryMaternityPayYtd,
            StatutoryAdoptionPayYtd = previousYtd.StatutoryAdoptionPayYtd,
            StatutoryPaternityPayYtd = previousYtd.StatutoryPaternityPayYtd,
            StatutoryParentalBereavementPayYtd = previousYtd.StatutoryParentalBereavementPayYtd,
            EmployeePensionContributionsUnderNpaYtd = previousYtd.EmployeePensionContributionsUnderNpaYtd,
            EmployeePensionContributionsUnderRasYtd = previousYtd.EmployeePensionContributionsUnderRasYtd,
            EmployerPensionContributionsYtd = previousYtd.EmployerPensionContributionsYtd

            // TODO: Earnings and Deductions histories
        };
    }


    static void MakeEmployeePayrunInput(
        in IEmployer employer,
        in IStaticInputTestDataEntry staticEntry,
        in IPensionSchemesTestDataEntry? pensionScheme,
        in IEmployeePayrollHistoryYtd history,
        in List<IPeriodInputTestDataEntry> periodInputs,
        out EmployeePayrunInputEntry entry)
    {
        var employee = new Employee()
        {
            FirstName = staticEntry.EmployeeFirstName,
            LastName = staticEntry.EmployeeLastName
        };

        var employment = new Model.Employment(history)
        {
            TaxCode = staticEntry.TaxCode,
            NiCategory = staticEntry.NiCategory,
            StudentLoanStatus = (staticEntry.StudentLoanPlan != null || staticEntry.GraduateLoan) ?
                new StudentLoanStatus() { StudentLoanType = staticEntry.StudentLoanPlan, HasPostGradLoan = staticEntry.GraduateLoan } :
                null,
            PensionScheme = staticEntry.PensionScheme != null ? new PensionScheme()
            {
                TaxTreatment = pensionScheme?.TaxTreatment ?? throw new ArgumentNullException("Pension scheme name doesn't match a value pension", nameof(pensionScheme)),
                EarningsBasis = pensionScheme?.EarningsBasis ?? throw new ArgumentNullException("Pension scheme name doesn't match a value pension", nameof(pensionScheme))
            } : null,
            DefaultPensionContributionLevels = staticEntry.PensionScheme != null ? new PensionContributionLevels()
            {
                EmployerContributionPercentage = staticEntry.EmployerPercentage ?? 0.0m,
                EmployeeContribution = staticEntry.EmployeeFixedAmount ?? staticEntry.EmployeePercentage ?? 0.0m,
                EmployeeContributionIsFixedAmount = staticEntry.EmployeeFixedAmount != null
            } :
            new PensionContributionLevels(),
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

        entry = new EmployeePayrunInputEntry(employee,
            employment,
            earnings,
            deductions,
            payrolledBenefits,
            pensionContributionLevels);
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


