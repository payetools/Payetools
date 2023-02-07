using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paytools.Common.Tests;

public static class TaxYearTestHelper
{
    public static void RunTaxPeriodTest(TaxYearEnding taxYearEnding, DateOnly payDate, PayFrequency payFrequency, int expectedPeriodNumber)
    {
        var taxYear = new TaxYear(taxYearEnding);
        var periodNumber = taxYear.GetTaxPeriod(payDate, payFrequency);

        Assert.Equal(expectedPeriodNumber, periodNumber);
    }
}
