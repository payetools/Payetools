// Copyright (c) 2023 Paytools Foundation.
//
// Paytools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Paytools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

using FluentAssertions;
using Paytools.Common.Extensions;
using Paytools.Common.Model;

namespace Paytools.NationalMinimumWage.Tests;

public class CompliantNmwTests_2023_2024 : IClassFixture<NmwEvaluatorFactoryDataFixture>
{
    private readonly PayDate _payDate = new PayDate(2023, 5, 5, PayFrequency.Monthly);
    private readonly NmwEvaluatorFactoryDataFixture _factoryProviderFixture;
    private readonly decimal[] _expectedHourlyNmwRates = new decimal[]
    {
        5.28m,
        5.28m,
        7.49m,
        10.18m,
        10.42m
    };
    private const int ApprenticeLevelIndex = 0;
    private const int Under18LevelIndex = 1;
    private const int Age18To20Level = 2;
    private const int Age21To22Level = 3;
    private const int Age23AndAboveLevel = 4;

    public CompliantNmwTests_2023_2024(NmwEvaluatorFactoryDataFixture factoryProviderFixture)
    {
        _factoryProviderFixture = factoryProviderFixture;
    }

    [Fact]
    public async Task Test23OrOverAsync()
    {
        var payPeriod = new PayReferencePeriod(new DateOnly(2023, 4, 1), new DateOnly(2023, 4, 30));
        var dateOfBirth = new DateOnly(2000, 4, 1);
        var hoursWorked = 24.0m;
        var hourlyRate = 10.42m;
        var grossPay = hoursWorked * hourlyRate;

        await RunValidNmwTestAsync(payPeriod, dateOfBirth, hoursWorked, grossPay,
            dateOfBirth.AgeAt(payPeriod.StartOfPayPeriod), _expectedHourlyNmwRates[Age23AndAboveLevel]);

        dateOfBirth = new DateOnly(2000, 3, 31);

        await RunValidNmwTestAsync(payPeriod, dateOfBirth, hoursWorked, grossPay,
            dateOfBirth.AgeAt(payPeriod.StartOfPayPeriod), _expectedHourlyNmwRates[Age23AndAboveLevel]);

        hoursWorked = 23.9m;
        dateOfBirth = new DateOnly(1951, 3, 31);

        await RunValidNmwTestAsync(payPeriod, dateOfBirth, hoursWorked, grossPay,
            dateOfBirth.AgeAt(payPeriod.StartOfPayPeriod), _expectedHourlyNmwRates[Age23AndAboveLevel]);
    }

    [Fact]
    public async Task Test21To22Async()
    {
        var payPeriod = new PayReferencePeriod(new DateOnly(2023, 8, 10), new DateOnly(2023, 9, 9));
        var dateOfBirth = new DateOnly(2002, 8, 10);
        var hoursWorked = 40.0m;
        var hourlyRate = 10.18m;
        var grossPay = hoursWorked * hourlyRate;

        await RunValidNmwTestAsync(payPeriod, dateOfBirth, hoursWorked, grossPay,
            dateOfBirth.AgeAt(payPeriod.StartOfPayPeriod), _expectedHourlyNmwRates[Age21To22Level]);

        grossPay += 0.01m;
        dateOfBirth = new DateOnly(2000, 8, 11);

        await RunValidNmwTestAsync(payPeriod, dateOfBirth, hoursWorked, grossPay,
            dateOfBirth.AgeAt(payPeriod.StartOfPayPeriod), _expectedHourlyNmwRates[Age21To22Level]);
    }

    [Fact]
    public async Task Test18To20Async()
    {
        var payPeriod = new PayReferencePeriod(new DateOnly(2024, 1, 1), new DateOnly(2024, 1, 31));
        var dateOfBirth = new DateOnly(2006, 1, 1);
        var hoursWorked = 35.0m;
        var hourlyRate = 7.49m;
        var grossPay = hoursWorked * hourlyRate;

        await RunValidNmwTestAsync(payPeriod, dateOfBirth, hoursWorked, grossPay,
            dateOfBirth.AgeAt(payPeriod.StartOfPayPeriod), _expectedHourlyNmwRates[Age18To20Level]);

        grossPay += 0.01m;
        dateOfBirth = new DateOnly(2003, 1, 2);

        await RunValidNmwTestAsync(payPeriod, dateOfBirth, hoursWorked, grossPay,
            dateOfBirth.AgeAt(payPeriod.StartOfPayPeriod), _expectedHourlyNmwRates[Age18To20Level]);
    }

    [Fact]
    public async Task TestUnder18NonApprenticeAsync()
    {
        var payPeriod = new PayReferencePeriod(new DateOnly(2023, 3, 5), new DateOnly(2024, 4, 5));
        var dateOfBirth = new DateOnly(2007, 3, 6);
        var hoursWorked = 17.5m;
        var hourlyRate = 5.28m;
        var grossPay = hoursWorked * hourlyRate;

        await RunValidNmwTestAsync(payPeriod, dateOfBirth, hoursWorked, grossPay,
            dateOfBirth.AgeAt(payPeriod.StartOfPayPeriod), _expectedHourlyNmwRates[Under18LevelIndex]);

        grossPay += 0.01m;
        dateOfBirth = new DateOnly(2005, 3, 6);

        await RunValidNmwTestAsync(payPeriod, dateOfBirth, hoursWorked, grossPay,
            dateOfBirth.AgeAt(payPeriod.StartOfPayPeriod), _expectedHourlyNmwRates[Under18LevelIndex]);
    }

    [Fact]
    public async Task TestApprenticeAsync()
    {
        var payPeriod = new PayReferencePeriod(new DateOnly(2024, 3, 5), new DateOnly(2024, 4, 5));
        var dateOfBirth = new DateOnly(2005, 3, 6);
        var hoursWorked = 17.5m;
        var hourlyRate = 5.28m;
        var grossPay = hoursWorked * hourlyRate;

        await RunValidNmwTestAsync(payPeriod, dateOfBirth, hoursWorked, grossPay,
            dateOfBirth.AgeAt(payPeriod.StartOfPayPeriod), _expectedHourlyNmwRates[ApprenticeLevelIndex], true, 1.0m);

        grossPay += 0.01m;
        dateOfBirth = new DateOnly(2004, 3, 5);

        await RunValidNmwTestAsync(payPeriod, dateOfBirth, hoursWorked, grossPay,
            dateOfBirth.AgeAt(payPeriod.StartOfPayPeriod), _expectedHourlyNmwRates[ApprenticeLevelIndex], true, 0.99m);
    }

    private async Task RunValidNmwTestAsync(PayReferencePeriod payPeriod, DateOnly dateOfBirth, decimal hoursWorked,
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