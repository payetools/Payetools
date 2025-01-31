// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

namespace Payetools.ReferenceData.IncomeTax;

/// <summary>
/// Record that details a single tax band for a given tax regime.
/// </summary>
public class IncomeTaxDeductionBand
{
    /// <summary>
    /// Gets the description of this tax band.
    /// </summary>
    public string Description { get; init; } = default!;

    /// <summary>
    /// Gets the taxable earnings threshold from which this tax band applies.
    /// </summary>
    public decimal? From { get; init; }

    /// <summary>
    /// Gets the taxable earnings threshold up to which this tax band applies.
    /// </summary>
    public decimal? To { get; init; }

    /// <summary>
    /// Gets the applicable tax rate.  NB Tax rates are normally expressed as percentages;
    /// values here are fractional i.e., 20% = 0.2m.
    /// </summary>
    public decimal Rate { get; init; }

    /// <summary>
    /// Gets a value indicating whether this band refers to the bottom rate of tax.
    /// </summary>
    public bool IsBottomRate => From == null;

    /// <summary>
    /// Gets a value indicating whether this band refers to the top rate of tax.
    /// </summary>
    public bool IsTopRate => To == null;
}