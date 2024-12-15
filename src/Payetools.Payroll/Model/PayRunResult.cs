﻿// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;
using System.Collections.Immutable;

namespace Payetools.Payroll.Model;

/// <summary>
/// Represents the output of a payrun.
/// </summary>
public class PayRunResult : IPayRunResult
{
    private readonly IEnumerable<IEmployeePayRunInputEntry> _payRunInputs;

    /// <summary>
    /// Gets the employer that this payrun refers to.
    /// </summary>
    public IEmployer Employer { get; init; }

    /// <summary>
    /// Gets the pay date for this payrun.
    /// </summary>
    public IPayRunDetails PayRunDetails { get; init; }

    /// <summary>
    /// Gets the list of employee payrun entries.
    /// </summary>
    public ImmutableArray<IEmployeePayRunResult> EmployeePayRunResults { get; init; }

    /// <summary>
    /// Initializes a new instance of the <see cref="PayRunResult"/> class.
    /// </summary>
    /// <param name="employer">Employer this pay run refers to.</param>
    /// <param name="payRunDetails">Pay date and pay period.</param>
    /// <param name="payRunInputs">Pay run inputs per employee.</param>
    /// <param name="employeePayRunResults">Employee pay run results.</param>
    public PayRunResult(
        IEmployer employer,
        IPayRunDetails payRunDetails,
        IEnumerable<IEmployeePayRunInputEntry> payRunInputs,
        ImmutableArray<IEmployeePayRunResult> employeePayRunResults)
    {
        Employer = employer;
        PayRunDetails = payRunDetails;
        _payRunInputs = payRunInputs;
        EmployeePayRunResults = employeePayRunResults;
    }

    /// <summary>
    /// Gets a summary of this pay run, providing totals for all statutory payments.
    /// </summary>
    /// <param name="payRunSummary"><see cref="IPayRunSummary"/> instance that provides summary figures.</param>
    public void GetPayRunSummary(out IPayRunSummary payRunSummary)
    {
        var allEarnings = _payRunInputs.SelectMany(pri => pri.Earnings);

        payRunSummary = new PayRunSummary
        {
            PayDate = PayRunDetails.PayDate,
            IncomeTaxTotal = EmployeePayRunResults
                .Select(r => r.TaxCalculationResult.FinalTaxDue)
                .Sum(),
            StudentLoansTotal = EmployeePayRunResults
                .Select(r => r.StudentLoanCalculationResult?.StudentLoanDeduction ?? 0)
                .Sum(),
            PostgraduateLoansTotal = EmployeePayRunResults
                .Select(r => r.StudentLoanCalculationResult?.PostgraduateLoanDeduction ?? 0)
                .Sum(),
            EmployerNiTotal = EmployeePayRunResults
                .Select(r => r.NiCalculationResult.EmployerContribution)
                .Sum(),
            EmployeeNiTotal = EmployeePayRunResults
                .Select(r => r.NiCalculationResult.EmployerContribution)
                .Sum(),
            StatutoryMaternityPayTotal = allEarnings
                .Where(e => e.EarningsDetails.PaymentType == PaymentType.StatutoryMaternityPay)
                .Select(e => e.TotalEarnings)
                .Sum(),
            StatutoryPaternityPayTotal = allEarnings
                .Where(e => e.EarningsDetails.PaymentType == PaymentType.StatutoryPaternityPay)
                .Select(e => e.TotalEarnings)
                .Sum(),
            StatutoryAdoptionPayTotal = allEarnings
                .Where(e => e.EarningsDetails.PaymentType == PaymentType.StatutoryAdoptionPay)
                .Select(e => e.TotalEarnings)
                .Sum(),
            StatutorySharedParentalPayTotal = allEarnings
                .Where(e => e.EarningsDetails.PaymentType == PaymentType.StatutorySharedParentalPay)
                .Select(e => e.TotalEarnings)
                .Sum(),
            StatutoryParentalBereavementPayTotal = allEarnings
                .Where(e => e.EarningsDetails.PaymentType == PaymentType.StatutoryParentalBereavementPay)
                .Select(e => e.TotalEarnings)
                .Sum(),
            StatutoryNeonatalCarePayTotal = allEarnings
                .Where(e => e.EarningsDetails.PaymentType == PaymentType.StatutoryNeonatalCarePay)
                .Select(e => e.TotalEarnings)
                .Sum()
        };
    }
}