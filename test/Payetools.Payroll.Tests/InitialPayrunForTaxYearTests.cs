// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using FluentAssertions;
using Payetools.Common.Model;
using Payetools.NationalInsurance.Model;
using Payetools.NationalInsurance.ReferenceData;
using Payetools.Payroll.Model;
using Payetools.Payroll.PayRuns;
using Payetools.Pensions.Model;
using Payetools.Testing.Data.EndToEnd;
using System.Collections;
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

        IEmployer employer = new Employer("Test Employer Ltd", "Test Employer");

        Output.WriteLine("Making payroll line items...");

        MakePayrollLineItems(testData.PeriodInputs.Where(pi => pi.TestReference == "Pay1"),
            testData.EarningsDefinitions,
            testData.DeductionDefinitions,
            out var earnings,
            out var deductions,
            out var payrolledBenefits);

        Output.WriteLine("Making payrun input...");

        MakeEmployeePayRunInput(
            staticInput,
            testData.PensionSchemes.Where(ps => ps.SchemeName == staticInput.PensionScheme).FirstOrDefault(),
            employeePayrollHistory,
            earnings,
            deductions,
            payrolledBenefits,
            out var payrunEntry);

        var employment = payrunEntry.Employment;

        var payRunInfo = testData.PayRunInfo.Where(pi => pi.TestReference == "Pay1").First();

        var payDate = new PayDate(payRunInfo.PayDay, payRunInfo.PayFrequency);
        var payPeriod = new DateRange(payRunInfo.PayPeriodStart, payRunInfo.PayPeriodEnd);

        var processor = await GetProcessorAsync(payDate, payPeriod);

        var entries = new List<IEmployeePayRunInputEntry> { payrunEntry };

        processor.Process(employer, entries, out var result);

        employment.UpdatePayrollHistory(payrunEntry, result.EmployeePayRunResults[0]);

        //IEmployeePayrollHistoryYtd historyYtd = employeePayrollHistory.Add(payrunEntry, result.EmployeePayRunResults[0]);

        foreach (var employeeResult in result.EmployeePayRunResults)
        {
            CheckResult("Pay1", employeeResult, testData.ExpectedOutputs.Where(eo => eo.TestReference == "Pay1").First());
        }

        Console.WriteLine(result.EmployeePayRunResults[0].NiCalculationResult.ToString());
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
        result.StudentLoanCalculationResult?.Should().NotBeNull();
        result.StudentLoanCalculationResult?.StudentLoanDeduction.Should().Be(expected.StudentLoanRepayments, because);
        result.StudentLoanCalculationResult?.PostgraduateLoanDeduction.Should().Be(expected.GraduateLoanRepayments, because);
        result.PensionContributionCalculationResult.Should().NotBeNull();
        result.PensionContributionCalculationResult?.CalculatedEmployeeContributionAmount.Should().Be(expected.EmployeePensionContribution, because);
        result.PensionContributionCalculationResult?.CalculatedEmployerContributionAmount.Should().Be(expected.EmployerPensionContribution, because);

    }

    static void MakeEmployeePayrollHistory(in IPreviousYtdTestDataEntry previousYtd,
        in List<INiYtdHistoryTestDataEntry> niYtdHistory, out IEmployeePayrollHistoryYtd history)
    {
        var niHistoryEntries = niYtdHistory.Select(nih => new EmployeeNiHistoryEntry(
            new NiCalculationResult(
                nih.NiCategoryPertaining,
                nih.GrossNicableEarnings,
                new TestNiCategoryRatesEntry(),
                new TestNiThresholdSet(),
                new NiEarningsBreakdown()
                {
                    EarningsAtLEL = nih.EarningsUpToAndIncludingLEL,
                    EarningsAboveLELUpToAndIncludingST = nih.EarningsAboveLELUpToAndIncludingST,
                    EarningsAboveSTUpToAndIncludingPT = nih.EarningsAboveSTUpToAndIncludingPT,
                    EarningsAbovePTUpToAndIncludingFUST = nih.EarningsAbovePTUpToAndIncludingFUST,
                    EarningsAboveFUSTUpToAndIncludingUEL = nih.EarningsAboveFUSTUpToAndIncludingUEL,
                    EarningsAboveSTUpToAndIncludingUEL = nih.EarningsAboveSTUpToAndIncludingUEL,
                    EarningsAboveUEL = nih.EarningsAboveUEL
                },
                nih.EmployeeContribution,
                nih.EmployerContribution,
                nih.TotalContribution))
            as IEmployeeNiHistoryEntry)
                .ToImmutableArray();

        history = new EmployeePayrollHistoryYtd()
        {
            EmployeeNiHistoryEntries = new NiYtdHistory(niHistoryEntries, null),
            GrossPayYtd = previousYtd.GrossPayYtd,
            NicablePayYtd = previousYtd.NicablePayYtd,
            TaxablePayYtd = previousYtd.TaxablePayYtd,
            TaxPaidYtd = previousYtd.TaxPaidYtd,
            TaxUnpaidDueToRegulatoryLimit = previousYtd.TaxUnpaidDueToRegulatoryLimit,
            PayrolledBenefitsYtd = previousYtd.PayrolledBenefitsYtd,
            StudentLoanRepaymentsYtd = previousYtd.StudentLoanRepaymentsYtd,
            PostgraduateLoanRepaymentsYtd = previousYtd.GraduateLoanRepaymentsYtd,
            StatutorySharedParentalPayYtd = previousYtd.SharedParentalPayYtd,
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

        var employment = new Employment(history)
        {
            TaxCode = staticEntry.TaxCode,
            NiCategory = staticEntry.NiCategory,
            StudentLoanInfo = (staticEntry.StudentLoanPlan != null || staticEntry.GraduateLoan) ?
                new StudentLoanInfo() { StudentLoanType = staticEntry.StudentLoanPlan, HasPostgraduateLoan = staticEntry.GraduateLoan } :
                null,
            PensionScheme = staticEntry.PensionScheme != null ? new PensionScheme()
            {
                TaxTreatment = pensionScheme?.TaxTreatment ?? throw new ArgumentNullException(nameof(pensionScheme), "Pension scheme name doesn't match a value pension"),
                EarningsBasis = pensionScheme?.EarningsBasis ?? throw new ArgumentNullException(nameof(pensionScheme), "Pension scheme name doesn't match a value pension")
            } : null,
            DefaultPensionContributionLevels = new PensionContributionLevels()
        };

        var pensionContributionLevels = staticEntry.PensionScheme != null ? new PensionContributionLevels()
        {
            EmployerContribution = staticEntry.EmployerPercentage ?? 0.0m,
            EmployerContributionIsFixedAmount = false,
            EmployeeContribution = staticEntry.EmployeeFixedAmount ?? staticEntry.EmployeePercentage ?? 0.0m,
            EmployeeContributionIsFixedAmount = staticEntry.EmployeeFixedAmount != null
        } : new PensionContributionLevels();


        entry = new EmployeePayRunInputEntry(
            employment,
            earnings.ToImmutableArray(),
            deductions.ToImmutableArray(),
            benefits.ToImmutableArray(),
            pensionContributionLevels);
    }

    private async Task<IPayRunProcessor> GetProcessorAsync(PayDate payDate, DateRange payPeriod)
    {
        var factory = await _payrollProcessorFactoryFixture.GetFactory();

        return factory.GetProcessor(payDate, payPeriod);
    }

    private class TestNiCategoryRatesEntry : INiCategoryRatesEntry
    {
        public NiCategory Category => throw new NotImplementedException();
        public decimal EmployeeRateToPT => throw new NotImplementedException();
        public decimal EmployeeRatePTToUEL => throw new NotImplementedException();
        public decimal EmployeeRateAboveUEL => throw new NotImplementedException();
        public decimal EmployerRateLELtoST => throw new NotImplementedException();
        public decimal EmployerRateSTtoFUST => throw new NotImplementedException();
        public decimal EmployerRateFUSTtoUEL => throw new NotImplementedException();
        public decimal EmployerRateAboveUEL => throw new NotImplementedException();
    }

    private class TestNiThresholdSet : INiThresholdSet
    {
        public INiThresholdEntry this[int index] => throw new NotImplementedException();
        public int Count => throw new NotImplementedException();
        public IEnumerator<INiThresholdEntry> GetEnumerator() => throw new NotImplementedException();
        public decimal GetThreshold(NiThresholdType thresholdType) => throw new NotImplementedException();
        IEnumerator IEnumerable.GetEnumerator() => throw new NotImplementedException();
    }
}