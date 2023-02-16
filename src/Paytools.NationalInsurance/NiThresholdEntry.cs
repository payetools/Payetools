namespace Paytools.NationalInsurance;

public record NiThresholdEntry
{
    public NiThreshold Threshold {  get; init; }
    public decimal ThresholdValuePerWeek { get; init; }
    public decimal ThresholdValuePerMonth { get; init; }
    public decimal ThresholdValuePerYear { get; init; }
}
