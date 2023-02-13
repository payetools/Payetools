using Paytools.Common;

namespace Paytools.IncomeTax.Tests.TestData;

public class IncomeTaxTestEntry
{
    public string Description { get; set; } = default!;
    public decimal GrossPay { get; set; }
    public decimal TaxablePayToDate { get; set; }
    public string TaxCode { get; set; } = default!;
    public string Wk1 { get; set; } = default!;
    public int Period { get; set; }
    public decimal TaxDue { get; set; }
    public decimal TaxDueToDate { get; set; }
    public PayFrequency PayFrequency { get; set; }

    public bool IsNonCumulative => Wk1 == "WM1";
}
