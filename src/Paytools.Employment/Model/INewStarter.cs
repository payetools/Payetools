// Copyright (c) 2023 Paytools Foundation.  All rights reserved.
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

using Paytools.StudentLoans;

namespace Paytools.Employment.Model;

/// <summary>
/// Interface that represents a new starter for employment purposes.
/// </summary>
public interface INewStarter : IEmployee
{
    /// <summary>
    /// Gets or sets the employee's starter declaration; null if it was not possible to
    /// obtain a starter declaration from the employee.
    /// </summary>
    public StarterDeclaration? StarterDeclaration { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether student loan deductions should continue.
    /// </summary>
    /// <remarks>As a P45 from a previous employer does not indicate the student loan type,
    /// it may be necessary to request the employee's student loan plan type separately.</remarks>
    public bool StudentLoanDeductionNeeded { get; set; }

    /// <summary>
    /// Gets or sets any applicable student loan type, if known.  (See <see cref="StudentLoanDeductionNeeded"/>).
    /// </summary>
    public StudentLoanType? StudentLoanType { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether post-graduate loan deductions should continue.
    /// </summary>
    public bool GraduateLoanDeductionNeeded { get; set; }
}
