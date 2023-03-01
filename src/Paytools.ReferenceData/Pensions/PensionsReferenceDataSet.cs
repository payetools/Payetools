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

namespace Paytools.ReferenceData.Pensions;

/// <summary>
/// Represents the reference data for pensions for a period; where there have been in-year changes,
/// then there may be several such entries for a given tax year, although this is very uncommon.
/// </summary>
public class PensionsReferenceDataSet : IApplicableFromTill
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
    /// Gets the lower set of earnings thresholds for Qualifying Earnings (i.e., per week, per 2-weeks, etc.).
    /// </summary>
    public PensionsThresholdEntry QualifyingEarningsLowerLevel { get; init; }

    /// <summary>
    /// Gets the upper set of earnings thresholds for Qualifying Earnings (i.e., per week, per 2-weeks, etc.).
    /// </summary>
    public PensionsThresholdEntry QualifyingEarningsUpperLevel { get; init; }

    /// <summary>
    /// Gets the set of earnings triggers for Automatic Enrolment (i.e., per week, per 2-weeks, etc.).
    /// </summary>
    public PensionsThresholdEntry AeEarningsTrigger { get; init; }
}
