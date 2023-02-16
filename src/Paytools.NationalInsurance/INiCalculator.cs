namespace Paytools.NationalInsurance;

public interface INiCalculator
{
    NiCalculationResult Calculate(decimal nicableSalaryInPeriod,
        NiCategory niCategory);

    NiCalculationResult CalculateDirectors(decimal nicableSalaryInPeriod,
        NiCategory niCategory);
}
