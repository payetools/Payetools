// This example code may be freely used without restriction; it may be freely copied, adapted and
// used without attribution.

using Payetools.Pensions.Model;

namespace Payroll
{
    internal class PensionContributionLevels : IPensionContributionLevels
    {
        public decimal EmployeeContribution => 5.0m;

        public bool EmployeeContributionIsFixedAmount => false;

        public decimal EmployerContribution => 3.0m;

        public bool EmployerContributionIsFixedAmount => false;

        public bool SalaryExchangeApplied => false;

        public decimal? EmployersNiReinvestmentPercentage => null;

        public decimal? AvcForPeriod => null;

        public decimal? SalaryForMaternityPurposes => null;
    }
}
