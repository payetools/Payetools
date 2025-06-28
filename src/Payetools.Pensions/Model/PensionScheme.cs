// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;

namespace Payetools.Pensions.Model;

/// <summary>
/// Represents a workplace pension scheme.
/// </summary>
public readonly struct PensionScheme : IPensionScheme
{
    /// <summary>
    /// Gets the earnings basis for this pension scheme.
    /// </summary>
    public PensionsEarningsBasis EarningsBasis { get; init; }

    /// <summary>
    /// Gets the tax treatment for this pension scheme.
    /// </summary>
    public PensionTaxTreatment TaxTreatment { get; init; }
}