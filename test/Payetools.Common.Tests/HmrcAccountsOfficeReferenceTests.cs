// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Payetools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

using FluentAssertions;
using Payetools.Common.Model;

namespace Payetools.Common.Tests;

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

    [Fact]
    public void TestImplicitCast()
    {
        string value = new HmrcAccountsOfficeReference("123PX12345678");
        value.Should().Be("123PX12345678");
    }
}