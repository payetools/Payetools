using Paytools.Common;
using Paytools.IncomeTax.Model;
using Test = Paytools.IncomeTax.Tests.TaxCodeTestHelper;

namespace Paytools.IncomeTax.Tests;

public class NonCumulativeTaxCodeTests
{
    [Fact]
    public void CheckNonCumulativeX()
    {
        Test.RunValidNonCumulativeCodeTest("BRX", TaxTreatment.BR);
        Test.RunValidNonCumulativeCodeTest("BR X", TaxTreatment.BR);
        Test.RunValidNonCumulativeCodeTest("BR  X", TaxTreatment.BR);
        Test.RunValidNonCumulativeCodeTest("D0X", TaxTreatment.D0);
        Test.RunValidNonCumulativeCodeTest("D1 X", TaxTreatment.D1);
        Test.RunValidNonCumulativeCodeTest("D2  X", TaxTreatment.D2);
        Test.RunValidNonCumulativeCodeTest("NT X", TaxTreatment.NT);
    }

    [Fact]
    public void CheckNonCumulativeW1()
    {
        Test.RunValidNonCumulativeCodeTest("BRW1", TaxTreatment.BR);
        Test.RunValidNonCumulativeCodeTest("BR W1", TaxTreatment.BR);
        Test.RunValidNonCumulativeCodeTest("BR  W1", TaxTreatment.BR);
        Test.RunValidNonCumulativeCodeTest("D0W1", TaxTreatment.D0);
        Test.RunValidNonCumulativeCodeTest("D1 W1", TaxTreatment.D1);
        Test.RunValidNonCumulativeCodeTest("D2  W1", TaxTreatment.D2);
        Test.RunValidNonCumulativeCodeTest("NT W1", TaxTreatment.NT);
    }

    [Fact]
    public void CheckNonCumulativeM1()
    {
        Test.RunValidNonCumulativeCodeTest("BRM1", TaxTreatment.BR);
        Test.RunValidNonCumulativeCodeTest("BR M1", TaxTreatment.BR);
        Test.RunValidNonCumulativeCodeTest("BR  M1", TaxTreatment.BR);
        Test.RunValidNonCumulativeCodeTest("D0M1", TaxTreatment.D0);
        Test.RunValidNonCumulativeCodeTest("D1 M1", TaxTreatment.D1);
        Test.RunValidNonCumulativeCodeTest("D2  M1", TaxTreatment.D2);
        Test.RunValidNonCumulativeCodeTest("NT M1", TaxTreatment.NT);
    }

    [Fact]
    public void CheckNonCumulativeM1W1()
    {
        Test.RunValidNonCumulativeCodeTest("BRW1M1", TaxTreatment.BR);
        Test.RunValidNonCumulativeCodeTest("BR W1M1", TaxTreatment.BR);
        Test.RunValidNonCumulativeCodeTest("BR  W1M1", TaxTreatment.BR);

        Test.RunValidNonCumulativeCodeTest("BRW1/M1", TaxTreatment.BR);
        Test.RunValidNonCumulativeCodeTest("BR W1/M1", TaxTreatment.BR);
        Test.RunValidNonCumulativeCodeTest("BR  W1/M1", TaxTreatment.BR);
    }
    [Fact]
    public void CheckNonCumulative1257LM1W1()
    {
        Test.RunValidNonCumulativeCodeTest("1257LX", TaxTreatment.L);
        Test.RunValidNonCumulativeCodeTest("1257L X", TaxTreatment.L);
        Test.RunValidNonCumulativeCodeTest("1257L  X", TaxTreatment.L);

        Test.RunValidNonCumulativeCodeTest("1257LW1M1", TaxTreatment.L);
        Test.RunValidNonCumulativeCodeTest("1257L W1M1", TaxTreatment.L);
        Test.RunValidNonCumulativeCodeTest("1257L  W1M1", TaxTreatment.L);

        Test.RunValidNonCumulativeCodeTest("1257MW1/M1", TaxTreatment.M);
        Test.RunValidNonCumulativeCodeTest("1257N W1/M1", TaxTreatment.N);
        Test.RunValidNonCumulativeCodeTest("1257L  W1/M1", TaxTreatment.L);
    }
}
