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

using Paytools.NationalInsurance;
using Paytools.NationalInsurance.ReferenceData;

namespace Paytools.ReferenceData.NationalInsurance;

/// <summary>
/// Represents a set of National Insurance rates applicable to a specific NI category.
/// </summary>
public record NiCategoryRatesEntry : INiCategoryRatesEntry
{
    /// <summary>Gets the applicable National Insurance Category.</summary>
    public NiCategory Category { get; }

    /// <summary>Gets or sets the employee rate for earnings at or above lower earnings limit up to and including primary threshold.</summary>
    public decimal EmployeeRateToPT { get; set; }

    /// <summary>Gets or sets the employee rate for earnings above the primary threshold up to and including upper earnings limit.</summary>
    public decimal EmployeeRatePTToUEL { get; set; }

    /// <summary>Gets or sets the employee rate for balance of earnings above upper earnings limit.</summary>
    public decimal EmployeeRateAboveUEL { get; set; }

    /// <summary>Gets or sets the employer rate for earnings at or above lower earnings limit up to and including secondary threshold,.</summary>
    public decimal EmployerRateLELtoST { get; set; }

    /// <summary>Gets or sets the employer rate for earnings above secondary threshold up to and including Freeport upper secondary threshold.</summary>
    public decimal EmployerRateSTtoFUST { get; set; }

    /// <summary>Gets or sets the employer rate for earnings above Freeport upper secondary threshold up to and including upper earnings limit, upper
    /// secondary thresholds for under 21s, apprentices and veterans.</summary>
    public decimal EmployerRateFUSTtoUEL { get; set; }

    /// <summary>Gets or sets the employer rate for balance of earnings above upper earnings limit, upper secondary thresholds for under 21s, apprentices
    /// and veterans.</summary>
    public decimal EmployerRateAboveUEL { get; set; }

    /// <summary>
    /// Initialises a new instance of <see cref="NiCategoryRatesEntry"/> for the specified NI category.
    /// </summary>
    /// <param name="category">NI category for this NiCategoryRatesEntry.</param>
    public NiCategoryRatesEntry(NiCategory category)
    {
        Category = category;
    }

    /// <summary>
    /// Gets a string representation of this <see cref="NiCategoryRatesEntry"/>.
    /// </summary>
    /// <returns>String representation of this instance.</returns>
    public override string ToString() =>
        $"(EmployeeRateToPT:{EmployeeRateToPT}, EmployeeRatePTToUEL:{EmployeeRatePTToUEL}, EmployeeRateAboveUEL:{EmployeeRateAboveUEL}, EmployerRateLELtoST:{EmployerRateLELtoST}, EmployerRateSTtoFUST:{EmployerRateSTtoFUST}, EmployerRateFUSTtoUEL:{EmployerRateFUSTtoUEL}, EmployerRateAboveUEL:{EmployerRateAboveUEL})";
}