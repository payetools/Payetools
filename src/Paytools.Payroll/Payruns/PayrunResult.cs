// Copyright (c) 2022-2023 Paytools Ltd
//
// Licensed under the Apache License, Version 2.0 (the "License")~
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

using Paytools.Common.Model;
using Paytools.Employment;
using System.Collections.Immutable;

namespace Paytools.Payroll.Payruns;

/// <summary>
/// Represents the output of a payrun.
/// </summary>
public record PayrunResult : IPayrunResult
{
    /// <summary>
    /// Gets the employer that this payrun refers to.
    /// </summary>
    public IEmployer Employer { get; init; } = null!;

    /// <summary>
    /// Gets the pay date for this payrun.
    /// </summary>
    public PayDate PayDate { get; init; }

    /// <summary>
    /// Gets the list of employee payrun entries.
    /// </summary>
    public ImmutableList<IEmployeePayrunEntry> EmployeePayrunEntries { get; init; } = null!;
}