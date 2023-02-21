using Test = Paytools.IncomeTax.Tests.TaxCodeTestHelper;

namespace Paytools.IncomeTax.Tests;

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
    public void CheckInvalidCodes()
    {
        // TODO: Add invalid tests
    }
}