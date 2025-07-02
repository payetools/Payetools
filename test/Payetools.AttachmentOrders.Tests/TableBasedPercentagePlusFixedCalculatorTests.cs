// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.AttachmentOrders.Calculators;
using Payetools.AttachmentOrders.Model;
using Payetools.Common.Model;
using Payetools.Payroll.Model;
using Payetools.Testing.Data;
using Payetools.Testing.Data.AttachmentOrders;
using Shouldly;
using System.Threading.Tasks;

namespace Payetools.AttachmentOrders.Tests;

public class TableBasedPercentagePlusFixedCalculatorTests : IClassFixture<AttachmentOrdersCalculatorFactoryDataFixture>
{
    private readonly AttachmentOrdersCalculatorFactoryDataFixture _calculatorDataFixture;

    public TableBasedPercentagePlusFixedCalculatorTests(AttachmentOrdersCalculatorFactoryDataFixture calculatorDataFixture)
    {
        _calculatorDataFixture = calculatorDataFixture;
    }

    [Fact]
    public async Task CalculateAttachmentOrderDeduction_ShouldReturnCorrectDeduction()
    {
        var db = new TestDataProvider(true);

        var testData = db.GetTestData<IAttachmentOrderTestDataEntry>("AttachmentOrders")
            .Where(t => t.CalculationType == AttachmentOrderCalculationType.TableBasedPercentageOfEarningsPlusFixedAmount &&
                CountriesForTaxPurposesConverter.ToEnum(t.Jurisdiction) == CountriesForTaxPurposes.Scotland)
            .ToList();

        if (!testData.Any())
            Assert.Fail("No income found");

        Console.WriteLine($"{testData.Count} tests found");

        var testIndex = 0;

        foreach (var entry in testData)
        {
            var today = DateOnly.FromDateTime(DateTime.Now);
            var payDay = new DateOnly(today.Year, today.AddMonths(1).Month, 1).AddMonths(-1);
            var startOfPayPeriod = entry.PayFrequency == PayFrequency.Weekly ? payDay.AddDays(-7) : payDay.AddMonths(-1);
            var payPeriod = new DateRange(startOfPayPeriod, payDay);
            var payDate = new PayDate(payDay, entry.PayFrequency);

            var calculator = await GetCalculator(payDate);

            var attachmentOrder = new AttachmentOrder
            {
                CalculationType = AttachmentOrderCalculationType.TableBasedPercentageOfEarnings,
                IssueDate = entry.IssueDate,
                EffectiveDate = startOfPayPeriod,
                RateType = entry.Rate,
                EmployeePayFrequency = (AttachmentOrderPayFrequency)entry.PayFrequency,
                ApplicableJurisdiction = CountriesForTaxPurposesConverter.ToEnum(entry.Jurisdiction)
            };

            var earnings = new EarningsEntry()
            {
                EarningsDetails = new GenericEarnings()
                {
                    Id = Guid.NewGuid(),
                    ShortName = "Earnings",
                    IsNetToGross = false,
                    IsPensionable = true,
                    IsSubjectToNi = true,
                    IsSubjectToTax = true,
                    IsTreatedAsOvertime = false,
                    PaymentType = EarningsType.GeneralEarnings
                },
                FixedAmount = entry.AttachableEarnings
            };

            calculator.Calculate(
                [attachmentOrder], 
                [earnings], 
                [], 
                payPeriod,
                0m,
                0m,
                0m,
                0m,
                out var result);

            result.ShouldNotBeNull();
            result.Entries.Count.ShouldBe(1);
            result.Entries.First().Deduction.ShouldBe(entry.ExpectedDeduction, $"Test # {testIndex}: earnings = {entry.AttachableEarnings}, Rate type = {entry.Rate}");

            testIndex++;
        }
    }
    private async Task<IAttachmentOrdersCalculator> GetCalculator(PayDate payDate)
    {
        var provider = await _calculatorDataFixture.GetFactory();

        return provider.GetCalculator(payDate);
    }
}
