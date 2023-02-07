namespace Paytools.Common;

public record HmrcDeductionBand
{
    public string Description { get; init; } = default!;
    public decimal? From { get; init; }
    public decimal? To { get; init; }
    public decimal Rate { get; init; }
    public bool IsBottomRate => From == null;
    public bool IsTopRate => To == null;
}