// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

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