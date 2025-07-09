// Copyright (c) 2023-2025, Payetools Ltd.
//
// Payetools Ltd licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;
using System;

namespace Payetools.Payroll.Model;

/// <summary>
/// Represents the year-to-date payroll history for a given employer.
/// </summary>
public class EmployerYtdHistory : IEmployerYtdHistory
{
    private readonly IEmployerHistoryEntry[] _historyEntries = new IEmployerHistoryEntry[12];

    /// <summary>
    /// Gets the tax year that this year-to-date history is for.
    /// </summary>
    public TaxYear TaxYear { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="EmployerYtdHistory"/> class.
    /// </summary>
    /// <param name="taxYear">Tax year pertaining.</param>
    public EmployerYtdHistory(in TaxYear taxYear)
    {
        TaxYear = taxYear;
    }

    /// <summary>
    /// Applies the supplied pay run summary to the relevant month's history entry, based on pay date.
    /// </summary>
    /// <param name="payRunSummary">Pay run summary to apply.</param>
    public void Apply(in IPayRunSummary payRunSummary)
    {
        var monthNumber = TaxYear.GetMonthNumber(TaxYear, payRunSummary.PayDate);
        var index = monthNumber - 1;

        var hasExistingEntry = _historyEntries[index] != null;

        _historyEntries[index] = hasExistingEntry ?
            _historyEntries[index].Apply(payRunSummary) :
            CreateEntryFromPayRun(monthNumber, payRunSummary);
    }

    /// <summary>
    /// Undoes the previous application of the supplied pay run summary against the relevant month's history entry,
    /// based on pay date.
    /// </summary>
    /// <param name="payRunSummary">Pay run summary to un-apply.</param>
    public void UndoApply(in IPayRunSummary payRunSummary)
    {
        var monthNumber = TaxYear.GetMonthNumber(TaxYear, payRunSummary.PayDate);
        var index = monthNumber - 1;

        if (_historyEntries[index] == null)
            throw new ArgumentException($"No history for month number {monthNumber} available to undo", nameof(payRunSummary));

        _historyEntries[index] = _historyEntries[index].UndoApply(payRunSummary);
    }

    /// <summary>
    /// Gets the year-to-date history by summing up each entry from the start of the tax year up to and including
    /// the specified month number.
    /// </summary>
    /// <param name="monthNumber">Month number to sum up to.</param>
    /// <param name="ytdHistory">A new <see cref="IEmployerHistoryEntry"/> containing the summarised data.</param>
    public void GetYearToDateFigures(in int monthNumber, out IEmployerHistoryEntry ytdHistory)
    {
        var month = monthNumber;

        if (month < 1 || month > 12)
            throw new ArgumentException($"Invalid month number {month}; value must be between 1 and 12", nameof(monthNumber));

        var entries = _historyEntries.Where(e => e != null && e.MonthNumber <= month);

        ytdHistory = new EmployerHistoryEntry
        {
            MonthNumber = month,
            TotalIncomeTax = entries.Sum(e => e.TotalIncomeTax),
            TotalStudentLoans = entries.Sum(e => e.TotalStudentLoans),
            TotalPostgraduateLoans = entries.Sum(e => e.TotalPostgraduateLoans),
            EmployerNiTotal = entries.Sum(e => e.EmployerNiTotal),
            EmployeeNiTotal = entries.Sum(e => e.EmployeeNiTotal),
            TotalStatutoryMaternityPay = entries.Sum(e => e.TotalStatutoryMaternityPay),
            TotalStatutoryPaternityPay = entries.Sum(e => e.TotalStatutoryPaternityPay),
            TotalStatutoryAdoptionPay = entries.Sum(e => e.TotalStatutoryAdoptionPay),
            TotalStatutorySharedParentalPay = entries.Sum(e => e.TotalStatutorySharedParentalPay),
            TotalStatutoryParentalBereavementPay = entries.Sum(e => e.TotalStatutoryParentalBereavementPay),
            TotalStatutoryNeonatalCarePay = entries.Sum(e => e.TotalStatutoryNeonatalCarePay)
        };
    }

    private static EmployerHistoryEntry CreateEntryFromPayRun(in int monthNumber, in IPayRunSummary summary) =>
        new EmployerHistoryEntry
        {
            MonthNumber = monthNumber,
            TotalIncomeTax = summary.IncomeTaxTotal,
            TotalStudentLoans = summary.StudentLoansTotal,
            TotalPostgraduateLoans = summary.PostgraduateLoansTotal,
            EmployerNiTotal = summary.EmployerNiTotal,
            EmployeeNiTotal = summary.EmployeeNiTotal,
            TotalStatutoryMaternityPay = summary.StatutoryMaternityPayTotal,
            TotalStatutoryPaternityPay = summary.StatutoryPaternityPayTotal,
            TotalStatutoryAdoptionPay = summary.StatutoryAdoptionPayTotal,
            TotalStatutorySharedParentalPay = summary.StatutorySharedParentalPayTotal,
            TotalStatutoryParentalBereavementPay = summary.StatutoryParentalBereavementPayTotal,
            TotalStatutoryNeonatalCarePay = summary.StatutoryNeonatalCarePayTotal
        };
}