// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

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
