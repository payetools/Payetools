// Copyright (c) 2023 Paytools Foundation
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

using Paytools.NationalInsurance;
using Paytools.NationalInsurance.ReferenceData;

namespace Paytools.ReferenceData;

public record NiCategoryRateEntry : INiCategoryRateEntry
{
    /// <summary>Applicable National Insurance Category.</summary>
    public NiCategory Category { get; init; }

    /// <summary>Employee rate for earnings at or above lower earnings limit up to and including primary threshold.</summary>
    public decimal EmployeeRateToPT { get; init; }

    /// <summary>Employee rate for earnings above the primary threshold up to and including upper earnings limit.</summary>
    public decimal EmployeeRatePTToUEL { get; init; }

    /// <summary>Employee rate for balance of earnings above upper earnings limit</summary>
    public decimal EmployeeRateAboveUEL { get; init; }

    /// <summary>Employer rate for earnings at or above lower earnings limit up to and including secondary threshold,</summary>
    public decimal EmployerRateLELtoST { get; init; }

    /// <summary>Employer rate for earnings above secondary threshold up to and including Freeport upper secondary threshold.</summary>
    public decimal EmployerRateSTtoFUST { get; init; }

    /// <summary>Employer rate for earnings above Freeport upper secondary threshold up to and including upper earnings limit, upper 
    /// secondary thresholds for under 21s, apprentices and veterans.</summary>
    public decimal EmployerRateFUSTtoUEL { get; init; }

    /// <summary>Employer rate for balance of earnings above upper earnings limit, upper secondary thresholds for under 21s, apprentices 
    /// and veterans.</summary>
    public decimal EmployerRateAboveUEL { get; init; }
}