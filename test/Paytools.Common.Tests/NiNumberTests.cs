// Copyright (c) 2023 Paytools Foundation.
//
// Licensed under the Apache License, Version 2.0 (the "License") ~
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

namespace Paytools.Common.Tests;

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