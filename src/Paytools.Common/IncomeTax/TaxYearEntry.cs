using System.Collections.Immutable;

namespace Paytools.Common;

public class TaxYearEntry
{
    public CountriesForTaxPurposes ApplicableCountries { get; init; }
    public ImmutableArray<PersonalAllowance> PersonalAllowances { get; init; } = ImmutableArray<PersonalAllowance>.Empty;
    public ImmutableArray<HmrcDeductionBand> TaxBands { get; init; } = ImmutableArray<HmrcDeductionBand>.Empty;
}
