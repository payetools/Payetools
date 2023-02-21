namespace Paytools.Pensions.Tests;

internal class TestEmployerNiSavingsCalculator : IEmployerNiSavingsCalculator
{
    public decimal EmployerNiRate { get; init; }
    public decimal EmployerNiSavingsReinvestmentPercentage { get; init; }


    public TestEmployerNiSavingsCalculator(decimal employerNiRate, decimal employerNiSavingsReinvestmentPercentage)
    {
        EmployerNiRate = employerNiRate;
        EmployerNiSavingsReinvestmentPercentage = employerNiSavingsReinvestmentPercentage;
    }

    public decimal Calculate(decimal salaryExchanged)
    {
        return decimal.Round(salaryExchanged * EmployerNiRate * EmployerNiSavingsReinvestmentPercentage / 100.0m, 2, MidpointRounding.AwayFromZero);
    }
}
