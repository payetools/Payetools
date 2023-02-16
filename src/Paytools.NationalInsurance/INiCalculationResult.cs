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
/// Interface for types representing National Insurance calculation results.
/// </summary>
public interface INiCalculationResult
{
    /// <summary>
    /// Gets the breakdown of earnings across each of the different National Insurance thresholds.
    /// </summary>
    NiEarningsBreakdown EarningsBreakdown { get; }

    /// <summary>
    /// Gets the total employee contribution due as a result of this calculation.
    /// </summary>
    decimal EmployeeContribution { get; }

    /// <summary>
    /// Gets the total employer contribution due as a result of this calculation.
    /// </summary>
    decimal EmployerContribution { get; }

    /// <summary>
    /// Gets the total contribution due (employee + employer) as a result of this calculation.
    /// </summary>
    decimal TotalContribution { get; }

    /// <summary>
    /// Gets a value indicating whether the results of this calculation need to be reported to HMRC.
    /// </summary>
    bool NoRecordingRequiredIndicator { get; }
}
