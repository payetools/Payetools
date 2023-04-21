// Copyright (c) 2023 Paytools Foundation.
//
// Paytools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Paytools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

using FluentAssertions;
using Paytools.Common.Model;

namespace Paytools.Common.Tests;

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
