using System.Net.NetworkInformation;

namespace Paytools.Common;

public enum TaxTreatment
{
    Unspecified,
    NT,
    BR,
    D0,
    D1,
    D2,
    _0T,
    K,
    L,
    M,
    N
}

public static class TaxTreatmentExtensions
{
    public static int GetBandIndex(this TaxTreatment taxTreatment)
    {
        return taxTreatment switch
        {
            TaxTreatment.BR => 0,
            TaxTreatment.D0 => 1,
            TaxTreatment.D1 => 2,
            TaxTreatment.D2 => 3,
            _ => throw new ArgumentException($"Band index not valid for tax treatment {taxTreatment}", nameof(taxTreatment))
        };
    }
}