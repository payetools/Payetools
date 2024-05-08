// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;

namespace Payetools.Payroll.Model;

/// <summary>
/// Interface that represents the year-to-date payroll history for a given employer.
/// </summary>
public interface IEmployerYtdHistory
{
    /// <summary>
    /// Gets the tax year that this year-to-date history is for.
    /// </summary>
    TaxYear TaxYear { get; }

    /// <summary>
    /// Applies the supplied pay run summary to the relevant month's history entry, based on pay date.
    /// </summary>
    /// <param name="payRunSummary">Pay run summary to apply.</param>
    void Apply(in IPayRunSummary payRunSummary);

    /// <summary>
    /// Undoes the previous application of the supplied pay run summary against the relevant month's history entry,
    /// based on pay date.
    /// </summary>
    /// <param name="payRunSummary">Pay run summary to un-apply.</param>
    void UndoApply(in IPayRunSummary payRunSummary);

    /// <summary>
    /// Gets the year-to-date history by summing up each entry from the start of the tax year up to and including
    /// the specified month number.
    /// </summary>
    /// <param name="monthNumber">Month number to sum up to.</param>
    /// <param name="ytdHistory"><see cref="IEmployerHistoryEntry"/> containing the summarised data.</param>
    void GetYearToDateFigures(in int monthNumber, out IEmployerHistoryEntry ytdHistory);
}