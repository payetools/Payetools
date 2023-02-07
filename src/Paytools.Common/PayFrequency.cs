namespace Paytools.Common;

public enum PayFrequency
{
    Unspecified,
    Weekly,
    TwoWeekly,
    FourWeekly,
    Monthly,
    Quarterly,
    BiAnnually,
    Annually
}

public static class PayFrequencyExtensions
{
    public static int GetStandardTaxPeriodCount(this PayFrequency payFrequency)
    {
        return payFrequency switch
        {
            PayFrequency.Weekly => 52,
            PayFrequency.TwoWeekly => 26,
            PayFrequency.FourWeekly => 13,
            PayFrequency.Monthly => 12,
            PayFrequency.Quarterly => 4,
            PayFrequency.BiAnnually => 2,
            PayFrequency.Annually => 1,
            _ => throw new ArgumentException($"Invalid pay frequency value {payFrequency}", nameof(PayFrequency))
        };
    }
}