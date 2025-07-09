// Copyright (c) 2023-2025, Payetools Ltd.
//
// Payetools Ltd licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.AttachmentOrders;
using Payetools.AttachmentOrders.Factories;
using Payetools.Common.Model;
using Payetools.IncomeTax;
using Payetools.NationalInsurance;
using Payetools.Payroll.Model;
using Payetools.Pensions;
using Payetools.Pensions.Model;
using Payetools.StudentLoans;

namespace Payetools.Payroll.PayRuns;

/// <summary>
/// Represents the calculator that can process an employee's set of input payroll data and
/// provide the results of the calculations in the form of an <see cref="IEmployeePayRunResult"/>.
/// </summary>
public class PayRunEntryProcessor : EmployeePayRunProcessor, IPayRunEntryProcessor
{
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
    public PayRunEntryProcessor(
        ITaxCalculatorFactory incomeTaxCalcFactory,
        INiCalculatorFactory niCalcFactory,
        IPensionContributionCalculatorFactory pensionCalcFactory,
        IStudentLoanCalculatorFactory studentLoanCalcFactory,
        IAttachmentOrdersCalculatorFactory attachmentOrdersCalculatorFactory,
        PayDate payDate,
        DateRange payPeriod)
        : base(incomeTaxCalcFactory, niCalcFactory, pensionCalcFactory, studentLoanCalcFactory, attachmentOrdersCalculatorFactory, payDate, payPeriod)
    {
    }

    /// <summary>
    /// Processes the supplied payrun entry calculating all the earnings and deductions, income tax, national insurance and
    /// other statutory deductions, and generating a result structure which includes the final net pay.
    /// </summary>
    /// <param name="entry">Instance of <see cref="IEmployeePayRunInputEntry"/> containing all the necessary input data for the
    /// payroll calculation.</param>
    /// <param name="result">An instance of <see cref="IEmployeePayRunResult"/> containing the results of the payroll calculations.</param>
    [Obsolete("Use IPayRunEntryProcessor.Process(IEmployeePayRunInputs, IEmployeePayRunOutputs) instead. Scheduled for removal in v3.0.0.", false)]
    public void Process(in IEmployeePayRunInputEntry entry, out IEmployeePayRunResult result)
    {
        var inputs = FromPayRunEntry<object>(entry);

        Process(inputs, out var interimResult);

        result = new EmployeePayRunResult(entry.Employment,
            ref interimResult.TaxCalculationResult,
            ref interimResult.NiCalculationResult,
            ref interimResult.StudentLoanCalculationResult,
            ref interimResult.PensionContributionCalculationResult,
            ref interimResult.AttachmentOfEarningsCalculationResult,
            interimResult.TotalGrossPay,
            interimResult.WorkingGrossPay,
            interimResult.TaxablePay,
            interimResult.NicablePay,
            interimResult.PayrollBenefitsInPeriod,
            0.0m,
            ref entry.Employment.PayrollHistoryYtd,
            entry.IsLeaverInThisPayRun);
    }

    [Obsolete]
    private static EmployeePayRunInputs<TIdentifier> FromPayRunEntry<TIdentifier>(IEmployeePayRunInputEntry entry)
        where TIdentifier : notnull
    {
        ArgumentNullException.ThrowIfNull(entry);

        var employment = entry.Employment;

        return new EmployeePayRunInputs<TIdentifier>(
            ConvertToIdentifier<TIdentifier>(entry.Employment.PayrollId),
            employment.TaxCode,
            employment.NiCategory,
            MakeDirectorInfo(employment),
            employment.StudentLoanInfo,
            entry.Earnings,
            entry.Deductions,
            entry.PayrolledBenefits,
            entry.AttachmentOfEarningsOrders,
            MakePensionContributions(employment, entry.PensionContributionLevels),
            employment.PayrollHistoryYtd);
    }

    private static DirectorInfo? MakeDirectorInfo(IEmployment employment) =>
        employment.IsDirector ? new DirectorInfo(
                employment.IsDirector,
                employment.DirectorsNiCalculationMethod,
                employment.DirectorsAppointmentDate,
                employment.CeasedToBeDirectorDate) : null;

    private static PensionContributions? MakePensionContributions(
        IEmployment employment,
        IPensionContributionLevels? levels)
    {
        if (employment.PensionScheme is null || levels is null)
            return null;

        var scheme = employment.PensionScheme;

        return new PensionContributions(
            scheme.EarningsBasis,
            scheme.TaxTreatment,
            levels.EmployeeContribution,
            levels.EmployeeContributionIsFixedAmount,
            levels.EmployerContribution,
            levels.EmployerContributionIsFixedAmount,
            levels.SalaryExchangeApplied,
            levels.EmployersNiReinvestmentPercentage,
            levels.AvcForPeriod,
            levels.SalaryForMaternityPurposes);
    }

    private static TIdentifier ConvertToIdentifier<TIdentifier>(PayrollId payrollId)
        where TIdentifier : notnull
    {
        ArgumentNullException.ThrowIfNull(payrollId);

        return (TIdentifier)Convert.ChangeType(payrollId.ToString(), typeof(TIdentifier));
    }
}