namespace Paytools.Pensions;

public class EmployerNiSavingsCalculator
{
    public decimal EmployerNiSavingsReinvestmentPercentage { get; init; }

    public EmployerNiSavingsCalculator(decimal employerNiSavingsReinvestmentPercentage)
    {
        EmployerNiSavingsReinvestmentPercentage = employerNiSavingsReinvestmentPercentage;
    }

    public decimal Calculate(decimal salaryExchanged)
    {
        throw new NotImplementedException();
    }
}
