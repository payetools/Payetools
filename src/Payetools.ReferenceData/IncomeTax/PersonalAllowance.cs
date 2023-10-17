// Copyright (c) 2023 Payetools Foundation.
//
// Payetools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Payetools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

using Payetools.Common.Model;

namespace Payetools.ReferenceData.IncomeTax;

/// <summary>
/// Represents a given personal allowance value for a specific pay frequency.
/// </summary>
public readonly struct PersonalAllowance
{
    /// <summary>
    /// Gets the pay frequency applicable to this personal allowance value.
    /// </summary>
    public PayFrequency PayFrequency { get; init; }

    /// <summary>
    /// Gets the personal allowance value for this pay frequency.
    /// </summary>
    public decimal Value { get; init; }
}
