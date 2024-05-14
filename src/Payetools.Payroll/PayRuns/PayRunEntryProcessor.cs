// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;
using Payetools.IncomeTax;
using Payetools.NationalInsurance;
using Payetools.NationalInsurance.Model;
using Payetools.Payroll.Model;
using Payetools.Pensions;
using Payetools.Pensions.Model;
using Payetools.Statutory.AttachmentOfEarnings;
using Payetools.StudentLoans;
using Payetools.StudentLoans.Model;
using System.Collections.Concurrent;

namespace Payetools.Payroll.PayRuns;

/// <summary>
/// Represents the calculator that can process an employee's set of input payroll data and
/// provide the results of the calculations in the form of an <see cref="IEmployeePayRunResult"/>.
/// </summary>
public class PayRunEntryProcessor : IPayRunEntryProcessor
{
    internal readonly struct EarningsTotals
    {
        public decimal GrossPay { get; init; }

        public decimal WorkingGrossPay { get; init; }

        public decimal TaxablePay { get; init; }

        public decimal NicablePay { get; init; }

        public decimal PensionablePay { get; init; }

        public decimal? BenefitsInKind { get; init; }
    }

    private readonly Dictionary<CountriesForTaxPurposes, ITaxCalculator> _incomeTaxCalculators;
    private readonly INiCalculator _niCalculator;
    private readonly IPensionContributionCalculatorFactory _pensionCalculatorFactory;
    private readonly IStudentLoanCalculator _studentLoanCalculator;
    private readonly IAttachmentOfEarningsCalculatorFactory _attachmentOfEarningsCalculatorFactory;
    private readonly ConcurrentDictionary<(PensionsEarningsBasis, PensionTaxTreatment), IPensionContributionCalculator> _pensionCalculators;
    private readonly ConcurrentDictionary<AttachmentOfEarningsType, IAttachmentOfEarningsCalculator> _attachmentOfEarningsCalculators;

    /// <summary>
    /// Gets the pay date for this payrun calculator.
    /// </summary>
    public PayDate PayDate { get; }

    /// <summary>
    /// Gets the pay period for this payrun calculator.
    /// </summary>
    public DateRange PayPeriod { get; }

    /// <summary>
    /// Initialises a new instance of <see cref="PayRunEntryProcessor"/> with the supplied factories
    /// and specified pay date.
    /// </summary>
    /// <param name="incomeTaxCalcFactory">Income tax calculator factory.</param>
    /// <param name="niCalcFactory">calculator factory.</param>
    /// <param name="pensionCalcFactory">Pension contributions calculator factory.</param>
    /// <param name="studentLoanCalcFactory">Student loan calculator factory.</param>
    /// <param name="attachmentOfEarningsCalculatorFactory">Attachment of earnings order calculators.</param>
    /// <param name="payDate">Pay date for this payrun.</param>
    /// <param name="payPeriod">Applicable pay period for this calculator.</param>
    public PayRunEntryProcessor(
        ITaxCalculatorFactory incomeTaxCalcFactory,
        INiCalculatorFactory niCalcFactory,
        IPensionContributionCalculatorFactory pensionCalcFactory,
        IStudentLoanCalculatorFactory studentLoanCalcFactory,
        IAttachmentOfEarningsCalculatorFactory attachmentOfEarningsCalculatorFactory,
        PayDate payDate,
        DateRange payPeriod)
    {
        _incomeTaxCalculators = payDate.TaxYear.GetCountriesForYear()
            .Select(regime => (regime, calculator: incomeTaxCalcFactory.GetCalculator(regime, payDate)))
            .ToDictionary(kv => kv.regime, kv => kv.calculator);
        _niCalculator = niCalcFactory.GetCalculator(payDate);
        _pensionCalculatorFactory = pensionCalcFactory;
        _studentLoanCalculator = studentLoanCalcFactory.GetCalculator(payDate);
        _attachmentOfEarningsCalculatorFactory = attachmentOfEarningsCalculatorFactory;
        PayDate = payDate;
        PayPeriod = payPeriod;

        _pensionCalculators = new ConcurrentDictionary<(PensionsEarningsBasis, PensionTaxTreatment), IPensionContributionCalculator>();
        _attachmentOfEarningsCalculators = new ConcurrentDictionary<AttachmentOfEarningsType, IAttachmentOfEarningsCalculator>();
    }

    /// <summary>
    /// Processes the supplied payrun entry calculating all the earnings and deductions, income tax, national insurance and
    /// other statutory deductions, and generating a result structure which includes the final net pay.
    /// </summary>
    /// <param name="entry">Instance of <see cref="IEmployeePayRunInputEntry"/> containing all the necessary input data for the
    /// payroll calculation.</param>
    /// <param name="result">An instance of <see cref="IEmployeePayRunResult"/> containing the results of the payroll calculations.</param>
    public void Process(IEmployeePayRunInputEntry entry, out IEmployeePayRunResult result)
    {
        GetAllEarningsTypes(entry, out var earningsTotals);

        decimal employersNiSavings = 0.0m;
        var workingGrossPay = earningsTotals.WorkingGrossPay;
        var taxablePay = earningsTotals.TaxablePay;
        var nicablePay = earningsTotals.NicablePay;
        IPensionContributionCalculationResult? pensionContributions = null;

        // Calculate National Insurance first in case it is needed for salary sacrifice
        CalculateNiContributions(entry, nicablePay, out INiCalculationResult niCalculationResult);

        if (entry.Employment.PensionScheme != null && entry.PensionContributionLevels != null)
        {
            var key = (entry.Employment.PensionScheme.EarningsBasis, entry.Employment.PensionScheme.TaxTreatment);

            var calculator = GetCalculator(
                _pensionCalculators,
                key,
                () => _pensionCalculatorFactory.GetCalculator(key.EarningsBasis, key.TaxTreatment, PayDate));

            if (entry.PensionContributionLevels.SalaryExchangeApplied)
            {
                var salaryExchangedAmount = calculator.GetSalaryExchangedAmount(earningsTotals.PensionablePay,
                    entry.PensionContributionLevels.EmployeeContribution, entry.PensionContributionLevels.EmployeeContributionIsFixedAmount);

                var originalEmployersNi = niCalculationResult.EmployerContribution;

                nicablePay -= salaryExchangedAmount;
                workingGrossPay -= salaryExchangedAmount;
                taxablePay -= salaryExchangedAmount;

                // Have to recalculate the NI contributions based on the adjusted salary
                CalculateNiContributions(entry, nicablePay, out niCalculationResult);

                employersNiSavings = originalEmployersNi - niCalculationResult.EmployerContribution;
            }

            // In order to process net pay arrangement pensions or salary exchange correctly, we must first
            // calculate the pension contributions, so we can apply the appropriate impacts on working gross
            // taxable salary figures.
            CalculatePensionContributions(ref entry, earningsTotals.PensionablePay, employersNiSavings,
                out pensionContributions);

            if (entry.PensionContributionLevels?.SalaryExchangeApplied == false &&
                entry.Employment.PensionScheme?.TaxTreatment == PensionTaxTreatment.NetPayArrangement)
            {
                workingGrossPay -= pensionContributions.CalculatedEmployeeContributionAmount;
                taxablePay -= pensionContributions.CalculatedEmployeeContributionAmount;
            }
        }

        if (!_incomeTaxCalculators.TryGetValue(entry.Employment.TaxCode.ApplicableCountries, out var taxCalculator))
            throw new InvalidOperationException($"Unable to perform tax calculation as calculator for tax regime '{entry.Employment.TaxCode.TaxRegimeLetter}' is not available");

        taxCalculator.Calculate(
            taxablePay,
            earningsTotals.BenefitsInKind ?? 0.0m,
            entry.Employment.TaxCode,
            entry.Employment.PayrollHistoryYtd.TaxablePayYtd,
            entry.Employment.PayrollHistoryYtd.TaxPaidYtd,
            entry.Employment.PayrollHistoryYtd.TaxUnpaidDueToRegulatoryLimit,
            out var taxCalculationResult);

        IStudentLoanCalculationResult? studentLoanCalculationResult = null;

        if (entry.Employment.StudentLoanInfo != null)
            _studentLoanCalculator.Calculate(earningsTotals.GrossPay, entry.Employment.StudentLoanInfo?.StudentLoanType,
                entry.Employment.StudentLoanInfo?.HasPostgraduateLoan == true, out studentLoanCalculationResult);

        IAttachmentOfEarningsCalculationResult? attachmentOfEarningsCalculationResult = null;

        result = new EmployeePayRunResult(
            entry.Employment,
            ref taxCalculationResult,
            ref niCalculationResult,
            ref studentLoanCalculationResult,
            ref pensionContributions,
            ref attachmentOfEarningsCalculationResult,
            earningsTotals.GrossPay,
            workingGrossPay,
            taxablePay,
            nicablePay,
            earningsTotals.BenefitsInKind,
            0.0m, // otherDeductions,
            ref entry.Employment.PayrollHistoryYtd,
            entry.IsLeaverInThisPayRun,
            entry.IsPaymentAfterLeaving);
    }

    private static void GetAllEarningsTypes(in IEmployeePayRunInputEntry entry, out EarningsTotals earningsTotals)
    {
        // The distinction between gross pay and working gross pay is that the former is the sum of all
        // earned income for the period, whereas the latter is that figure less any pre-tax deductions,
        // for example, salary exchange arrangements.
        decimal grossPay = 0.0m;
        decimal taxablePay = 0.0m;
        decimal nicablePay = 0.0m;
        decimal pensionablePay = 0.0m;
        decimal benefitsInKind = 0.0m;

        foreach (var e in entry.Earnings)
        {
            grossPay += e.TotalEarnings;
            taxablePay += e.EarningsDetails.IsSubjectToTax ? e.TotalEarnings : 0.0m;
            nicablePay += e.EarningsDetails.IsSubjectToNi ? e.TotalEarnings : 0.0m;
            pensionablePay += e.EarningsDetails.IsPensionable ? e.TotalEarnings : 0.0m;
        }

        decimal workingGrossPay = grossPay;

        foreach (var d in entry.Deductions)
        {
            taxablePay -= d.DeductionClassification.ReducesTaxablePay ? d.TotalDeduction : 0.0m;
            nicablePay -= d.DeductionClassification.ReducesNicablePay ? d.TotalDeduction : 0.0m;
            pensionablePay -= d.DeductionClassification.ReducesPensionablePay ? d.TotalDeduction : 0.0m;
            workingGrossPay -= d.DeductionClassification.ReducesGrossPay ? d.TotalDeduction : 0.0m;
        }

        foreach (var b in entry.PayrolledBenefits)
        {
            taxablePay += b.AmountForPeriod;
            benefitsInKind += b.AmountForPeriod;
        }

        earningsTotals = new EarningsTotals()
        {
            GrossPay = decimal.Round(grossPay, 2, MidpointRounding.AwayFromZero),
            WorkingGrossPay = decimal.Round(workingGrossPay, 2, MidpointRounding.AwayFromZero),
            TaxablePay = decimal.Round(taxablePay, 2, MidpointRounding.AwayFromZero),
            NicablePay = decimal.Round(nicablePay, 2, MidpointRounding.AwayFromZero),
            PensionablePay = decimal.Round(pensionablePay, 2, MidpointRounding.AwayFromZero),
            BenefitsInKind = benefitsInKind != 0 ? decimal.Round(benefitsInKind, 2, MidpointRounding.AwayFromZero) : null
        };
    }

    private void CalculateNiContributions(in IEmployeePayRunInputEntry entry, decimal nicablePay, out INiCalculationResult result)
    {
        if (entry.Employment.IsDirector)
        {
            var histories = entry.Employment.PayrollHistoryYtd.EmployeeNiHistoryEntries;

            var (employeesNiPaidYtd, employersNiPaidYtd) = histories != null ? histories.GetNiYtdTotals() : (0.0m, 0.0m);

            _niCalculator.CalculateDirectors(entry.Employment.DirectorsNiCalculationMethod ?? DirectorsNiCalculationMethod.StandardAnnualisedEarningsMethod,
                entry.Employment.NiCategory, nicablePay, nicablePay + entry.Employment.PayrollHistoryYtd.NicablePayYtd,
                employeesNiPaidYtd, employersNiPaidYtd, null, out result);
        }
        else
        {
            _niCalculator.Calculate(entry.Employment.NiCategory, nicablePay, out result);
        }
    }

    private void CalculatePensionContributions(ref IEmployeePayRunInputEntry entry, decimal pensionablePay, decimal employersNiSavings,
        out IPensionContributionCalculationResult result)
    {
        if (entry.Employment.PensionScheme == null || entry.PensionContributionLevels == null)
        {
            result = PensionContributionCalculationResult.NoPensionApplicable;
        }
        else
        {
            var key = (entry.Employment.PensionScheme.EarningsBasis, entry.Employment.PensionScheme.TaxTreatment);

            var calculator = GetCalculator(
                _pensionCalculators,
                key,
                () => _pensionCalculatorFactory.GetCalculator(key.EarningsBasis, key.TaxTreatment, PayDate));

            if (entry.PensionContributionLevels.SalaryExchangeApplied)
            {
                calculator.CalculateUnderSalaryExchange(
                    pensionablePay,
                    entry.PensionContributionLevels.EmployerContribution,
                    entry.PensionContributionLevels.EmployerContributionIsFixedAmount,
                    employersNiSavings,
                    entry.PensionContributionLevels.EmployersNiReinvestmentPercentage ?? 0.0m,
                    entry.PensionContributionLevels.EmployeeContribution,
                    entry.PensionContributionLevels.EmployeeContributionIsFixedAmount,
                    entry.PensionContributionLevels.AvcForPeriod ?? 0.0m,
                    entry.PensionContributionLevels.SalaryForMaternityPurposes,
                    out result);
            }
            else
            {
                calculator.Calculate(
                    pensionablePay,
                    entry.PensionContributionLevels.EmployerContribution,
                    entry.PensionContributionLevels.EmployerContributionIsFixedAmount,
                    entry.PensionContributionLevels.EmployeeContribution,
                    entry.PensionContributionLevels.EmployeeContributionIsFixedAmount,
                    entry.PensionContributionLevels.AvcForPeriod ?? 0.0m,
                    entry.PensionContributionLevels.SalaryForMaternityPurposes,
                    out result);
            }
        }
    }

    private static TCalculator GetCalculator<TKey, TCalculator>(
        ConcurrentDictionary<TKey, TCalculator> dictionary,
        TKey key,
        Func<TCalculator> calculatorFactoryFunction)
        where TKey : notnull
        where TCalculator : class
    {
        if (!dictionary.TryGetValue(key, out TCalculator? calculator))
            dictionary.TryAdd(key, calculator = calculatorFactoryFunction());

        return calculator;
    }
}