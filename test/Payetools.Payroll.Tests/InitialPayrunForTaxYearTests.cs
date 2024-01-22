// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using FluentAssertions;
using Payetools.Common.Model;
using Payetools.NationalInsurance.Model;
using Payetools.Payroll.Extensions;
using Payetools.Payroll.Model;
using Payetools.Payroll.PayRuns;
using Payetools.Pensions.Model;
using Payetools.Testing.Data.EndToEnd;
using System.Collections.Immutable;
using Xunit.Abstractions;

namespace Payetools.Payroll.Tests;

public class InitialPayRunForTaxYearTests : IClassFixture<PayrollProcessorFactoryFixture>
{
    private readonly ITestOutputHelper Output;
    private readonly PayDate _payDate = new PayDate(2022, 5, 5, PayFrequency.Monthly);
    private readonly PayrollProcessorFactoryFixture _payrollProcessorFactoryFixture;

    public InitialPayRunForTaxYearTests(PayrollProcessorFactoryFixture payrollProcessorFactoryFixture, ITestOutputHelper output)
    {
        _payrollProcessorFactoryFixture = payrollProcessorFactoryFixture;
        Output = output;
    }

    [Fact]
    public async Task Test1Async()
    {
        IEndToEndTestDataSet testData = EndToEndTestDataSource.GetAllData(Output);

        Output.WriteLine($"Fetched test data; {testData.StaticInputs.Count} static input items returned");

        MakeEmployeePayrollHistory(testData.PreviousYtdInputs.Where(pyi => pyi.TestReference == "Pay1").First(),
            testData.NiYtdHistory.Where(nyh => nyh.TestReference == "Pay1").ToList(),
            out var employeePayrollHistory);

        if (employeePayrollHistory == null)
            throw new InvalidOperationException("History can't be null");

        Output.WriteLine("Employee payroll history created okay");

        var staticInput = testData.StaticInputs.Where(si => si.TestReference == "Pay1").First();

        Output.WriteLine("Static inputs retrieved okay");

        IEmployer employer = new Employer();

        Output.WriteLine("Making payroll line items...");

        MakePayrollLineItems(testData.PeriodInputs.Where(pi => pi.TestReference == "Pay1"),
            testData.EarningsDefinitions,
            testData.DeductionDefinitions,
            out var earnings,
            out var deductions,
            out var payrolledBenefits);

        Output.WriteLine("Making payrun input...");

        MakeEmployeePayRunInput(employer,
            staticInput,
            testData.PensionSchemes.Where(ps => ps.SchemeName == staticInput.PensionScheme).FirstOrDefault(),
            employeePayrollHistory,
            earnings,
            deductions,
            payrolledBenefits,
            out var payrunEntry);

        var payRunInfo = testData.PayRunInfo.Where(pi => pi.TestReference == "Pay1").First();

        var payDate = new PayDate(payRunInfo.PayDay, payRunInfo.PayFrequency);
        var payPeriod = new DateRange(payRunInfo.PayPeriodStart, payRunInfo.PayPeriodEnd);

        var processor = await GetProcessorAsync(employer, payDate, payPeriod);

        List<IEmployeePayRunInputEntry> entries = new List<IEmployeePayRunInputEntry>();
        entries.Add(payrunEntry);

        processor.Process(entries, out var result);

        IEmployeePayrollHistoryYtd historyYtd = employeePayrollHistory.Add(result.EmployeePayRunEntries[0]);

        foreach (var employeeResult in result.EmployeePayRunEntries)
        {
            CheckResult("Pay1", employeeResult, testData.ExpectedOutputs.Where(eo => eo.TestReference == "Pay1").First());
        }

        Console.WriteLine(result.EmployeePayRunEntries[0].NiCalculationResult.ToString());
        Console.WriteLine();

        Console.WriteLine();
    }

    static void CheckResult(string testReference, in IEmployeePayRunResult result, in IExpectedOutputTestDataEntry expected)
    {
        var because = $"TestReference = '{testReference}'";

        result.TotalGrossPay.Should().Be(expected.GrossPay, because);
        result.TaxablePay.Should().Be(expected.TaxablePay, because);
        result.NicablePay.Should().Be(expected.NicablePay, because);
        result.TaxCalculationResult.FinalTaxDue.Should().Be(expected.TaxPaid, because);
        result.NiCalculationResult.EmployeeContribution.Should().Be(expected.EmployeeNiContribution, because);
        result.NiCalculationResult.EmployerContribution.Should().Be(expected.EmployerNiContribution, because);
        result.StudentLoanCalculationResult.StudentLoanDeduction.Should().Be(expected.StudentLoanRepayments, because);
        result.StudentLoanCalculationResult.PostGraduateLoanDeduction.Should().Be(expected.GraduateLoanRepayments, because);
        result.PensionContributionCalculationResult.CalculatedEmployeeContributionAmount.Should().Be(expected.EmployeePensionContribution, because);
        result.PensionContributionCalculationResult.CalculatedEmployerContributionAmount.Should().Be(expected.EmployerPensionContribution, because);

    }

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

    static void MakePayrollLineItems(
        in IEnumerable<IPeriodInputTestDataEntry> periodInputs,
        in List<IEarningsTestDataEntry> earningsDetails,
        in List<IDeductionsTestDataEntry> deductionDetails,
        out List<IEarningsEntry> earnings,
        out List<IDeductionEntry> deductions,
        out List<IPayrolledBenefitForPeriod> benefits)
    {
        earnings = new List<IEarningsEntry>();
        deductions = new List<IDeductionEntry>();
        benefits = new List<IPayrolledBenefitForPeriod>();

        foreach (var pi in periodInputs)
        {
            if (pi.FixedAmount == null && (pi.Rate == null || pi.Qty == null))
                throw new ArgumentException($"Invalid earnings/deduction/benefits entry '{pi.EntryType}' with description '{pi.Description}'; insufficient data supplied", nameof(periodInputs));

            switch (pi.EntryType)
            {
                case "Earnings":
                    var thisEarnings = new EarningsEntry()
                    {
                        EarningsDetails = earningsDetails.Where(ed => ed.ShortName == pi.ShortName).Select(ed =>
                            new GenericEarnings()
                            {
                                Id = Guid.NewGuid(),
                                ShortName = ed.ShortName,
                                IsNetToGross = false,
                                IsPensionable = ed.IsPensionable,
                                IsSubjectToNi = ed.IsSubjectToNi,
                                IsSubjectToTax = ed.IsSubjectToTax,
                                IsTreatedAsOvertime = ed.IsTreatedAsOvertime
                            }).First(),
                        FixedAmount = pi.FixedAmount,
                        QuantityInUnits = pi.Qty,
                        ValuePerUnit = pi.Rate
                    };
                    earnings.Add(thisEarnings);
                    break;

                case "Deduction":
                    var thisDeduction = new DeductionEntry()
                    {
                        DeductionClassification = deductionDetails.Where(d => d.ShortName == pi.ShortName).Select(d =>
                            new GenericDeduction()
                            {
                                ShortName = d.ShortName,
                                ReducesTaxablePay = d.ReducesTaxablePay,
                                ReducesNicablePay = d.ReducesNicablePay,
                                ReducesPensionablePay = d.ReducesPensionablePay,
                                IsUnderSalaryExchangeArrangement = d.IsUnderSalaryExchangeArrangement
                            }).First(),
                        FixedAmount = pi.FixedAmount,
                        QuantityInUnits = pi.Qty,
                        ValuePerUnit = pi.Rate
                    };
                    deductions.Add(thisDeduction);
                    break;

                case "PayrolledBenefit":
                    var thisBenefit = new PayrolledBenefitForPeriod(pi.FixedAmount ?? pi.Qty * pi.Rate ?? 0.0m);
                    benefits.Add(thisBenefit);
                    break;

                default:
                    throw new ArgumentException($"Unrecognised period input type '{pi.EntryType}'; must be one of 'Earnings', 'Deduction' or 'PayrolledBenefit'", nameof(periodInputs));
            }
        }
    }

    static void MakeEmployeePayRunInput(
        in IEmployer employer,
        in IStaticInputTestDataEntry staticEntry,
        in IPensionSchemesTestDataEntry? pensionScheme,
        in IEmployeePayrollHistoryYtd history,
        in List<IEarningsEntry> earnings,
        in List<IDeductionEntry> deductions,
        in List<IPayrolledBenefitForPeriod> benefits,
        out EmployeePayRunInputEntry entry)
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
            StudentLoanInfo = (staticEntry.StudentLoanPlan != null || staticEntry.GraduateLoan) ?
                new StudentLoanInfo() { StudentLoanType = staticEntry.StudentLoanPlan, HasPostGradLoan = staticEntry.GraduateLoan } :
                null,
            PensionScheme = staticEntry.PensionScheme != null ? new PensionScheme()
            {
                TaxTreatment = pensionScheme?.TaxTreatment ?? throw new ArgumentNullException("Pension scheme name doesn't match a value pension", nameof(pensionScheme)),
                EarningsBasis = pensionScheme?.EarningsBasis ?? throw new ArgumentNullException("Pension scheme name doesn't match a value pension", nameof(pensionScheme))
            } : null,
            DefaultPensionContributionLevels = new PensionContributionLevels()
        };

        var pensionContributionLevels = staticEntry.PensionScheme != null ? new PensionContributionLevels()
        {
            EmployerContributionPercentage = staticEntry.EmployerPercentage ?? 0.0m,
            EmployeeContribution = staticEntry.EmployeeFixedAmount ?? staticEntry.EmployeePercentage ?? 0.0m,
            EmployeeContributionIsFixedAmount = staticEntry.EmployeeFixedAmount != null
        } : new PensionContributionLevels();


        entry = new EmployeePayRunInputEntry(new EmployeeAccessor(employee),
            employment,
            earnings.ToImmutableList(),
            deductions.ToImmutableList(),
            benefits.ToImmutableList(),
            pensionContributionLevels);
    }

    private async Task<IPayRunProcessor> GetProcessorAsync(IEmployer employer, PayDate payDate, DateRange payPeriod)
    {
        var factory = await _payrollProcessorFactoryFixture.GetFactory();

        return factory.GetProcessor(employer, payDate, payPeriod);
    }

    private class EmployeeAccessor : IEmployeeAccessor
    {
        private readonly IEmployee _employee;

        public EmployeeAccessor(IEmployee employee)
        {
            _employee = employee;
        }

        public IEmployee GetEmployee() => _employee;
    }
}