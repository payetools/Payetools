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

using Paytools.Common.Model;
using Test = Paytools.Common.Tests.TaxCodeTestHelper;

namespace Paytools.Common.Tests;

public class FixedTaxCodeTests
{
    [Fact]
    public void CheckNTCode()
    {
        Test.RunFixedCodeTest("NT", TaxTreatment.NT);
    }

    [Fact]
    public void CheckDXCodes()
    {
        Test.RunFixedCodeTest("D0", TaxTreatment.D0);
        Test.RunFixedCodeTest("D1", TaxTreatment.D1);
        Test.RunFixedCodeTest("D2", TaxTreatment.D2);
        Test.RunFixedCodeTest("SD0", TaxTreatment.D0);
        Test.RunFixedCodeTest("SD1", TaxTreatment.D1);
        Test.RunFixedCodeTest("SD2", TaxTreatment.D2);
        Test.RunFixedCodeTest("CD0", TaxTreatment.D0);
        Test.RunFixedCodeTest("CD1", TaxTreatment.D1);
        Test.RunFixedCodeTest("CD2", TaxTreatment.D2);
    }

    [Fact]
    public void CheckBRCodes()
    {
        Test.RunFixedCodeTest("BR", TaxTreatment.BR);
        Test.RunFixedCodeTest("SBR", TaxTreatment.BR);
        Test.RunFixedCodeTest("CBR", TaxTreatment.BR);
    }

    [Fact]
    public void Check0TCodes()
    {
        Test.RunFixedCodeTest("0T", TaxTreatment._0T);
        Test.RunFixedCodeTest("S0T", TaxTreatment._0T);
        Test.RunFixedCodeTest("C0T", TaxTreatment._0T);
    }

    [Fact]
    public void CheckInvalidCodes()
    {
        Test.RunInvalidCodeTest("NX");
        Test.RunInvalidCodeTest("BY");
        Test.RunInvalidCodeTest("AR");
        Test.RunInvalidCodeTest("D3");
        Test.RunInvalidCodeTest("XBR");
        Test.RunInvalidCodeTest("CJR");
        Test.RunInvalidCodeTest("SD5");
    }
}