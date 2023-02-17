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

namespace Paytools.NationalInsurance.ReferenceData;

/// <summary>
/// Interface for types that detail the various HMRC-published National Insurance threshold levels.
/// </summary>
public interface INiCategoryRatesEntry
{
    /// <summary>Gets the applicable National Insurance Category.</summary>
    NiCategory Category { get; }

    /// <summary>Gets the employee rate for earnings at or above lower earnings limit up to and including primary threshold.</summary>
    decimal EmployeeRateToPT { get; }

    /// <summary>Gets the employee rate for earnings above the primary threshold up to and including upper earnings limit.</summary>
    decimal EmployeeRatePTToUEL { get; }

    /// <summary>Gets the employee rate for balance of earnings above upper earnings limit.</summary>
    decimal EmployeeRateAboveUEL { get; }

    /// <summary>Gets the employer rate for earnings at or above lower earnings limit up to and including secondary threshold.</summary>
    decimal EmployerRateLELtoST { get; }

    /// <summary>Gets the employer rate for earnings above secondary threshold up to and including Freeport upper secondary threshold.</summary>
    decimal EmployerRateSTtoFUST { get; }

    /// <summary>Gets the employer rate for earnings above Freeport upper secondary threshold up to and including upper earnings limit, upper
    /// secondary thresholds for under 21s, apprentices and veterans.</summary>
    decimal EmployerRateFUSTtoUEL { get; }

    /// <summary>Gets the employer rate for balance of earnings above upper earnings limit, upper secondary thresholds for under 21s, apprentices
    /// and veterans.</summary>
    decimal EmployerRateAboveUEL { get; }
}