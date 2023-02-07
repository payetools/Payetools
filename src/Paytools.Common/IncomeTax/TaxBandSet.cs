namespace Paytools.Common;

public record TaxBandSet
{
    public TaxYearEnding ApplicableTaxYearEnding { get; init; }
    public IReadOnlyList<TaxYearEntry> TaxYearEntries { get; init; } = default!;
}