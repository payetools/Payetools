// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

namespace Payetools.ReferenceData.Employer;

/// <summary>
/// Represents the reference data needed to perform Apprentice Levy calculations.
/// </summary>
public class ApprenticeLevyInfo
{
    /// <summary>
    /// Gets the threshold at which employers need to start paying the Apprentice Levy.
    /// </summary>
    public decimal ApprenticeLevyThreshold { get; init; }

    /// <summary>
    /// Gets the allowance that employers get to offset their levy payments.
    /// </summary>
    public decimal ApprenticeLevyAllowance { get; init; }

    /// <summary>
    /// Gets the rate (as a decimal) that employers must pay Apprentice Levy contributions at.
    /// </summary>
    public decimal ApprenticeLevyRate { get; init; }
}
