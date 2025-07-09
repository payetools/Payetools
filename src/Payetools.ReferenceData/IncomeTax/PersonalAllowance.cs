// Copyright (c) 2023-2025, Payetools Ltd.
//
// Payetools Ltd licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

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