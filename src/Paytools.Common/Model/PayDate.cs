using Paytools.Common.Extensions;
using System.Runtime.CompilerServices;

namespace Paytools.Common.Model;

public readonly struct PayDate
{
    public DateOnly Date { get; init; }
    public TaxYear TaxYear { get; init; }
    public int TaxPeriod { get; init; }

    public static implicit operator DateTime(PayDate payDate) => payDate.Date.ToMiddayUtcDateTime();

    public PayDate(DateOnly date, PayFrequency payFrequency)
    {
        Date = date;
        TaxYear = new TaxYear(date);
        TaxPeriod = TaxYear.GetTaxPeriod(date, payFrequency);
    }

    public PayDate(int year, int month, int day, PayFrequency payFrequency)
        : this(new DateOnly(year,month,day), payFrequency)
    {
    }
}