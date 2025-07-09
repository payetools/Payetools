// Copyright (c) 2023-2025, Payetools Ltd.
//
// Payetools Ltd licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;

namespace Payetools.Pensions.Model;

/// <summary>
/// Interface for types that represent pension schemes.
/// </summary>
/// <remarks>A <see cref="IPensionScheme"/> instance refers to a pension scheme with a specific provider, with
/// specific tax treatment and earnings basis.  Whilst it is not common for pension schemes to change
/// tax treatment, it is quite possible for an employer to operate more than one type of earnings basis
/// across its employee base.  In this case, two (or more) instances of this type would be required, one
/// for each earnings basis in use, even though all contributions might be flowing to a single scheme
/// with the same provider.</remarks>
public interface IPensionScheme
{
    /// <summary>
    /// Gets the earnings basis for this pension scheme.
    /// </summary>
    PensionsEarningsBasis EarningsBasis { get; }

    /// <summary>
    /// Gets the tax treatment for this pension scheme.
    /// </summary>
    PensionTaxTreatment TaxTreatment { get; }
}