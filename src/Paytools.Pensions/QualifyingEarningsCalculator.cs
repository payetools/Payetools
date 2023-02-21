namespace Paytools.Pensions;

public class QualifyingEarningsCalculator : PensionContributionCalculator
{
    private readonly decimal _lowerLevelForQualifyingEarnings;
    private readonly decimal _upperLevelForQualifyingEarnings;

    public override EarningsBasis EarningsBasis => EarningsBasis.QualifyingEarnings;

    public QualifyingEarningsCalculator(PensionTaxTreatment taxTreatment,
        decimal lowerLevelForQualifyingEarnings,
        decimal upperLevelForQualifyingEarnings,
        decimal? basicRateOfTax = null)
        : base(taxTreatment, basicRateOfTax)
    {
        _lowerLevelForQualifyingEarnings = lowerLevelForQualifyingEarnings;
        _upperLevelForQualifyingEarnings = upperLevelForQualifyingEarnings;
    }

    protected override (decimal earningsForPensionCalculation, decimal employerContribution, decimal employeeContribution) CalculateContributions(decimal pensionableSalary,
        decimal employerContributionPercentage,
        decimal employeeContribution,
        bool employeeContributionIsFixedAmount = false)
    {
        decimal bandedEarnings = pensionableSalary <= _lowerLevelForQualifyingEarnings ?
            0.0m : Math.Min(pensionableSalary, _upperLevelForQualifyingEarnings) - _lowerLevelForQualifyingEarnings;

        return (bandedEarnings,
            decimal.Round(bandedEarnings * employerContributionPercentage / 100.0m, 2, MidpointRounding.AwayFromZero),
                employeeContributionIsFixedAmount ? employeeContribution :
                    decimal.Round(bandedEarnings * employeeContribution / 100.0m, 2, MidpointRounding.AwayFromZero));
    }
}