﻿// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using FluentAssertions;
using Payetools.Common.Model;
using System.Drawing;
using System.Numerics;

namespace Payetools.Common.Tests;

public class UkPostcodeTests
{
    [Fact]
    public void TestValidPostcodes()
    {
        var validPostcodes = new string[]
        {
            "W1J 7NT",
            "DE128HJ",
            "SW1A 1AA",
            "JE23XP",
            "IM9 4EB",
            "GY79YH"
        };

        foreach(var validPostcode in validPostcodes)
        {
            Action act = () => new UkPostcode(validPostcode);

            act.Should().NotThrow();
        }
    }

    [Fact]
    public void TestInvalidPostcodes()
    {
        var invalidPostcodes = new string[]
        {
            "W1J-7NT",
            "DE9998HJ",
            "SW1A11",
            "JE23XP123V",
            "IEB",
            "GYXXYH"
        };

        foreach (var invalidPostcode in invalidPostcodes)
        {
            Action act = () => new UkPostcode(invalidPostcode);
            
            act.Should()
                .Throw<ArgumentException>()
                .WithMessage($"Argument '{invalidPostcode}' is not a valid UK postcode (Parameter 'value')");
        }
    }
}
