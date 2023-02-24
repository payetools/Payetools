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

namespace Paytools.ReferenceData.IncomeTax;

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