// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.AttachmentOrders;
using Payetools.AttachmentOrders.Factories;
using Payetools.AttachmentOrders.Model;
using Payetools.Common.Model;
using Payetools.IncomeTax;
using Payetools.NationalInsurance;
using Payetools.NationalInsurance.Model;
using Payetools.Payroll.Model;
using Payetools.Pensions;
using Payetools.Pensions.Model;
using Payetools.StudentLoans;
using Payetools.StudentLoans.Model;
using System.Collections.Concurrent;

namespace Payetools.Payroll.PayRuns;

/// <summary>
/// Represents the calculator that can process an employee's set of input payroll data and
/// provide the results of the calculations in the form of an <see cref="IEmployeePayRunResult"/>.
/// </summary>
public abstract class EmployeePayRunProcessor : IEmployeePayRunProcessor
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
    private readonly ConcurrentDictionary<(PensionsEarningsBasis, PensionTaxTreatment), IPensionContributionCalculator> _pensionCalculators;
    private readonly IAttachmentOrdersCalculator _attachmentOrdersCalculator;

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
    /// <param name="attachmentOrdersCalculatorFactory">Attachment orders calculator factory.</param>
    /// <param name="payDate">Pay date for this payrun.</param>
    /// <param name="payPeriod">Applicable pay period for this calculator.</param>
    public EmployeePayRunProcessor(
        in ITaxCalculatorFactory incomeTaxCalcFactory,
        in INiCalculatorFactory niCalcFactory,
        in IPensionContributionCalculatorFactory pensionCalcFactory,
        in IStudentLoanCalculatorFactory studentLoanCalcFactory,
        in IAttachmentOrdersCalculatorFactory attachmentOrdersCalculatorFactory,
        PayDate payDate,
        in DateRange payPeriod)
    {
        var taxFactory = incomeTaxCalcFactory;
        PayDate = payDate;
        PayPeriod = payPeriod;

        _incomeTaxCalculators = payDate.TaxYear.GetCountriesForYear()
            .Select(regime => (regime, calculator: taxFactory.GetCalculator(regime, PayDate)))
            .ToDictionary(kv => kv.regime, kv => kv.calculator);
        _niCalculator = niCalcFactory.GetCalculator(payDate);
        _pensionCalculatorFactory = pensionCalcFactory;
        _studentLoanCalculator = studentLoanCalcFactory.GetCalculator(payDate);
        _attachmentOrdersCalculator = attachmentOrdersCalculatorFactory.GetCalculator(payDate);

        _pensionCalculators = new ConcurrentDictionary<(PensionsEarningsBasis, PensionTaxTreatment), IPensionContributionCalculator>();
    }

    /// <summary>
    /// Processes the supplied payrun entry calculating all the earnings and deductions, income tax, national insurance and
    /// other statutory deductions, and generating a result structure which includes the final net pay.
    /// </summary>
    /// <param name="payRunInputs">Instance of <see cref="IEmployeePayRunInputs{TIdentifier}"/> containing all the necessary input data for the
    /// payroll calculation.</param>
    /// <param name="result">An instance of <see cref="IEmployeePayRunOutputs{TIdentifier}"/> containing the results of the payroll calculations.</param>
    /// <typeparam name="TIdentifier">Identifier type for payrolls, pay runs, etc.</typeparam>
    public void Process<TIdentifier>(in IEmployeePayRunInputs<TIdentifier> payRunInputs, out IEmployeePayRunOutputs<TIdentifier> result)
        where TIdentifier : notnull
    {
        GetAllEarningsTypes(payRunInputs.Earnings, payRunInputs.PayrolledBenefits, payRunInputs.Deductions, out var earningsTotals);

        decimal employersNiSavings = 0.0m;
        var workingGrossPay = earningsTotals.WorkingGrossPay;
        var taxablePay = earningsTotals.TaxablePay;
        var nicablePay = earningsTotals.NicablePay;
        IPensionContributionCalculationResult? pensionContributions = null;

        // Calculate National Insurance first in case it is needed for salary sacrifice
        CalculateNiContributions(
            payRunInputs.NiCategory,
            payRunInputs.DirectorInfo,
            payRunInputs.YtdFigures,
            nicablePay,
            out INiCalculationResult niCalculationResult);

        if (payRunInputs.PensionContributions is IPensionContributions pensionContribs)
        {
            var key = (pensionContribs.EarningsBasis, pensionContribs.TaxTreatment);

            var calculator = GetCalculator(
                _pensionCalculators,
                key,
                () => _pensionCalculatorFactory.GetCalculator(key.EarningsBasis, key.TaxTreatment, PayDate));

            if (pensionContribs.SalaryExchangeApplied)
            {
                var salaryExchangedAmount = calculator.GetSalaryExchangedAmount(earningsTotals.PensionablePay,
                    pensionContribs.EmployeeContribution, pensionContribs.EmployeeContributionIsFixedAmount);

                var originalEmployersNi = niCalculationResult.EmployerContribution;

                nicablePay -= salaryExchangedAmount;
                workingGrossPay -= salaryExchangedAmount;
                taxablePay -= salaryExchangedAmount;

                // Have to recalculate the NI contributions based on the adjusted salary
                CalculateNiContributions(
                    payRunInputs.NiCategory,
                    payRunInputs.DirectorInfo,
                    payRunInputs.YtdFigures,
                    nicablePay,
                    out niCalculationResult);

                employersNiSavings = originalEmployersNi - niCalculationResult.EmployerContribution;
            }

            // In order to process net pay arrangement pensions or salary exchange correctly, we must first
            // calculate the pension contributions, so we can apply the appropriate impacts on working gross
            // taxable salary figures.
            CalculatePensionContributions(pensionContribs, earningsTotals.PensionablePay, employersNiSavings,
                out pensionContributions);

            if (!pensionContribs.SalaryExchangeApplied &&
                pensionContribs.TaxTreatment == PensionTaxTreatment.NetPayArrangement)
            {
                workingGrossPay -= pensionContributions.CalculatedEmployeeContributionAmount;
                taxablePay -= pensionContributions.CalculatedEmployeeContributionAmount;
            }
        }

        if (!_incomeTaxCalculators.TryGetValue(payRunInputs.TaxCode.ApplicableCountries, out var taxCalculator))
            throw new InvalidOperationException($"Unable to perform tax calculation as calculator for tax regime '{payRunInputs.TaxCode.TaxRegimeLetter}' is not available");

        taxCalculator.Calculate(
            taxablePay,
            earningsTotals.BenefitsInKind ?? 0.0m,
            payRunInputs.TaxCode,
            payRunInputs.YtdFigures.TaxablePayYtd,
            payRunInputs.YtdFigures.TaxPaidYtd,
            payRunInputs.YtdFigures.TaxUnpaidDueToRegulatoryLimit,
            out var taxCalculationResult);

        IStudentLoanCalculationResult? studentLoanCalculationResult = null;

        if (payRunInputs.StudentLoanInfo is IStudentLoanInfo studentLoan)
        {
            _studentLoanCalculator.Calculate(
                earningsTotals.GrossPay,
                studentLoan.StudentLoanType,
                studentLoan.HasPostgraduateLoan,
                out studentLoanCalculationResult);
        }

        IAttachmentOrderCalculationResult? attachmentOfEarningsCalculationResult = null;

        if (payRunInputs.AttachmentOrders is not null && payRunInputs.AttachmentOrders.Any())
        {
            _attachmentOrdersCalculator.Calculate(
                payRunInputs.AttachmentOrders,
                payRunInputs.Earnings,
                payRunInputs.Deductions,
                PayPeriod,
                taxCalculationResult.FinalTaxDue,
                niCalculationResult.EmployeeContribution,
                studentLoanCalculationResult,
                pensionContributions?.CalculatedEmployeeContributionAmount ?? 0.0m,
                out attachmentOfEarningsCalculationResult);

            // TODO: Student loan deductions / protected earnings
        }

        result = new EmployeePayRunOutputs<TIdentifier>(
            payRunInputs.EmployeeId,
            ref taxCalculationResult,
            ref niCalculationResult,
            ref studentLoanCalculationResult,
            ref pensionContributions,
            ref attachmentOfEarningsCalculationResult,
            earningsTotals.GrossPay,
            workingGrossPay,
            taxablePay,
            nicablePay,
            earningsTotals.BenefitsInKind);
    }

    private static void GetAllEarningsTypes(
        in IEnumerable<IEarningsEntry> earnings,
        in IEnumerable<IPayrolledBenefitForPeriod> payrolledBenefits,
        in IEnumerable<IDeductionEntry> deductions,
        out EarningsTotals earningsTotals)
    {
        // The distinction between gross pay and working gross pay is that the former is the sum of all
        // earned income for the period, whereas the latter is that figure less any pre-tax deductions,
        // for example, salary exchange arrangements.
        decimal grossPay = 0.0m;
        decimal taxablePay = 0.0m;
        decimal nicablePay = 0.0m;
        decimal pensionablePay = 0.0m;
        decimal benefitsInKind = 0.0m;

        foreach (var e in earnings)
        {
            grossPay += e.TotalEarnings;
            taxablePay += e.EarningsDetails.IsSubjectToTax ? e.TotalEarnings : 0.0m;
            nicablePay += e.EarningsDetails.IsSubjectToNi ? e.TotalEarnings : 0.0m;
            pensionablePay += e.EarningsDetails.IsPensionable ? e.TotalEarnings : 0.0m;
        }

        decimal workingGrossPay = grossPay;

        foreach (var d in deductions)
        {
            taxablePay -= d.DeductionClassification.ReducesTaxablePay ? d.TotalDeduction : 0.0m;
            nicablePay -= d.DeductionClassification.ReducesNicablePay ? d.TotalDeduction : 0.0m;
            pensionablePay -= d.DeductionClassification.ReducesPensionablePay ? d.TotalDeduction : 0.0m;
            workingGrossPay -= d.DeductionClassification.ReducesGrossPay ? d.TotalDeduction : 0.0m;
        }

        foreach (var b in payrolledBenefits)
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

    private void CalculateNiContributions(
        in NiCategory niCategory,
        in IDirectorInfo? directorInfo,
        in IEmployeeCoreYtdFigures employeeYtdFigures,
        in decimal nicablePay,
        out INiCalculationResult result)
    {
        if (directorInfo != null && directorInfo.IsDirector)
        {
            var niHistories = employeeYtdFigures.NiHistory;

            var (employeesNiPaidYtd, employersNiPaidYtd) = niHistories != null ? niHistories.GetNiYtdTotals() : (0.0m, 0.0m);

            _niCalculator.CalculateDirectors(directorInfo.DirectorsNiCalculationMethod ?? DirectorsNiCalculationMethod.StandardAnnualisedEarningsMethod,
                niCategory, nicablePay, nicablePay + employeeYtdFigures.NicablePayYtd,
                employeesNiPaidYtd, employersNiPaidYtd, null, out result);
        }
        else
        {
            _niCalculator.Calculate(niCategory, nicablePay, out result);
        }
    }

    private void CalculatePensionContributions(
        in IPensionContributions? pensionContributions,
        in decimal pensionablePay,
        in decimal employersNiSavings,
        out IPensionContributionCalculationResult result)
    {
        if (pensionContributions is not IPensionContributions pensionContribs)
        {
            result = PensionContributionCalculationResult.NoPensionApplicable;
        }
        else
        {
            var key = (pensionContribs.EarningsBasis, pensionContribs.TaxTreatment);

            var calculator = GetCalculator(
                _pensionCalculators,
                key,
                () => _pensionCalculatorFactory.GetCalculator(key.EarningsBasis, key.TaxTreatment, PayDate));

            if (pensionContribs.SalaryExchangeApplied)
            {
                calculator.CalculateUnderSalaryExchange(
                    pensionablePay,
                    pensionContribs.EmployerContribution,
                    pensionContribs.EmployerContributionIsFixedAmount,
                    employersNiSavings,
                    pensionContribs.EmployersNiReinvestmentPercentage ?? 0.0m,
                    pensionContribs.EmployeeContribution,
                    pensionContribs.EmployeeContributionIsFixedAmount,
                    pensionContribs.AvcForPeriod ?? 0.0m,
                    pensionContribs.SalaryForMaternityPurposes,
                    out result);
            }
            else
            {
                calculator.Calculate(
                    pensionablePay,
                    pensionContribs.EmployerContribution,
                    pensionContribs.EmployerContributionIsFixedAmount,
                    pensionContribs.EmployeeContribution,
                    pensionContribs.EmployeeContributionIsFixedAmount,
                    pensionContribs.AvcForPeriod ?? 0.0m,
                    pensionContribs.SalaryForMaternityPurposes,
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