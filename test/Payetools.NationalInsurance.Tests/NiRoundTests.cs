// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using FluentAssertions;
using Payetools.NationalInsurance.Extensions;

namespace Payetools.NationalInsurance.Tests;

// Testing the following expected behaviour:
//
// Rounds a decimal value based on the value of the third digit after the decimal point.  If the value of the third digit is 6 or above,
// the number is rounded up to the nearest whole pence; if the value of the third digit is 5 or below, the number is rounded down.
//
// Throws ArgumentOutOfRangeException if a negative number is supplied for value.

public class NiRoundTests
{
    [Fact]
    public void CheckNegativeNumberExceptionThrown()
    {
        decimal input = -1.2345m;

        Action action = () => input.NiRound();

        action.Should().Throw<ArgumentOutOfRangeException>()
            .WithMessage("decimal.NiRound() only supports rounding of positive numbers (Parameter 'value')");
    }

    [Fact]
    public void CheckZeroInput()
    {
        decimal input = 0.0m;

        input.NiRound().Should().Be(0.0m);
    }

    [Fact]
    public void CheckThirdDigitZeroInput()
    {
        decimal input = 123.050m;

        input.NiRound().Should().Be(123.05m);
    }

    [Fact]
    public void CheckThirdDigitFiveInput()
    {
        decimal input = 123.055m;

        input.NiRound().Should().Be(123.05m);
    }

    [Fact]
    public void CheckThirdDigitSixInput()
    {
        decimal input = 123.056m;

        input.NiRound().Should().Be(123.06m);
    }

    [Fact]
    public void CheckFourthDigitNineInput()
    {
        decimal input = 123.0559m;

        input.NiRound().Should().Be(123.05m);
    }
}
