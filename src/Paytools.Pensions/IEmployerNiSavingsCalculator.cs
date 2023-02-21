namespace Paytools.Pensions;

public interface IEmployerNiSavingsCalculator
{
    decimal EmployerNiSavingsReinvestmentPercentage { get; init; }

    decimal Calculate(decimal salaryExchanged);
}
