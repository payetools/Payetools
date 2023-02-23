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

namespace Paytools.StudentLoans.ReferenceData;

/// <summary>
/// Type that provides access to a specific set of student (and post-grad) loan
/// thresholds for a given plan type and specific period.
/// </summary>
public readonly struct StudentLoanThresholdSet : IStudentLoanThresholdSet
{
    /// <summary>
    /// Gets the period threshold for Plan 1 student loan deductions.
    /// </summary>
    public decimal Plan1PerPeriodThreshold { get; init; }

    /// <summary>
    /// Gets the period threshold for Plan 2 student loan deductions.
    /// </summary>
    public decimal Plan2PerPeriodThreshold { get; init; }

    /// <summary>
    /// Gets the period threshold for Plan 4 student loan deductions.
    /// </summary>
    public decimal Plan4PerPeriodThreshold { get; init; }

    /// <summary>
    /// Gets the period threshold for post-graduate student loan deductions.
    /// </summary>
    public decimal PostGradPerPeriodThreshold { get; init; }
}