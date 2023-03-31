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

using Paytools.Common.Model;

namespace Paytools.Employment.Model;

/// <summary>
/// Struct that holds student loan status information for an employee.
/// </summary>
public readonly struct StudentLoanStatus
{
    /// <summary>
    /// Gets the student loan applicable for an employee.  Null if the employee does not have an
    /// outstanding student loan.
    /// </summary>
    public StudentLoanType? StudentLoanType { get; init; }

    /// <summary>
    /// Gets a value indicating whether the employee has an outstanding post-graduate loan.
    /// </summary>
    public bool HasPostGradLoan { get; init; }
}
