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

namespace Paytools.ReferenceData.StudentLoans;

/// <summary>
/// Represents the reference data for student loans for a period; where there have been in-year changes,
/// then there may be several such entries for a given tax year, although this is very uncommon.
/// </summary>
public class StudentLoanReferenceDataEntry : IApplicableBetween
{
    /// <summary>
    /// Gets the start date (i.e., the first full day) for applicability.
    /// </summary>
    public DateOnly ApplicableFrom { get; init; }

    /// <summary>
    /// Gets the end date (i.e., the last full day) for applicability.
    /// </summary>
    public DateOnly ApplicableTill { get; init; }

    /// <summary>
    /// Gets the weekly, monthly and annual threshold for Plan 1 student loan deductions.
    /// </summary>
    public StudentLoanThresholdsEntry Plan1Thresholds { get; init; }

    /// <summary>
    /// Gets the weekly, monthly and annual threshold for Plan 2 student loan deductions.
    /// </summary>
    public StudentLoanThresholdsEntry Plan2Thresholds { get; init; }

    /// <summary>
    /// Gets the weekly, monthly and annual threshold for Plan 4 student loan deductions.
    /// </summary>
    public StudentLoanThresholdsEntry Plan4Thresholds { get; init; }

    /// <summary>
    /// Gets the weekly, monthly and annual threshold for post-graduate student loan deductions.
    /// </summary>
    public StudentLoanThresholdsEntry PostGradThresholds { get; init; }

    /// <summary>
    /// Gets the set of rates to be used for student and post-graduate loan deductions.
    /// </summary>
    public StudentLoanRatesSet DeductionRates { get; init; }
}