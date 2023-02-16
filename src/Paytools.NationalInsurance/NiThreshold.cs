namespace Paytools.NationalInsurance;

public enum NiThreshold
{
    /// <summary>Lower earnings limit</summary>
    LEL,

    /// <summary>Primary threshold</summary>
    PT,

    /// <summary>Secondary threshold</summary>
    ST,

    /// <summary>Freeport upper secondary threshold</summary>
    FUST,

    /// <summary>Upper secondary threshold</summary>
    UST,

    /// <summary>Apprentice upper secondary threshold</summary>
    AUST,

    /// <summary>Veterans upper secondary threshold</summary>
    VUST,

    /// <summary>Upper earnings limit</summary>
    UEL,

    /// <summary>Number of elements in enum</summary>
    Count = 8
}

public static class NationalInsuranceThresholdExtensions
{
    public static int GetIndex(this NiThreshold threshold) =>
        (int)threshold;
}