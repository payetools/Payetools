// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Extensions;
using System.Collections;
using System.Collections.Immutable;

namespace Payetools.NationalInsurance.Model;

/// <summary>
/// Represents an employee's year to date National Insurance history.
/// </summary>
public class NiYtdHistory : IEnumerable<IEmployeeNiHistoryEntry>
{
    private readonly ImmutableArray<IEmployeeNiHistoryEntry> _entries;

    /// <summary>
    /// Gets the value of any Class 1A National Insurance contributions payable year to date.
    /// </summary>
    public decimal? Class1ANicsYtd { get; private set; }

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

        Class1ANicsYtd = initialNiCalculationResult.Class1ANicsPayable;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="NiYtdHistory"/> class.
    /// </summary>
    public NiYtdHistory()
    {
        _entries = ImmutableArray<IEmployeeNiHistoryEntry>.Empty;
    }

    /// <summary>
    /// Initialises a new instance of <see cref="NiYtdHistory"/>.
    /// </summary>
    /// <param name="entries">NI history entries for the tax year to date.</param>
    /// <param name="class1ANicsYtd">Class 1A NICs for the tax year to date.</param>
    public NiYtdHistory(in ImmutableArray<IEmployeeNiHistoryEntry> entries, decimal? class1ANicsYtd)
    {
        _entries = entries;
        Class1ANicsYtd = class1ANicsYtd;
    }

    /// <summary>
    /// Returns a new instance of <see cref="NiYtdHistory"/> with the previous history updated by the latest
    /// payrun result.  Where an entry in the history matches the current NI category, that entry
    /// is updated, but otherwise a new history entry is created and appended.
    /// </summary>
    /// <param name="latestNiCalculationResult">Result of this payrun's NI calculation.</param>
    /// <returns>A new instance of <see cref="NiYtdHistory"/> with the previous history updated by the latest
    /// payrun result.</returns>
    public NiYtdHistory Add(in INiCalculationResult latestNiCalculationResult)
    {
        int index;
        var entryAlreadyPresent = false;

        for (index = 0; index < _entries.Length; index++)
            if (entryAlreadyPresent = _entries[index].NiCategoryPertaining == latestNiCalculationResult.NiCategory)
                break;

        var class1ANicsYtd = latestNiCalculationResult.Class1ANicsPayable == null && Class1ANicsYtd == null ?
            null :
            latestNiCalculationResult.Class1ANicsPayable ?? 0.0m + Class1ANicsYtd;

        return entryAlreadyPresent ?
            new NiYtdHistory(_entries.ReplaceAt(index, _entries[index].Add(latestNiCalculationResult)), class1ANicsYtd) :
            new NiYtdHistory(_entries.Add(new EmployeeNiHistoryEntry(latestNiCalculationResult)), class1ANicsYtd);
    }

    /// <summary>
    /// Gets the totals of employee and employer NI contributions paid to date across all entries.
    /// </summary>
    /// <returns>Totals of employee and employer NI contributions paid tear to date.</returns>
    public (decimal employeeTotal, decimal employerTotal) GetNiYtdTotals() =>
        (_entries.Sum(e => e.EmployeeContribution), _entries.Sum(e => e.EmployerContribution));

    /// <summary>
    /// Gets an enumerator to enumerate over the employee's NI history entries by NI category.
    /// </summary>
    /// <returns>Enumerator for enumerating over the employee's NI history entries.</returns>
    public IEnumerator<IEmployeeNiHistoryEntry> GetEnumerator() =>
        _entries.AsEnumerable().GetEnumerator();

    /// <summary>
    /// Gets an untyped enumerator to enumerate over the employee's NI history entries by NI category.
    /// </summary>
    /// <returns>Untyped Enumerator for enumerating over the employee's NI history entries.</returns>
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
