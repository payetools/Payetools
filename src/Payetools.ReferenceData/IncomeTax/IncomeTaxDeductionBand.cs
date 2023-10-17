// Copyright (c) 2023 Payetools Foundation.
//
// Payetools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Payetools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

namespace Payetools.ReferenceData.IncomeTax;

/// <summary>
/// Record that details a single tax band for a given tax regime.
/// </summary>
public record IncomeTaxDeductionBand
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