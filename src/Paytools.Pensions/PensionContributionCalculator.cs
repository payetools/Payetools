// Copyright (c) 2023 Paytools Foundation.  All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License") ~
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace Paytools.Pensions;

public abstract class PensionContributionCalculator : IPensionContributionCalculator
{
    private readonly PensionTaxTreatment _taxTreatment;
    private readonly decimal? _basicRateOfTax;

    public abstract EarningsBasis EarningsBasis { get; }

    protected decimal TaxReliefFactor => (decimal)(_basicRateOfTax.HasValue ? (1 - _basicRateOfTax) : 1.0m);

    public PensionContributionCalculator(PensionTaxTreatment taxTreatment, decimal? basicRateOfTax = null)
    {
        _taxTreatment = taxTreatment;

        _basicRateOfTax = basicRateOfTax ??
            (taxTreatment != PensionTaxTreatment.ReliefAtSource ? null :
                throw new ArgumentException("Parameter cannot be null for relief at source tax treatment", nameof(basicRateOfTax)));
    }

    public virtual PensionContributionCalculationResult Calculate(
        decimal pensionableSalary,
        decimal employerContributionPercentage,
        decimal employeeContribution,
        bool employeeContributionIsFixedAmount = false,
        decimal avcForPeriod = 0.0M)
    {
        decimal adjustedEmployeeContribution = employeeContributionIsFixedAmount ? employeeContribution :
            _taxTreatment == PensionTaxTreatment.ReliefAtSource ? employeeContribution * TaxReliefFactor : employeeContribution;

        var contributions = CalculateContributions(pensionableSalary,
            employerContributionPercentage,
            adjustedEmployeeContribution,
            employeeContributionIsFixedAmount);

        return new PensionContributionCalculationResult()
        {
            BandedEarnings = EarningsBasis == EarningsBasis.QualifyingEarnings ? contributions.earningsForPensionCalculation : null,
            EarningsBasis = EarningsBasis,
            EmployeeAvcAmount = avcForPeriod,
            EmployeeContributionAmount = contributions.employeeContribution + avcForPeriod,
            EmployeeContributionFixedAmount = employeeContributionIsFixedAmount ? employeeContribution : null,
            EmployeeContributionPercentage = employeeContributionIsFixedAmount ? null : employeeContribution,
            EmployerContributionAmount = contributions.employerContribution,
            EmployerContributionPercentage = employerContributionPercentage,
            EmployersNiReinvestmentPercentage = null,
            EmployerContributionAmountBeforeSalaryExchange = null,
            EmployerNiSavingsToReinvest = null,
            PensionableSalaryInPeriod = pensionableSalary,
            SalaryExchangeApplied = false,
            SalaryExchangedAmount = null
        };
    }

    public PensionContributionCalculationResult CalculateUnderSalaryExchange(
        decimal pensionableSalary,
        decimal employerContributionPercentage,
        IEmployerNiSavingsCalculator employersNiSavingsCalculator,
        decimal employeeSalaryExchanged,
        bool employeeSalaryExchangedIsFixedAmount = false,
        decimal avcForPeriod = 0.0M)
    {
        var contributions = CalculateContributions(pensionableSalary,
            employerContributionPercentage,
            employeeSalaryExchanged,
            employeeSalaryExchangedIsFixedAmount);

        var employerNiSavings = employersNiSavingsCalculator.Calculate(contributions.employeeContribution);

        return new PensionContributionCalculationResult()
        {
            BandedEarnings = EarningsBasis == EarningsBasis.QualifyingEarnings ? contributions.earningsForPensionCalculation : null,
            EarningsBasis = EarningsBasis,
            EmployeeAvcAmount = avcForPeriod,
            EmployeeContributionAmount = avcForPeriod,
            EmployeeContributionFixedAmount = employeeSalaryExchangedIsFixedAmount ? employeeSalaryExchanged : null,
            EmployeeContributionPercentage = employeeSalaryExchangedIsFixedAmount ? null : employeeSalaryExchanged,
            EmployerContributionAmount = contributions.employerContribution +
                contributions.employeeContribution +
                employerNiSavings,
            EmployerContributionPercentage = employerContributionPercentage,
            EmployersNiReinvestmentPercentage = null,
            EmployerContributionAmountBeforeSalaryExchange = contributions.employerContribution,
            EmployerNiSavingsToReinvest = employerNiSavings,
            PensionableSalaryInPeriod = pensionableSalary,
            SalaryExchangeApplied = true,
            SalaryExchangedAmount = contributions.employeeContribution
        };
    }

    protected abstract (decimal earningsForPensionCalculation, decimal employerContribution, decimal employeeContribution) CalculateContributions(decimal pensionableSalary,
        decimal employerContributionPercentage,
        decimal employeeContribution,
        bool employeeContributionIsFixedAmount = false);
}