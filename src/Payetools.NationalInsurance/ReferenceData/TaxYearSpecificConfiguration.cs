// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;
using Payetools.NationalInsurance.Model;
using static Payetools.Common.Model.NiCategory;
using static Payetools.NationalInsurance.Model.NiThresholdType;

namespace Payetools.NationalInsurance.ReferenceData;

/// <summary>
/// Repository for settings configuration data that is tax year-specific but sits outside of
/// the dynamically loaded reference data configuration (see <em>Payetools.ReferenceData</em>.
/// </summary>
public static class TaxYearSpecificConfiguration
{
    private static readonly HashSet<NiCategory> _Pre_Apr6_2024_NiCategories = new HashSet<NiCategory>
    {
        A,
        B,
        C,
        F,
        H,
        I,
        J,
        L,
        M,
        N,
        S,
        V,
        X,
        Z
    };

    private static readonly HashSet<NiCategory> _Post_Apr62024_NiCategories = new HashSet<NiCategory>
    {
        A,
        B,
        C,
        D,
        E,
        F,
        H,
        I,
        J,
        K,
        L,
        M,
        N,
        S,
        V,
        X,
        Z
    };

    private static readonly HashSet<NiThresholdType> _Pre_Apr62024_NiThresholdTypes = new HashSet<NiThresholdType>
    {
        LEL,
        PT,
        ST,
        FUST,
        UST,
        AUST,
        VUST,
        UEL,
        DPT
    };

    private static readonly HashSet<NiThresholdType> _Post_Apr62024_NiThresholdTypes = new HashSet<NiThresholdType>
    {
        LEL,
        PT,
        ST,
        FUST,
        IZUST,
        UST,
        AUST,
        VUST,
        UEL,
        DPT
    };

    /// <summary>
    /// Gets the set of NI category letters that pertain to a specific tax year.
    /// </summary>
    /// <param name="taxYearEnding">Tax year ending value.</param>
    /// <returns>Array of NI categories that pertain to the supplied tax year.</returns>
    public static HashSet<NiCategory> GetNiCategoriesForTaxYear(TaxYearEnding taxYearEnding) =>
        taxYearEnding switch
        {
            > TaxYearEnding.Apr5_2024 => _Post_Apr62024_NiCategories,
            _ => _Pre_Apr6_2024_NiCategories
        };

    /// <summary>
    /// Gets the set of NI threshold types that pertain to a specific tax year.
    /// </summary>
    /// <param name="taxYearEnding">Tax year ending value.</param>
    /// <returns>Array of NI threshold types that pertain to the supplied tax year.</returns>
    public static HashSet<NiThresholdType> GetNiThresholdTypesForTaxYear(TaxYearEnding taxYearEnding) =>
        taxYearEnding switch
        {
            > TaxYearEnding.Apr5_2024 => _Post_Apr62024_NiThresholdTypes,
            _ => _Pre_Apr62024_NiThresholdTypes
        };
}