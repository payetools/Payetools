namespace Paytools.Pensions;

public interface IPensionContributionCalculator
{
    PensionContributionCalculationResult Calculate(decimal pensionableSalary,
        decimal employerContributionPercentage,
        decimal employeeContribution,
        bool employeeContributionIsFixedAmount = false,
        decimal avcForPeriod = 0.0m);

    PensionContributionCalculationResult CalculateUnderSalaryExchange(decimal pensionableSalary,
        decimal employerContributionPercentage,
        IEmployerNiSavingsCalculator employersNiSavingsCalculator,
        decimal employeeSalaryExchanged,
        bool employeeSalaryExchangedIsFixedAmount = false,
        decimal avcForPeriod = 0.0M);
}