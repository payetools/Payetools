// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using FluentAssertions;
using Payetools.Common.Model;

namespace Payetools.Common.Tests;

public class NiNumberTests
{
    [Fact]
    public void TestValidNiNumbers()
    {
        var niNumber = new NiNumber("AB123456A");
        niNumber.ToString().Should().Be("AB123456A");

        niNumber = new NiNumber("HJ 12 34 56 B");
        niNumber.ToString().Should().Be("HJ123456B");

        niNumber = new NiNumber("KL 12 34 56 C");
        niNumber.ToString().Should().Be("KL123456C");

        niNumber = new NiNumber("  PR12 3456 D  ");
        niNumber.ToString().Should().Be("PR123456D");

        niNumber = new NiNumber("ab123456a");
        niNumber.ToString().Should().Be("AB123456A");
    }

    [Fact]
    public void TestInvalidNiNumbers()
    {
        TestInvalidNiNumber("");

        for (char suffix = 'E'; suffix <= 'Z'; suffix++)
            TestInvalidNiNumber($"AB123456{suffix}");

        TestInvalidNiNumber($"AO123456A");

        TestInvalidNiNumber($"BG123456A");
        TestInvalidNiNumber($"GB123456A");
        TestInvalidNiNumber($"KN123456A");
        TestInvalidNiNumber($"NK123456A");
        TestInvalidNiNumber($"NT123456A");
        TestInvalidNiNumber($"TN123456A");
        TestInvalidNiNumber($"ZZ123456A");

        var invalidPrefixes = new[] { "D", "F", "I", "Q", "U", "V" };

        foreach (var prefix in invalidPrefixes)
        {
            TestInvalidNiNumber($"{prefix}A123456A");
            TestInvalidNiNumber($"A{prefix}123456A");
        }
    }

    [Fact]
    public void TestImplicitCasts()
    {
        string value = new NiNumber("bc654321d");
        value.Should().Be("BC654321D");

        NiNumber niNumber = "EG 12 45  78 A";
        niNumber.ToString().Should().Be("EG124578A");
    }

    [Fact]
    public void TestToStringMethods()
    {
        var value = new NiNumber("bc654321d");
        value.ToString().Should().Be("BC654321D");
        value.ToString(true).Should().Be("BC 65 43 21 D");
    }

    private void TestInvalidNiNumber(string niNumber)
    {
        Action action = () => new NiNumber(niNumber);
        action.Should().Throw<ArgumentException>()
            .WithMessage("Argument is not a valid National Insurance Number (Parameter 'niNumber')");

        action = () => new NiNumber(niNumber.ToLower());
        action.Should().Throw<ArgumentException>()
            .WithMessage("Argument is not a valid National Insurance Number (Parameter 'niNumber')");
    }
}