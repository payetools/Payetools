namespace Paytools.NationalInsurance;

public readonly struct NiEarningsBreakdown
{
    public decimal EarningsUpToAndIncludingLEL { get; init; }
    public decimal EarningsAboveLELUpToAndIncludingST { get; init; }
    public decimal EarningsAboveSTUpToAndIncludingPT { get; init; }
    public decimal EarningsAbovePTUpToAndIncludingFUST { get; init; }
    public decimal EarningsAboveFUSTUpToAndIncludingUEL { get; init; }
    public decimal EarningsAboveUEL { get; init; }
    public decimal EarningsAboveSTUpToAndIncludingUEL { get; init; }
}