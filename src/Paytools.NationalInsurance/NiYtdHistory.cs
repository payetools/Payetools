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

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paytools.NationalInsurance;

/// <summary>
/// Represents an employee's year to date National Insurance history.
/// </summary>
public class NiYtdHistory
{
    private readonly ImmutableList<EmployeeNiHistoryEntry> _entries;

    /// <summary>
    /// Initialises a new instance of <see cref="NiYtdHistory"/>.
    /// </summary>
    /// <param name="entries">NI history entries for the tax year to date.</param>
    public NiYtdHistory(ImmutableList<EmployeeNiHistoryEntry> entries)
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
