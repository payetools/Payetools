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

/// <summary>
/// Abstract class that represents different types of pension contribution calculators.
/// </summary>
public abstract class PensionContributionCalculator : IPensionContributionCalculator
{
    private readonly PensionTaxTreatment _taxTreatment;
    private readonly decimal? _basicRateOfTax;

    /// <summary>
    /// Gets the earnings basis of this pension contribution calculator i.e., Qualifying Earnings vs Pensionable
    /// Pay Set X.
    /// </summary>
    public abstract EarningsBasis EarningsBasis { get; }

    /// <summary>
    /// Gets the tax relief factor to apply for relief at source pensions.  For example, if the basic rate of
    /// tax is 20%, then the tax relief factor is 0.8.
    /// </summary>
    protected decimal TaxReliefFactor => (decimal)(_basicRateOfTax.HasValue ? (1 - _basicRateOfTax) : 1.0m);

    /// <summary>
    /// Initialises a new instance of <see cref="PensionContributionCalculator"/> with the supplied tax treatment
    /// and optional basic rate of tax (needed for relief at source pensions only).
    /// </summary>
    /// <param name="taxTreatment">Tax treatment i.e., relief at source vs net pay arrangement.</param>
    /// <param name="basicRateOfTax">Basic rate of tax to use for relief at source pensions.  Optional.</param>
    /// <exception cref="ArgumentException">Thrown if no basic rate of tax is supplied for a relief at
    /// source pension.</exception>
    public PensionContributionCalculator(PensionTaxTreatment taxTreatment, decimal? basicRateOfTax = null)
    {
        _taxTreatment = taxTreatment;

        _basicRateOfTax = basicRateOfTax ??
            (taxTreatment != PensionTaxTreatment.ReliefAtSource ? null :
                throw new ArgumentException("Parameter cannot be null for relief at source tax treatment", nameof(basicRateOfTax)));
    }

    /// <summary>
    /// Calculates the appropriate employee and employer pension contributions based on the supplied employer
    /// and employee rates.  Also supports the ability to supply a fixed employee contribution as an
    /// alternative to an employee percentage rate.
    /// </summary>
    /// <param name="pensionableSalary">Pensionable salary to be used for calculation.</param>
    /// <param name="employerContributionPercentage">Employer contribution level, expressed in percentage points (i.e., 3% = 3.0m).</param>
    /// <param name="employeeContribution">Employee contribution level, either expressed in percentage points (i.e., 5% = 5.0m)
    /// or as a fixed amount (i.e. £500.00), as indicated by the following parameter.</param>
    /// <param name="employeeContributionIsFixedAmount">True if the previous parameter should be treated as a fixed amount; false if
    /// it should be treated as a percentage.</param>
    /// <param name="avcForPeriod">Any Additional Voluntary Contribution (AVC) on the part of the employee.</param>
    /// <param name="salaryForMaternityPurposes">Used to override the employer contribution when an individual is on
    /// maternity leave and should be paid employer contributions based on their contracted salary rather than their
    /// pensionable pay.</param>
    /// <returns>An instance of a <see cref="IPensionContributionCalculationResult"/> implementation that contains
    /// the results of the calculation.</returns>
    public virtual IPensionContributionCalculationResult Calculate(
        decimal pensionableSalary,
        decimal employerContributionPercentage,
        decimal employeeContribution,
        bool employeeContributionIsFixedAmount = false,
        decimal avcForPeriod = 0.0M,
        decimal? salaryForMaternityPurposes = null)
    {
        decimal adjustedEmployeeContribution = employeeContributionIsFixedAmount ? employeeContribution :
            _taxTreatment == PensionTaxTreatment.ReliefAtSource ? employeeContribution * TaxReliefFactor : employeeContribution;

        var contributions = CalculateContributions(pensionableSalary,
            employerContributionPercentage,
            adjustedEmployeeContribution,
            employeeContributionIsFixedAmount,
            salaryForMaternityPurposes);

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

    /// <summary>
    /// Calculates the appropriate employee and employer pension contributions based on the supplied employer
    /// and employee rates, but under salary exchange arrangements.  Here the output employee contribution will
    /// always be zero.  Also supports the ability to supply a fixed employee contribution as an
    /// alternative to an employee percentage rate.
    /// </summary>
    /// <param name="pensionableSalary">Pensionable salary to be used for calculation.</param>
    /// <param name="employerContributionPercentage">Employer contribution level, expressed in percentage points (i.e., 3% = 3.0m).</param>
    /// <param name="employersNiSavingsCalculator">Instance of <see cref="IEmployerNiSavingsCalculator"/> that knows how
    /// to calculate employer NI savings.</param>
    /// <param name="employeeSalaryExchanged">The level of employee's salary forgone as set out in the salary
    /// exchange arrangements.  Expressed either as a percentage in percentage points (e.g., 5% = 5.0m), or as a fixed
    /// amount, as indicated by the following parameter.  NB If fixed amount is given, it relates to the pay period
    /// (as opposed to annually).</param>
    /// <param name="employeeSalaryExchangedIsFixedAmount">True if the previous parameter should be treated as a fixed amount; false if
    /// it should be treated as a percentage.</param>
    /// <param name="avcForPeriod">Any Additional Voluntary Contribution (AVC) on the part of the employee.</param>
    /// <param name="salaryForMaternityPurposes">Used to override the employer contribution when an individual is on
    /// maternity leave and should be paid employer contributions based on their contracted salary rather than their
    /// pensionable pay.</param>
    /// <returns>An instance of a <see cref="IPensionContributionCalculationResult"/> implementation that contains
    /// the results of the calculation.</returns>
    public IPensionContributionCalculationResult CalculateUnderSalaryExchange(
        decimal pensionableSalary,
        decimal employerContributionPercentage,
        IEmployerNiSavingsCalculator employersNiSavingsCalculator,
        decimal employeeSalaryExchanged,
        bool employeeSalaryExchangedIsFixedAmount = false,
        decimal avcForPeriod = 0.0M,
        decimal? salaryForMaternityPurposes = null)
    {
        var contributions = CalculateContributions(pensionableSalary,
            employerContributionPercentage,
            employeeSalaryExchanged,
            employeeSalaryExchangedIsFixedAmount,
            salaryForMaternityPurposes);

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

    /// <summary>
    /// Abstract method signature for calculating pension contributions based on the supplied inputs.
    /// </summary>
    /// <param name="pensionableSalary">Pensionable salary to be used for calculation.</param>
    /// <param name="employerContributionPercentage">Employer contribution level, expressed in percentage points (i.e., 3% = 3.0m).</param>
    /// <param name="employeeContribution">Employee contribution level, either expressed in percentage points (i.e., 5% = 5.0m)
    /// or as a fixed amount (i.e. £500.00), as indicated by the following parameter.</param>
    /// <param name="employeeContributionIsFixedAmount">True if the previous parameter should be treated as a fixed amount; false if
    /// it should be treated as a percentage.</param>
    /// <param name="salaryForMaternityPurposes">Used to override the employer contribution when an individual is on
    /// maternity leave and should be paid employer contributions based on their contracted salary rather than their
    /// pensionable pay.</param>
    /// <returns>A tuple containing the earnings used for the calculation (employee only if maternity override applies),
    /// and the employer and employee contribution levels.</returns>
    protected abstract (decimal earningsForPensionCalculation, decimal employerContribution, decimal employeeContribution) CalculateContributions(
        decimal pensionableSalary,
        decimal employerContributionPercentage,
        decimal employeeContribution,
        bool employeeContributionIsFixedAmount,
        decimal? salaryForMaternityPurposes = null);
}