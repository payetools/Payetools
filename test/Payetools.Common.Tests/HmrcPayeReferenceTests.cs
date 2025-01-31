// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using FluentAssertions;
using Payetools.Common.Model;

namespace Payetools.Common.Tests;

public class HmrcPayeReferenceTests
{
    [Fact]
    public void TestValidPayeReferences()
    {
        var success = HmrcPayeReference.TryParse("123/ABC", out var result);
        success.Should().BeTrue();
        result.ToString().Should().Be("123/ABC");
        result?.HmrcOfficeNumber.Should().Be(123);
        result?.EmployerPayeReference.Should().Be("ABC");

        success = HmrcPayeReference.TryParse(" 456 / ABC12345", out result);
        success.Should().BeTrue();
        result.ToString().Should().Be("456/ABC12345");
        result?.HmrcOfficeNumber.Should().Be(456);
        result?.EmployerPayeReference.Should().Be("ABC12345");
    }

    [Fact]
    public void TestInvalidAccountsOfficeReferences()
    {
        var success = HmrcPayeReference.TryParse("1/ABC", out var result);
        success.Should().BeFalse();
        result.Should().BeNull();

        success = HmrcPayeReference.TryParse("123/ABC1212121212121", out result);
        success.Should().BeFalse();
        result.Should().BeNull();

        success = HmrcPayeReference.TryParse("123/?$%", out result);
        success.Should().BeFalse();
        result.Should().BeNull();

        success = HmrcPayeReference.TryParse(null, out result);
        success.Should().BeFalse();
        result.Should().BeNull();

        success = HmrcPayeReference.TryParse(string.Empty, out result);
        success.Should().BeFalse();
        result.Should().BeNull();
    }

    [Fact]
    public void TestImplicitCast()
    {
        var success = HmrcPayeReference.TryParse("123 / ABC", out var result);
        success.Should().BeTrue();
        string stringResult = result ?? "";
        stringResult.Should().Be("123/ABC");
    }
}
