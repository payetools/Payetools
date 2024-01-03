// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;
using Payetools.NationalInsurance.ReferenceData;

namespace Payetools.ReferenceData.NationalInsurance;

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