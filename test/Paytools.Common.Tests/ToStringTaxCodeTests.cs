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

using Test = Paytools.Common.Tests.TaxCodeTestHelper;

namespace Paytools.Common.Tests;

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