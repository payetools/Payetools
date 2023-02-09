// Copyright (c) 2023 Paytools Foundation
//
// Licensed under the Apache License, Version 2.0 (the "License")~
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

public class HmrcAccountsOfficeReferenceTests
{
    [Fact]
    public void TestValidAccountsOfficeReferences()
    {
        var aor = new HmrcAccountsOfficeReference("123PX12345678");
        aor.ToString().Should().Be("123PX12345678");

        aor = new HmrcAccountsOfficeReference("123PX1234567X");
        aor.ToString().Should().Be("123PX1234567X");

        aor = new HmrcAccountsOfficeReference("000PY1234567X");
        aor.ToString().Should().Be("000PY1234567X");
    }

    [Fact]
    public void TestInvalidAccountsOfficeReferences()
    {
        Action action = () => new HmrcAccountsOfficeReference("");
        action.Should().Throw<ArgumentException>()
            .WithMessage("Argument is not a valid Accounts Office Reference (Parameter 'accountsOfficeReference')");

        action = () => new HmrcAccountsOfficeReference("123XX12345678");
        action.Should().Throw<ArgumentException>()
            .WithMessage("Argument is not a valid Accounts Office Reference (Parameter 'accountsOfficeReference')");

        action = () => new HmrcAccountsOfficeReference("12PX123456789");
        action.Should().Throw<ArgumentException>()
            .WithMessage("Argument is not a valid Accounts Office Reference (Parameter 'accountsOfficeReference')");

        action = () => new HmrcAccountsOfficeReference("123PX1234567");
        action.Should().Throw<ArgumentException>()
            .WithMessage("Argument is not a valid Accounts Office Reference (Parameter 'accountsOfficeReference')");

        action = () => new HmrcAccountsOfficeReference("123PX1234567Y");
        action.Should().Throw<ArgumentException>()
            .WithMessage("Argument is not a valid Accounts Office Reference (Parameter 'accountsOfficeReference')");

        action = () => new HmrcAccountsOfficeReference("123PX123456789");
        action.Should().Throw<ArgumentException>()
            .WithMessage("Argument is not a valid Accounts Office Reference (Parameter 'accountsOfficeReference')");
    }
}