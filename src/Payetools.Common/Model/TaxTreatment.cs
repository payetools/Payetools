// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

namespace Payetools.Common.Model;

/// <summary>
/// Enum representing the tax treatment aspect of the tax code.
/// </summary>
public enum TaxTreatment
{
    /// <summary>Not specified.</summary>
    Unspecified,

    /// <summary>NT - no tax payable.</summary>
    NT,

    /// <summary>BR - basic rate tax to be applied to all taxable earnings.</summary>
    BR,

    /// <summary>D0 - next highest rate above BR to be applied to all taxable earnings.</summary>
    D0,

    /// <summary>D1 - next highest rate above D0 to be applied to all taxable earnings.</summary>
    D1,

    /// <summary>D2 - next highest rate above D1 to be applied to all taxable earnings.</summary>
    D2,

    /// <summary>Zero personal allowance to be applied.</summary>
    _0T,

    /// <summary>Additional notional taxable income must be applied to existing taxable earnings.</summary>
    K,

    /// <summary>Tax payer is entitled to the standard Personal Allowance.</summary>
    L,

    /// <summary>Married Allowance where individual has received a transfer of 10% of their partner’s Personal Allowance.</summary>
    M,

    /// <summary>Married Allowance where individual has transferred 10% of their Personal Allowance to their partner.</summary>
    N
}

/// <summary>
/// Extension methods for <see cref="TaxTreatment"/>.
/// </summary>
public static class TaxTreatmentExtensions
{
    /// <summary>
    /// Gets the zero-based index of the band that a given tax code (BR, D0, D1, D2) applies to.
    /// </summary>
    /// <param name="taxTreatment">Tax treatment to determine band index for.</param>
    /// <param name="applicableCountries">Applicable countries for the input tax treatment.</param>
    /// <returns>Band index for supplied tax treatment.</returns>
    /// <exception cref="ArgumentException">Thrown if it is not possible to retrieve a band index for ththe supplied tax treatment.</exception>
    public static int GetBandIndex(this TaxTreatment taxTreatment, in CountriesForTaxPurposes applicableCountries) =>
        taxTreatment switch
        {
            TaxTreatment.BR => applicableCountries == CountriesForTaxPurposes.Scotland ? 1 : 0,
            TaxTreatment.D0 => applicableCountries == CountriesForTaxPurposes.Scotland ? 2 : 1,
            TaxTreatment.D1 => applicableCountries == CountriesForTaxPurposes.Scotland ? 3 : 2,
            TaxTreatment.D2 => applicableCountries == CountriesForTaxPurposes.Scotland ? 4 : throw new ArgumentOutOfRangeException(nameof(taxTreatment), "Invalid tax treatment for applicable tax regime"),
            _ => throw new ArgumentException($"Band index not valid for tax treatment {taxTreatment}", nameof(taxTreatment))
        };
}