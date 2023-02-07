namespace Paytools.Common;

public readonly struct PersonalAllowance
{
    public PayFrequency PayFrequency { get; init; }
    public decimal Value { get; init; }
}
