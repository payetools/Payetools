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

using Paytools.StudentLoans.ReferenceData;

namespace Paytools.ReferenceData.StudentLoans;

/// <summary>
/// Represents the set of deduction rates to be applied for student and post-graduate loans.
/// </summary>
public readonly struct StudentLoanRatesSet : IStudentLoanRateSet
{
    /// <summary>
    /// Gets the deduction rate for Plan 1, 2 and 4 student loan deductions.
    /// </summary>
    public decimal Student { get; init; }

    /// <summary>
    /// Gets the deduction rate for post-graduate student loan deductions.
    /// </summary>
    public decimal PostGrad { get; init; }
}
