// Copyright (c) 2023 Paytools Foundation.
//
// Licensed under the Apache License, Version 2.0 (the "License") ~
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System.Collections.Immutable;

namespace Paytools.NationalInsurance.Model;

/// <summary>
/// Represents an employee's year to date National Insurance history.
/// </summary>
public record NiYtdHistory
{
    private readonly ImmutableList<IEmployeeNiHistoryEntry> _entries;

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
        var prevCount = _entries.Count;
        var lastEntry = prevCount > 0 ? _entries.Last() : null;

        if (lastEntry != null && lastEntry.NiCategoryPertaining != latestNiCalculationResult.NiCategory)
        {
            return new NiYtdHistory(_entries
                    .RemoveAt(prevCount - 1)
                    .Add(lastEntry.Add(latestNiCalculationResult)));
        }
        else
        {
            return new NiYtdHistory(_entries.Add(new EmployeeNiHistoryEntry(latestNiCalculationResult)));
        }
    }

    /// <summary>
    /// Initialises a new instance of <see cref="NiYtdHistory"/> with the supplied NI calculation result.  This
    /// constructor is intended to be used for the first time a payrun is run during the tax year.
    /// </summary>
    /// <param name="initialNiCalculationResult">NI calculation result for the first payrun of the tax year
    /// for a given employee.</param>
    public NiYtdHistory(in INiCalculationResult initialNiCalculationResult)
    {
        _entries = ImmutableList<IEmployeeNiHistoryEntry>.Empty
            .Add(new EmployeeNiHistoryEntry(initialNiCalculationResult));
    }

    /// <summary>
    /// Initialises a new instance of <see cref="NiYtdHistory"/>.
    /// </summary>
    /// <param name="entries">NI history entries for the tax year to date.</param>
    public NiYtdHistory(in ImmutableList<IEmployeeNiHistoryEntry> entries)
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
