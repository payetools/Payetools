// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

namespace Payetools.Common.Model;

/// <summary>
/// Represents the set of National Insurance category letters assigned by HMRC.  Taken from
/// https://www.gov.uk/national-insurance-rates-letters/category-letters (retrieved 5-Dec-2022).
/// </summary>
public enum NiCategory
{
    /// <summary>Not specified.</summary>
    Unspecified,

    /// <summary>All employees apart from those in groups B, C, H, J, M, V and Z, and those that work in
    /// freeports (F, I, L and S) and those that are exempt (X).</summary>
    A,

    /// <summary>Married women and widows entitled to pay reduced National Insurance.</summary>
    B,

    /// <summary>Employees over the State Pension age.</summary>
    C,

    /// <summary>Investment Zone — deferment.  Added for 2024-2025.</summary>
    D,

    /// <summary>Investment Zone — married women and widows reduced rate.  Added for 2024-2025.</summary>
    E,

    /// <summary>All employees who work in freeports, apart from those in categories I, L, and S.</summary>
    F,

    /// <summary>Apprentices under 25.</summary>
    H,

    /// <summary>Married women and widows who work in freeports and are entitled to pay reduced National Insurance.</summary>
    I,

    /// <summary>Employees who can defer National Insurance because they’re already paying it in another job.</summary>
    J,

    /// <summary>Investment Zone — state pensioner.  Added for 2024-2025.</summary>
    K,

    /// <summary>Employees who work in freeports and can defer National Insurance because they’re already paying it in another job.</summary>
    L,

    /// <summary>Employees under 21.</summary>
    M,

    /// <summary>Investment Zone.  Added for 2024-2025.</summary>
    N,

    /// <summary>Employees who work in freeports and are over the State Pension age.</summary>
    S,

    /// <summary>Employees who are working in their first job since leaving the armed forces (veterans).</summary>
    V,

    /// <summary>Employees who do not have to pay National Insurance, for example because they’re under 16.</summary>
    X,

    /// <summary>Employees under 21 who can defer National Insurance because they’re already paying it in another job.</summary>
    Z
}

/// <summary>
/// Extension methods related to <see cref="NiCategory"/>.
/// </summary>
public static class NiCategoryRelatedExtensions
{
    /// <summary>
    /// Converts the supplied string into an <see cref="NiCategory"/> value based on the
    /// specified tax year.  (Not all NI letters are valid for all tax years.)
    /// </summary>
    /// <param name="value">String value to convert.  May be null.</param>
    /// <param name="taxYearEnding">Tax year ending that pertains to this conversion.</param>
    /// <returns>NI category value that corresponds to the supplied string.</returns>
    /// <exception cref="ArgumentException">Thrown if the supplied value cannot be converted
    /// to a valid NI category for the specified tax year.</exception>
    public static NiCategory? ToNiCategory(this string? value, TaxYearEnding taxYearEnding)
    {
        if (value == null)
            return null;

        if (!Enum.TryParse<NiCategory>(value, out var category))
            throw new ArgumentException($"Value '{value}' is not a valid NICategory value", nameof(value));

        if (taxYearEnding < TaxYearEnding.Apr5_2025)
        {
            if (category == NiCategory.D ||
                category == NiCategory.E ||
                category == NiCategory.K ||
                category == NiCategory.N)
                throw new ArgumentException("NI Categories D, E, K and N are not valid NI categories prior to tax year ending 5 April 2025", nameof(value));
        }

        return category;
    }
}