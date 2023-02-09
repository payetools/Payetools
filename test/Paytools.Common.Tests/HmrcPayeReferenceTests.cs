// Copyright (c) 2023 Paytools Foundation
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using FluentAssertions;
using Paytools.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
