// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Payetools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

using Payetools.Common.Model;
using Test = Payetools.Common.Tests.TaxCodeTestHelper;

namespace Payetools.Common.Tests;

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