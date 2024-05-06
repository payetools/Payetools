// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;

namespace Payetools.Payroll.Model;

/// <summary>
/// Represents the year-to-date payroll history for a given employer.
/// </summary>
public class EmployerYtdHistory : IEmployerYtdHistory
{
    private readonly IEmployerYtdHistoryEntry[] _historyEntries = new IEmployerYtdHistoryEntry[12];

    /// <summary>
    /// Gets the tax year that this year-to-date history is for.
    /// </summary>
    public TaxYear TaxYear { get; }

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
            EmployerYtdHistoryEntry.FromPayRun(monthNumber, payRunSummary);
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
    /// <param name="ytdHistory">A new <see cref="IEmployerYtdHistoryEntry"/> containing the summarised data.</param>
    public void GetYearToDateFigures(in int monthNumber, out IEmployerYtdHistoryEntry ytdHistory)
    {
        if (monthNumber < 1 || monthNumber > 12)
            throw new ArgumentException($"Invalid month number {monthNumber}; value must be between 1 and 12", nameof(monthNumber));

        var month = monthNumber;

        var entries = _historyEntries.Where(e => e.MonthNumber <= month);

        ytdHistory = new EmployerYtdHistoryEntry
        {
            MonthNumber = monthNumber,
            TotalIncomeTax = entries.Sum(e => e.TotalIncomeTax),
            TotalStudentLoans = entries.Sum(e => e.TotalStudentLoans),
            TotalPostgraduateLoans = entries.Sum(e => e.TotalPostgraduateLoans),
            EmployerNiTotal = entries.Sum(e => e.EmployerNiTotal),
            EmployeeNiTotal = entries.Sum(e => e.EmployeeNiTotal),
            TotalYtdSMP = entries.Sum(e => e.TotalYtdSMP),
            TotalYtdSPP = entries.Sum(e => e.TotalYtdSPP),
            TotalYtdSAP = entries.Sum(e => e.TotalYtdSAP),
            TotalYtdShPP = entries.Sum(e => e.TotalYtdShPP),
            TotalYtdSPBP = entries.Sum(e => e.TotalYtdSPBP)
        };
    }
}