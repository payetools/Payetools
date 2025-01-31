// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using FluentAssertions;
using Payetools.Common.Extensions;
using Payetools.Common.Model;

namespace Payetools.NationalMinimumWage.Tests;

public class CompliantNmwTests_2022_2023 : IClassFixture<NmwEvaluatorFactoryDataFixture>
{
    private readonly PayDate _payDate = new PayDate(2022, 5, 5, PayFrequency.Monthly);
    private readonly NmwEvaluatorFactoryDataFixture _factoryProviderFixture;
    private readonly decimal[] _expectedHourlyNmwRates = new decimal[]
    {
        4.81m,
        4.81m,
        6.83m,
        9.18m,
        9.50m
    };
    private const int ApprenticeLevelIndex = 0;
    private const int Under18LevelIndex = 1;
    private const int Age18To20Level = 2;
    private const int Age21To22Level = 3;
    private const int Age23AndAboveLevel = 4;

    public CompliantNmwTests_2022_2023(NmwEvaluatorFactoryDataFixture factoryProviderFixture)
    {
        _factoryProviderFixture = factoryProviderFixture;
    }

    [Fact]
    public async Task Test23OrOverAsync()
    {
        var payPeriod = new DateRange(new DateOnly(2022, 4, 1), new DateOnly(2022, 4, 30));
        var dateOfBirth = new DateOnly(1999, 4, 1);
        var hoursWorked = 24.0m;
        var hourlyRate = 9.50m;
        var grossPay = hoursWorked * hourlyRate;

        await RunValidNmwTestAsync(payPeriod, dateOfBirth, hoursWorked, grossPay,
            dateOfBirth.AgeAt(payPeriod.Start), _expectedHourlyNmwRates[Age23AndAboveLevel]);

        dateOfBirth = new DateOnly(1999, 3, 31);

        await RunValidNmwTestAsync(payPeriod, dateOfBirth, hoursWorked, grossPay,
            dateOfBirth.AgeAt(payPeriod.Start), _expectedHourlyNmwRates[Age23AndAboveLevel]);

        hoursWorked = 23.9m;
        dateOfBirth = new DateOnly(1950, 3, 31);

        await RunValidNmwTestAsync(payPeriod, dateOfBirth, hoursWorked, grossPay,
            dateOfBirth.AgeAt(payPeriod.Start), _expectedHourlyNmwRates[Age23AndAboveLevel]);
    }

    [Fact]
    public async Task Test21To22Async()
    {
        var payPeriod = new DateRange(new DateOnly(2022, 8, 10), new DateOnly(2022, 9, 9));
        var dateOfBirth = new DateOnly(2001, 8, 10);
        var hoursWorked = 40.0m;
        var hourlyRate = 9.18m;
        var grossPay = hoursWorked * hourlyRate;

        await RunValidNmwTestAsync(payPeriod, dateOfBirth, hoursWorked, grossPay,
            dateOfBirth.AgeAt(payPeriod.Start), _expectedHourlyNmwRates[Age21To22Level]);

        grossPay += 0.01m;
        dateOfBirth = new DateOnly(1999, 8, 11);

        await RunValidNmwTestAsync(payPeriod, dateOfBirth, hoursWorked, grossPay,
            dateOfBirth.AgeAt(payPeriod.Start), _expectedHourlyNmwRates[Age21To22Level]);
    }

    [Fact]
    public async Task Test18To20Async()
    {
        var payPeriod = new DateRange(new DateOnly(2023, 1, 1), new DateOnly(2023, 1, 31));
        var dateOfBirth = new DateOnly(2005, 1, 1);
        var hoursWorked = 35.0m;
        var hourlyRate = 6.83m;
        var grossPay = hoursWorked * hourlyRate;

        await RunValidNmwTestAsync(payPeriod, dateOfBirth, hoursWorked, grossPay,
            dateOfBirth.AgeAt(payPeriod.Start), _expectedHourlyNmwRates[Age18To20Level]);

        grossPay += 0.01m;
        dateOfBirth = new DateOnly(2003, 1, 2);

        await RunValidNmwTestAsync(payPeriod, dateOfBirth, hoursWorked, grossPay,
            dateOfBirth.AgeAt(payPeriod.Start), _expectedHourlyNmwRates[Age18To20Level]);
    }

    [Fact]
    public async Task TestUnder18NonApprenticeAsync()
    {
        var payPeriod = new DateRange(new DateOnly(2023, 3, 5), new DateOnly(2023, 4, 5));
        var dateOfBirth = new DateOnly(2006, 3, 6);
        var hoursWorked = 17.5m;
        var hourlyRate = 4.81m;
        var grossPay = hoursWorked * hourlyRate;

        await RunValidNmwTestAsync(payPeriod, dateOfBirth, hoursWorked, grossPay,
            dateOfBirth.AgeAt(payPeriod.Start), _expectedHourlyNmwRates[Under18LevelIndex]);

        grossPay += 0.01m;
        dateOfBirth = new DateOnly(2005, 3, 6);

        await RunValidNmwTestAsync(payPeriod, dateOfBirth, hoursWorked, grossPay,
            dateOfBirth.AgeAt(payPeriod.Start), _expectedHourlyNmwRates[Under18LevelIndex]);
    }

    [Fact]
    public async Task TestApprenticeAsync()
    {
        var payPeriod = new DateRange(new DateOnly(2023, 3, 5), new DateOnly(2023, 4, 5));
        var dateOfBirth = new DateOnly(2004, 3, 6);
        var hoursWorked = 17.5m;
        var hourlyRate = 4.81m;
        var grossPay = hoursWorked * hourlyRate;

        await RunValidNmwTestAsync(payPeriod, dateOfBirth, hoursWorked, grossPay,
            dateOfBirth.AgeAt(payPeriod.Start), _expectedHourlyNmwRates[ApprenticeLevelIndex], true, 1.0m);

        grossPay += 0.01m;
        dateOfBirth = new DateOnly(2004, 3, 5);

        await RunValidNmwTestAsync(payPeriod, dateOfBirth, hoursWorked, grossPay,
            dateOfBirth.AgeAt(payPeriod.Start), _expectedHourlyNmwRates[ApprenticeLevelIndex], true, 0.99m);
    }

    private async Task RunValidNmwTestAsync(DateRange payPeriod, DateOnly dateOfBirth, decimal hoursWorked,
        decimal grossPay, int expectedAge, decimal expectedRate, bool isApprentice = false, decimal? yearsAsApprentice = null)
    {
        var evaluator = await GetEvaluator();
        var hourlyRate = decimal.Round(grossPay / hoursWorked, 4, MidpointRounding.AwayFromZero);

        var result = evaluator.Evaluate(payPeriod, dateOfBirth, grossPay, hoursWorked, isApprentice, yearsAsApprentice);

        result.AgeAtStartOfPayPeriod.Should().Be(expectedAge);
        result.IsCompliant.Should().BeTrue();
        result.NmwLevelApplied.Should().Be(expectedRate);

        if (isApprentice && (expectedAge < 19 || yearsAsApprentice < 1.0m))
        {
            var apprenticeText = expectedAge >= 19 ? "Treated as apprentice 19 or over but in the first year of their apprenticeship" : "Treated as apprentice under 19";
            result.Commentary.Should().Be($"Age at start of pay period = {expectedAge}. {apprenticeText}. Pay is compliant as gross pay per hour of {hourlyRate:F4} is greater than or equal to minimum NMW/NLW rate {expectedRate}");
        }
        else
            result.Commentary.Should().Be($"Age at start of pay period = {expectedAge}. Pay is compliant as gross pay per hour of {hourlyRate:F4} is greater than or equal to minimum NMW/NLW rate {expectedRate}");
    }

    private async Task<INmwEvaluator> GetEvaluator()
    {
        var provider = await _factoryProviderFixture.GetFactory();

        return provider.GetEvaluator(_payDate);
    }
}