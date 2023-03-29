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
using Paytools.Employment.Model;
using Paytools.NationalInsurance.Model;
using Paytools.Payroll.Extensions;
using Paytools.Payroll.Model;
using Paytools.Payroll.Payruns;
using Paytools.Pensions.Model;
using Paytools.Testing.Data.EndToEnd;
using System.Collections.Immutable;
using Xunit.Abstractions;

namespace Paytools.Payroll.Tests;

public class InitialPayrunForTaxYearTests : IClassFixture<PayrollProcessorFactoryFixture>
{
    private readonly ITestOutputHelper Output;
    private readonly PayrollProcessorFactoryFixture _payrollProcessorFactoryFixture;

    public InitialPayrunForTaxYearTests(PayrollProcessorFactoryFixture payrollProcessorFactoryFixture, ITestOutputHelper output)
    {
        _payrollProcessorFactoryFixture = payrollProcessorFactoryFixture;
        Output = output;
    }

    [Fact]
    public async Task Test1Async()
    {
        IEndToEndTestDataSet testData = EndToEndTestDataSource.GetAllData();

        MakeEmployeePayrollHistory(testData.PreviousYtdInputs.Where(pyi => pyi.TestReference == "Pay1").First(),
            testData.NiYtdHistory.Where(nyh => nyh.TestReference == "Pay1").ToList(),
            out var employeePayrollHistory);

        if (employeePayrollHistory == null)
            throw new InvalidOperationException("History can't be null");

        var staticInput = testData.StaticInputs.Where(si => si.TestReference == "Pay1").First();

        IEmployer employer = new Employer();

        MakePayrollLineItems(testData.PeriodInputs.Where(pi => pi.TestReference == "Pay1"),
            testData.EarningsDefinitions,
            testData.DeductionDefinitions,
            out var earnings,
            out var deductions,
            out var payrolledBenefits);

        MakeEmployeePayrunInput(employer,
            staticInput,
            testData.PensionSchemes.Where(ps => ps.SchemeName == staticInput.PensionScheme).FirstOrDefault(),
            employeePayrollHistory,
            earnings,
            deductions,
            payrolledBenefits,
            out var payrunEntry);

        var payrunInfo = testData.PayrunInfo.Where(pi => pi.TestReference == "Pay1").First();

        PayDate payDate = new PayDate(payrunInfo.PayDay, payrunInfo.PayFrequency);
        PayReferencePeriod payPeriod = new PayReferencePeriod(payrunInfo.PayPeriodStart, payrunInfo.PayPeriodEnd);

        var processor = await GetProcessor(employer, payDate, payPeriod);

        List<IEmployeePayrunInputEntry> entries = new List<IEmployeePayrunInputEntry>();
        entries.Add(payrunEntry);

        processor.Process(entries, out var result);

        IEmployeePayrollHistoryYtd historyYtd = employeePayrollHistory.Add(result.EmployeePayrunEntries[0]);

        foreach (var employeeResult in result.EmployeePayrunEntries)
        {
            CheckResult("Pay1", employeeResult, testData.ExpectedOutputs.Where(eo => eo.TestReference == "Pay1").First());
        }

        Console.WriteLine(result.EmployeePayrunEntries[0].NiCalculationResult.ToString());
        Console.WriteLine();

        Console.WriteLine();
    }

    void CheckResult(string testReference, in IEmployeePayrunResult result, in IExpectedOutputTestDataEntry expected)
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

        Output.WriteLine($"Completed test reference '{testReference}'");
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

    static void MakeEmployeePayrunInput(
        in IEmployer employer,
        in IStaticInputTestDataEntry staticEntry,
        in IPensionSchemesTestDataEntry? pensionScheme,
        in IEmployeePayrollHistoryYtd history,
        in List<IEarningsEntry> earnings,
        in List<IDeductionEntry> deductions,
        in List<IPayrolledBenefitForPeriod> benefits,
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
            DefaultPensionContributionLevels = new PensionContributionLevels()
        };

        var pensionContributionLevels = staticEntry.PensionScheme != null ? new PensionContributionLevels()
        {
            EmployerContributionPercentage = staticEntry.EmployerPercentage ?? 0.0m,
            EmployeeContribution = staticEntry.EmployeeFixedAmount ?? staticEntry.EmployeePercentage ?? 0.0m,
            EmployeeContributionIsFixedAmount = staticEntry.EmployeeFixedAmount != null
        } : new PensionContributionLevels();


        entry = new EmployeePayrunInputEntry(employee,
            employment,
            earnings.ToImmutableList(),
            deductions.ToImmutableList(),
            benefits.ToImmutableList(),
            pensionContributionLevels);
    }

    private async Task<IPayrunProcessor> GetProcessor(IEmployer employer, PayDate payDate, PayReferencePeriod payPeriod)
    {
        var factory = await _payrollProcessorFactoryFixture.GetFactory();

        return await factory.GetProcessorAsync(employer, payDate, payPeriod);
    }
}


