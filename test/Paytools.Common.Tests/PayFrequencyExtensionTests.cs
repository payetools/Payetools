using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paytools.Common.Tests;

public class PayFrequencyExtensionTests
{
    [Fact]
    public void TestTaxYearPeriodCounts()
    {
        Assert.Equal(52, PayFrequency.Weekly.GetStandardTaxPeriodCount());
        Assert.Equal(26, PayFrequency.TwoWeekly.GetStandardTaxPeriodCount());
        Assert.Equal(13, PayFrequency.FourWeekly.GetStandardTaxPeriodCount());
        Assert.Equal(12, PayFrequency.Monthly.GetStandardTaxPeriodCount());
        Assert.Equal(1, PayFrequency.Annually.GetStandardTaxPeriodCount());
    }
}
