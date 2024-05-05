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

        if (_historyEntries[monthNumber - 1] == null)
        {
            _historyEntries[monthNumber - 1] = FromPayRun(payRunSummary);
        }
        else
        {
            _historyEntries[monthNumber - 1] = null!;
        }
    }

    /// <summary>
    /// Undoes the previous application of the supplied pay run summary against the relevant month's history entry,
    /// based on pay date.
    /// </summary>
    /// <param name="payRunSummary">Pay run summary to un-apply.</param>
    public void UndoApply(in IPayRunSummary payRunSummary)
    {
    }

    /// <summary>
    /// Gets the year-to-date history by summing up each entry from the start of the tax year up to and including
    /// the specified month number.
    /// </summary>
    /// <param name="monthNumber">Month number to sum up to.</param>
    /// <param name="ytdHistory"><see cref="IEmployerYtdHistoryEntry"/> containing the summarised data.</param>
    public void GetYearToDateFigures(in int monthNumber, out IEmployerYtdHistoryEntry ytdHistory)
    {
        ytdHistory = new EmployerYtdHistoryEntry();
    }

    private static IEmployerYtdHistoryEntry FromPayRun(IPayRunSummary payRunSummary) =>
        new EmployerYtdHistoryEntry
        {
        };
}
