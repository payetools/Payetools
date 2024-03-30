// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Extensions;
using System.Collections.Immutable;

namespace Payetools.NationalInsurance.Model;

/// <summary>
/// Represents an employee's year to date National Insurance history.
/// </summary>
public record NiYtdHistory
{
    private readonly ImmutableArray<IEmployeeNiHistoryEntry> _entries;

    /// <summary>
    /// Returns a new instance of <see cref="NiYtdHistory"/> with the previous history updated by the latest
    /// payrun result.  Where the most recent entry in the history matches the current NI category, that entry
    /// is updated, but otherwise a new history entry is created and appended.
    /// </summary>
    /// <param name="latestNiCalculationResult">Result of this payrun's NI calculation.</param>
    /// <returns>A new instance of <see cref="NiYtdHistory"/> with the previous history updated by the latest
    /// payrun result.</returns>
    public NiYtdHistory Add(in INiCalculationResult latestNiCalculationResult)
    {
        var lastEntry = _entries.Any() ? _entries.Last() : null;

        return lastEntry?.NiCategoryPertaining == latestNiCalculationResult.NiCategory ?
            new NiYtdHistory(_entries.ReplaceLast(lastEntry.Add(latestNiCalculationResult))) :
            new NiYtdHistory(_entries.Add(new EmployeeNiHistoryEntry(latestNiCalculationResult)));
    }

    /// <summary>
    /// Initialises a new instance of <see cref="NiYtdHistory"/> with the supplied NI calculation result.  This
    /// constructor is intended to be used for the first time a payrun is run during the tax year.
    /// </summary>
    /// <param name="initialNiCalculationResult">NI calculation result for the first payrun of the tax year
    /// for a given employee.</param>
    public NiYtdHistory(in INiCalculationResult initialNiCalculationResult)
    {
        _entries = ImmutableArray<IEmployeeNiHistoryEntry>.Empty
            .Add(new EmployeeNiHistoryEntry(initialNiCalculationResult));
    }

    /// <summary>
    /// Initialises a new instance of <see cref="NiYtdHistory"/>.
    /// </summary>
    /// <param name="entries">NI history entries for the tax year to date.</param>
    public NiYtdHistory(in ImmutableArray<IEmployeeNiHistoryEntry> entries)
    {
        _entries = entries;
    }

    /// <summary>
    /// Gets the totals of employee and employer NI contributions paid to date across all entries.
    /// </summary>
    /// <returns>Totals of employee and employer NI contributions paid tear to date.</returns>
    public (decimal employeeTotal, decimal employerTotal) GetNiYtdTotals() =>
        (_entries.Sum(e => e.EmployeeContribution), _entries.Sum(e => e.EmployerContribution));
}
