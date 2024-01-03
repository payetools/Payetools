// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Payetools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

namespace Payetools.Common.Model;

/// <summary>
/// Enum that represents the tax treatment to be applied to employee pension contributions.
/// </summary>
public enum PensionTaxTreatment
{
    /// <summary>Not specified.</summary>
    Unspecified,

    /// <summary>Relief at source.  Contributions are taken from post-tax salary and the pension
    /// provider claims back basic rate tax from HMRC.</summary>
    ReliefAtSource,

    /// <summary>
    /// Net pay arrangement.  Contributions are taken from salary before tax.
    /// </summary>
    NetPayArrangement
}