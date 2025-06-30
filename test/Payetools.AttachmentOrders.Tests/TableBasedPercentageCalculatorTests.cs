// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.AttachmentOrders.Calculators;
using Payetools.AttachmentOrders.Model;
using Payetools.Common.Model;
using Payetools.Testing.Data;
using Payetools.Testing.Data.AttachmentOrders;
using System.Threading.Tasks;

namespace Payetools.AttachmentOrders.Tests;

public class TableBasedPercentageCalculatorTests :IClassFixture<AttachmentOrdersCalculatorFactoryDataFixture>
{
    private readonly AttachmentOrdersCalculatorFactoryDataFixture _calculatorDataFixture;

    public TableBasedPercentageCalculatorTests(AttachmentOrdersCalculatorFactoryDataFixture calculatorDataFixture)
    {
        _calculatorDataFixture = calculatorDataFixture;
    }

    [Fact]
    public async Task CalculateAttachmentOrderDeduction_ShouldReturnCorrectDeduction()
    {
        var db = new TestDataProvider(true);

        var testData = db.GetTestData<IAttachmentOrderTestDataEntry>("AttachmentOrders")
            .Where(t => t.CalculationType == AttachmentOrderCalculationType.TableBasedPercentageOfEarnings)
            // .Where(t => t.TaxYearEnding == taxYearEnding)
            .ToList();

        if (!testData.Any())
            Assert.Fail("No income tax tests found");

        Console.WriteLine($"{testData.Count} tests found");

        var calculator = await GetCalculator(new PayDate(DateOnly.FromDateTime(DateTime.Now), PayFrequency.Monthly));

        // calculator.Calculate(testData, out var results);

        // Arrange
        // var calculator = new TableBasedPercentageCalculator();
        var earnings = 1000m; // Example earnings
        var rate = 0.15m; // Example rate (15%)
        // Act
        //var deduction = calculator.CalculateAttachmentOrderDeduction(earnings, rate);
        // Assert
        //deduction.ShouldBe(150m); // 15% of 1000 is 150
    }

    private async Task<IAttachmentOrdersCalculator> GetCalculator(PayDate payDate)
    {
        var provider = await _calculatorDataFixture.GetFactory();

        return provider.GetCalculator(payDate);
    }
}
