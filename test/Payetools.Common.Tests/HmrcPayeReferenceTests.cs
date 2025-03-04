// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Shouldly;
using Payetools.Common.Model;

namespace Payetools.Common.Tests;

public class HmrcPayeReferenceTests
{
    [Fact]
    public void TestValidPayeReferences()
    {
        var success = HmrcPayeReference.TryParse("123/ABC", out var result);
        success.ShouldBeTrue();
        result.ToString().ShouldBe("123/ABC");
        result?.HmrcOfficeNumber.ShouldBe(123);
        result?.EmployerPayeReference.ShouldBe("ABC");

        success = HmrcPayeReference.TryParse(" 456 / ABC12345", out result);
        success.ShouldBeTrue();
        result.ToString().ShouldBe("456/ABC12345");
        result?.HmrcOfficeNumber.ShouldBe(456);
        result?.EmployerPayeReference.ShouldBe("ABC12345");
    }

    [Fact]
    public void TestInvalidAccountsOfficeReferences()
    {
        var success = HmrcPayeReference.TryParse("1/ABC", out var result);
        success.ShouldBeFalse();
        result.ShouldBeNull();

        success = HmrcPayeReference.TryParse("123/ABC1212121212121", out result);
        success.ShouldBeFalse();
        result.ShouldBeNull();

        success = HmrcPayeReference.TryParse("123/?$%", out result);
        success.ShouldBeFalse();
        result.ShouldBeNull();

        success = HmrcPayeReference.TryParse(null, out result);
        success.ShouldBeFalse();
        result.ShouldBeNull();

        success = HmrcPayeReference.TryParse(string.Empty, out result);
        success.ShouldBeFalse();
        result.ShouldBeNull();
    }

    [Fact]
    public void TestImplicitCast()
    {
        var success = HmrcPayeReference.TryParse("123 / ABC", out var result);
        success.ShouldBeTrue();
        string stringResult = result ?? "";
        stringResult.ShouldBe("123/ABC");
    }
}
