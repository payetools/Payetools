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

namespace Paytools.NationalInsurance;

/// <summary>
/// Represents once element of the employee's NI history during the current tax year.  For employees that have only
/// have one NI category throughout the tax year, they will have just one instance of <see cref="EmployeeNiHistoryEntry"/>
/// applicable.  But it is of course possible for an employee's NI category to change throughout the tax year (for example
/// because they turned 21 years of age), and in this case, multiple records must be held.
/// </summary>
public record EmployeeNiHistoryEntry
{
    private NiEarningsBreakdown _niEarningsBreakdown = default;

    /// <summary>
    /// Gets the National Insurance category letter pertaining to this record.
    /// </summary>
    public NiCategory NiCategoryPertaining { get; init; }

    /// <summary>
    /// Gets the gross NI-able earnings applicable for the duration of this record.
    /// </summary>
    public decimal GrossNicableEarnings { get; init; }

    /// <summary>
    /// Gets the total employee contribution made during the duration of this record.
    /// </summary>
    public decimal EmployeeContribution { get; init; }

    /// <summary>
    /// Gets the total employer contribution made during the duration of this record.
    /// </summary>
    public decimal EmployerContribution { get; init; }

    /// <summary>
    /// Gets the total contribution (employee + employer) made during the duration of this record.
    /// </summary>
    public decimal TotalContribution { get; init; }

    /// <summary>
    /// Gets the earnings up to and including the Lower Earnings Limit for this record.
    /// </summary>
    public decimal EarningsUpToAndIncludingLEL => _niEarningsBreakdown.EarningsUpToAndIncludingLEL;

    /// <summary>
    /// Gets the earnings up above the Lower Earnings Limit and up to and including the Secondary Threshold
    /// for this record.
    /// </summary>
    public decimal EarningsAboveLELUpToAndIncludingST => _niEarningsBreakdown.EarningsAboveLELUpToAndIncludingST;

    /// <summary>
    /// Gets the earnings up above the Secondary Threshold and up to and including the Primary Threshold
    /// for this record.
    /// </summary>
    public decimal EarningsAboveSTUpToAndIncludingPT => _niEarningsBreakdown.EarningsAboveSTUpToAndIncludingPT;

    /// <summary>
    /// Gets the earnings up above the Primary Threshold and up to and including the Freeport Upper Secondary
    /// Threshold for this record.
    /// </summary>
    public decimal EarningsAbovePTUpToAndIncludingFUST => _niEarningsBreakdown.EarningsAbovePTUpToAndIncludingFUST;

    /// <summary>
    /// Gets the earnings up above the Freeport Upper Secondary Threshold and up to and including the Upper
    /// Earnings Limit for this record.
    /// </summary>
    public decimal EarningsAboveFUSTUpToAndIncludingUEL => _niEarningsBreakdown.EarningsAboveFUSTUpToAndIncludingUEL;

    /// <summary>
    /// Gets the earnings up above the Upper Earnings Limit for this record.
    /// </summary>
    public decimal EarningsAboveUEL => _niEarningsBreakdown.EarningsAboveUEL;

    /// <summary>
    /// Gets the earnings up above the Secondary Threshold and up to and including the Upper Earnings Limit
    /// for this record.
    /// </summary>
    public decimal EarningsAboveSTUpToAndIncludingUEL => _niEarningsBreakdown.EarningsAboveSTUpToAndIncludingUEL;
}