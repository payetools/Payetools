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

namespace Paytools.StudentLoans.ReferenceData;

/// <summary>
/// Interface for types that provide access to a specific set of student (and post-grad) loan
/// thresholds for a given plan type and specific period.
/// </summary>
public interface IStudentLoanThresholdSet
{
    /// <summary>
    /// Gets the period threshold for Plan 1 student loan deductions.
    /// </summary>
    decimal Plan1PerPeriodThreshold { get; }

    /// <summary>
    /// Gets the period threshold for Plan 2 student loan deductions.
    /// </summary>
    decimal Plan2PerPeriodThreshold { get; }

    /// <summary>
    /// Gets the period threshold for Plan 4 student loan deductions.
    /// </summary>
    decimal Plan4PerPeriodThreshold { get; }

    /// <summary>
    /// Gets the period threshold for post-graduate student loan deductions.
    /// </summary>
    decimal PostGradPerPeriodThreshold { get; }
}