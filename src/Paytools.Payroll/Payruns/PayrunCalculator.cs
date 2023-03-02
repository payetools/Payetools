// Copyright (c) 2023 Paytools Foundation.
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

using Paytools.Common.Model;
using Paytools.Employment.Model;
using Paytools.IncomeTax;
using Paytools.NationalInsurance;
using Paytools.Payroll.Model;
using Paytools.Pensions;
using Paytools.Pensions.Model;
using Paytools.StudentLoans;
using System.Collections.Concurrent;

namespace Paytools.Payroll.Payruns;

/// <summary>
/// Represents the calculator that can process an employee's set of input payroll data and
/// provide the results of the calculations in the form of an <see cref="IEmployeePayrunResult"/>.
/// </summary>
public class PayrunCalculator : IPayrunCalculator
{
    private readonly Dictionary<CountriesForTaxPurposes, ITaxCalculator> _incomeTaxCalculators;
    private readonly INiCalculator _niCalculator;
    private readonly IPensionContributionCalculatorFactory _pensionCalculatorFactory;
    private readonly IStudentLoanCalculator _studentLoanCalculator;
    private readonly Dictionary<Tuple<EarningsBasis, PensionTaxTreatment>, IPensionContributionCalculator> _pensionCalculators;
    private readonly PayDate _payDate;

    /// <summary>
    /// Initialises a new instance of <see cref="PayrunCalculator"/> with the supplied factories
    /// and specified pay date.
    /// </summary>
    /// <param name="incomeTaxCalcFactory">Income tax calculator factory.</param>
    /// <param name="niCalcFactory">calculator factory.</param>
    /// <param name="pensionCalcFactory">Pension contributions calculator factory.</param>
    /// <param name="studentLoanCalcFactory">Student loan calculator factory.</param>
    /// <param name="payDate">Pay date for this payrun.</param>
    public PayrunCalculator(
        ITaxCalculatorFactory incomeTaxCalcFactory,
        INiCalculatorFactory niCalcFactory,
        IPensionContributionCalculatorFactory pensionCalcFactory,
        IStudentLoanCalculatorFactory studentLoanCalcFactory,
        PayDate payDate)
    {
        _incomeTaxCalculators = payDate.TaxYear.GetCountriesForYear()
            .Select(regime => (regime, calculator: incomeTaxCalcFactory.GetCalculator(regime, payDate)))
            .ToDictionary(kv => kv.regime, kv => kv.calculator);
        _niCalculator = niCalcFactory.GetCalculator(payDate);
        _pensionCalculatorFactory = pensionCalcFactory;
        _studentLoanCalculator = studentLoanCalcFactory.GetCalculator(payDate);
        _payDate = payDate;

        _pensionCalculators = new Dictionary<Tuple<EarningsBasis, PensionTaxTreatment>, IPensionContributionCalculator>();
    }

    /// <summary>
    /// Processes the supplied payrun entry calculating all the earnings and deductions, income tax, national insurance and
    /// other statutory deductions, and generating a result structure which includes the final net pay.
    /// </summary>
    /// <param name="entry">Instance of <see cref="IEmployeePayrunEntry"/> containing all the necessary input data for the
    /// payroll calculation.</param>
    /// <param name="result">An instance of <see cref="IEmployeePayrunResult"/> containing the results of the payroll calculations.</param>
    public void Process(ref IEmployeePayrunEntry entry, out IEmployeePayrunResult result)
    {
        var (grossPay,  taxablePay,  nicablePay,  pensionablePay) = GetTotalEarnings(ref entry);

        if (!_incomeTaxCalculators.TryGetValue(entry.Employment.TaxCode.ApplicableCountries, out var taxCalculator))
            throw new InvalidOperationException($"Unable to perform tax calculation as calculator for tax regime '{entry.Employment.TaxCode.TaxRegimeLetter}' is not available");

        var payrolledBenefitsAmount = GetTotalAmountForPayrolledBenefits(ref entry);
        var taxablePayWithBenefits = taxablePay + payrolledBenefitsAmount;

        taxCalculator.Calculate(taxablePayWithBenefits, payrolledBenefitsAmount,
            entry.Employment.TaxCode, entry.Employment.PayrollHistoryYtd.TaxablePayYtd,
            entry.Employment.PayrollHistoryYtd.TaxPaidYtd, entry.Employment.PayrollHistoryYtd.TaxUnpaidDueToRegulatoryLimit,
            out var taxCalculationResult);

        INiCalculationResult niCalculationResult;

        if (entry.Employment.IsDirector && entry.Employment.DirectorsNiCalculationMethod == Employment.DirectorsNiCalculationMethod.StandardAnnualisedEarningsMethod)
            _niCalculator.CalculateDirectors(entry.Employment.NiCategory, nicablePay, out niCalculationResult);
        else
            _niCalculator.Calculate(entry.Employment.NiCategory, nicablePay, out niCalculationResult);

        CalculatePensionContributions(ref entry, pensionablePay, out var pensionContributions);

        IStudentLoanCalculationResult studentLoanCalculationResult;

        if (entry.Employment.StudentLoanStatus != null)
        {
            _studentLoanCalculator.Calculate(grossPay, entry.Employment.StudentLoanStatus?.StudentLoanType,
                entry.Employment.StudentLoanStatus?.HasPostGradLoan == true, out studentLoanCalculationResult);
        }
        else
        {
            studentLoanCalculationResult = StudentLoanCalculationResult.NoStudentLoanApplicable;
        }

        result = new EmployeePayrunResult(entry.Employee, false, ref taxCalculationResult, ref niCalculationResult, ref studentLoanCalculationResult,
            ref pensionContributions, grossPay, ref entry.Employment.PayrollHistoryYtd);
    }

    private static (decimal grossPay, decimal taxablePay, decimal nicablePay, decimal pensionablePay) GetTotalEarnings(ref IEmployeePayrunEntry entry)
    {
        decimal grossPay = 0.0m;
        decimal taxablePay = 0.0m;
        decimal nicablePay = 0.0m;
        decimal pensionablePay = 0.0m;

        entry.Earnings.ForEach(e =>
        {
            grossPay += e.TotalEarnings;
            taxablePay += e.EarningsType.IsSubjectToTax ? e.TotalEarnings : 0.0m;
            nicablePay += e.EarningsType.IsSubjectToNi ? e.TotalEarnings : 0.0m;
            pensionablePay += e.EarningsType.IsPensionable ? e.TotalEarnings : 0.0m;
        });

        entry.Deductions.ForEach(d =>
        {
            taxablePay -= d.DeductionType.ReducesTaxablePay ? d.TotalDeduction : 0.0m;
            nicablePay -= d.DeductionType.ReducesNicablePay ? d.TotalDeduction : 0.0m;
        });

        entry.PayrolledBenefits.ForEach(b =>
        {
            taxablePay += b.AmountForPeriod;
        });

        return (grossPay, taxablePay, nicablePay, pensionablePay);
    }

    private static decimal GetTotalAmountForPayrolledBenefits(ref IEmployeePayrunEntry entry) =>
        entry.PayrolledBenefits.Sum(b => b.AmountForPeriod);

    private void CalculatePensionContributions(ref IEmployeePayrunEntry entry, decimal pensionablePay, out IPensionContributionCalculationResult result)
    {
        if (entry.Employment.PensionScheme == null)
        {
            result = PensionContributionCalculationResult.NoPensionApplicable;
        }
        else
        {
            var key = Tuple.Create(entry.Employment.PensionScheme.EarningsBasis, entry.Employment.PensionScheme.TaxTreatment);

            IPensionContributionCalculator? calculator;

            lock (_pensionCalculators)
            {
                if (!_pensionCalculators.TryGetValue(key, out calculator))
                {
                    calculator = _pensionCalculatorFactory.GetCalculator(entry.Employment.PensionScheme.EarningsBasis,
                            entry.Employment.PensionScheme.TaxTreatment, _payDate);

                    _pensionCalculators.Add(key, calculator);
                }
            }

            calculator.Calculate(pensionablePay, entry.PensionContributionLevels.EmployerContributionPercentage,
                entry.PensionContributionLevels.EmployeeContribution, entry.PensionContributionLevels.EmployeeContributionIsFixedAmount,
                entry.PensionContributionLevels.AvcForPeriod ?? 0.0m, entry.PensionContributionLevels.SalaryForMaternityPurposes,
                out result);
        }
    }
}