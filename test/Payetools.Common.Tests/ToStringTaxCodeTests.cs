// Copyright (c) 2023 Payetools Foundation.
//
// Payetools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Payetools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

using Test = Payetools.Common.Tests.TaxCodeTestHelper;

namespace Payetools.Common.Tests;

public class ToStringTaxCodeTests
{
    [Fact]
    public void Check1257LCode()
    {
        Test.RunToStringTest("1257L", "1257L", false);
        Test.RunToStringTest("S1257L", "S1257L", false);
        Test.RunToStringTest("C1257L", "C1257L", false);
    }

    [Fact]
    public void CheckNonCumulative1257LCode()
    {
        Test.RunToStringTest("1257L X", "1257L", true);
        Test.RunToStringTest("S1257L X", "S1257L", true);
        Test.RunToStringTest("C1257L X", "C1257L", true);
    }

    [Fact]
    public void CheckKCode()
    {
        Test.RunToStringTest("K1257 X", "K1257", true);
        Test.RunToStringTest("SK1257 X", "SK1257", true);
        Test.RunToStringTest("CK1257 X", "CK1257", true);
    }

    [Fact]
    public void Check1257LFullCode()
    {
        Test.RunToFullStringTest("1257L", "1257L", false);
        Test.RunToFullStringTest("S1257L", "S1257L", false);
        Test.RunToFullStringTest("C1257L", "C1257L", false);
    }

    [Fact]
    public void CheckNonCumulative1257LFullCode()
    {
        Test.RunToFullStringTest("1257L X", "1257L X", true);
        Test.RunToFullStringTest("S1257L X", "S1257L X", true);
        Test.RunToFullStringTest("C1257L X", "C1257L X", true);
    }

    [Fact]
    public void CheckKFullCode()
    {
        Test.RunToFullStringTest("K1257 X", "K1257 X", true);
        Test.RunToFullStringTest("SK1257 X", "SK1257 X", true);
        Test.RunToFullStringTest("CK1257 X", "CK1257 X", true);
    }
}