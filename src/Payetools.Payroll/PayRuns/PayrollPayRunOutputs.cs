// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;
using Payetools.Payroll.Model;
using System.Collections.Immutable;

namespace Payetools.Payroll.PayRuns;

/// <summary>
/// Represents the output of a payroll pay run, which includes the results for each employee.
/// </summary>
/// <typeparam name="TIdentifier">Identifier type for payrolls, pay runs, etc.</typeparam>
public class PayrollPayRunOutputs<TIdentifier> : IPayrollPayRunOutputs<TIdentifier>
        where TIdentifier : notnull
{
    /// <summary>
    /// Gets the unique identifier for this payrun.
    /// </summary>
    public required TIdentifier PayRunId { get; init; }

    /// <summary>
    /// Gets the <see cref="PayDate"/> for this payrun, which provides access to the pay date and the pay frequency.
    /// </summary>
    public PayDate PayDate { get; init; }

    /// <summary>
    /// Gets the start and end dates of the pay period that pertains to this payrun, in the form of a <see cref="DateRange"/>.
    /// </summary>
    public DateRange PayPeriod { get; init; }

    /// <summary>
    /// Gets the set of employee pay run outputs for this payroll pay run.
    /// </summary>
    public ImmutableArray<IEmployeePayRunOutputs<TIdentifier>> EmployeePayRunOutputs { get; init; }

    /// <summary>
    /// Gets a summary of this pay run, providing totals for all statutory payments.
    /// </summary>
    /// <param name="payRunInputs">Instance of <see cref="IPayrollPayRunInputs{TIdentifier}"/> that provides the input data for
    /// the pay run.</param>
    /// <returns><see cref="IPayRunSummary"/> instance that provides summary figures.</returns>
    public IPayRunSummary GetPayRunSummary(in IPayrollPayRunInputs<TIdentifier> payRunInputs)
    {
        var allEarnings = payRunInputs.EmployeePayRunInputs.SelectMany(pri => pri.Earnings);

        var payRunSummary = new PayRunSummary
        {
            PayDate = PayDate,
            IncomeTaxTotal = EmployeePayRunOutputs
                .Select(r => r.TaxCalculationResult.FinalTaxDue)
                .Sum(),
            StudentLoansTotal = EmployeePayRunOutputs
                .Select(r => r.StudentLoanCalculationResult?.StudentLoanDeduction ?? 0)
                .Sum(),
            PostgraduateLoansTotal = EmployeePayRunOutputs
                .Select(r => r.StudentLoanCalculationResult?.PostgraduateLoanDeduction ?? 0)
                .Sum(),
            EmployerNiTotal = EmployeePayRunOutputs
                .Select(r => r.NiCalculationResult.EmployerContribution)
                .Sum(),
            EmployeeNiTotal = EmployeePayRunOutputs
                .Select(r => r.NiCalculationResult.EmployerContribution)
                .Sum(),
            StatutoryMaternityPayTotal = allEarnings
                .Where(e => e.EarningsDetails.PaymentType == EarningsType.StatutoryMaternityPay)
                .Select(e => e.TotalEarnings)
                .Sum(),
            StatutoryPaternityPayTotal = allEarnings
                .Where(e => e.EarningsDetails.PaymentType == EarningsType.StatutoryPaternityPay)
                .Select(e => e.TotalEarnings)
                .Sum(),
            StatutoryAdoptionPayTotal = allEarnings
                .Where(e => e.EarningsDetails.PaymentType == EarningsType.StatutoryAdoptionPay)
                .Select(e => e.TotalEarnings)
                .Sum(),
            StatutorySharedParentalPayTotal = allEarnings
                .Where(e => e.EarningsDetails.PaymentType == EarningsType.StatutorySharedParentalPay)
                .Select(e => e.TotalEarnings)
                .Sum(),
            StatutoryParentalBereavementPayTotal = allEarnings
                .Where(e => e.EarningsDetails.PaymentType == EarningsType.StatutoryParentalBereavementPay)
                .Select(e => e.TotalEarnings)
                .Sum(),
            StatutoryNeonatalCarePayTotal = allEarnings
                .Where(e => e.EarningsDetails.PaymentType == EarningsType.StatutoryNeonatalCarePay)
                .Select(e => e.TotalEarnings)
                .Sum()
        };

        return payRunSummary;
    }
}