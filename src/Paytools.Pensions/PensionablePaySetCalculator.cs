namespace Paytools.Pensions;

public class PensionablePaySetCalculator : PensionContributionCalculator
{
    public override EarningsBasis EarningsBasis { get; }

    public PensionablePaySetCalculator(EarningsBasis earningsBasis, PensionTaxTreatment taxTreatment, decimal? basicRateOfTax = null)
        : base(taxTreatment, basicRateOfTax)
    {
        EarningsBasis = earningsBasis;
    }

    protected override (decimal earningsForPensionCalculation, decimal employerContribution, decimal employeeContribution) CalculateContributions(decimal pensionableSalary,
        decimal employerContributionPercentage,
        decimal employeeContribution,
        bool employeeContributionIsFixedAmount = false)
    {
        return (pensionableSalary,
            decimal.Round(pensionableSalary * employerContributionPercentage / 100.0m, 2, MidpointRounding.AwayFromZero),
                employeeContributionIsFixedAmount ? employeeContribution :
                    decimal.Round(pensionableSalary * employeeContribution / 100.0m, 2, MidpointRounding.AwayFromZero));
    }
}
