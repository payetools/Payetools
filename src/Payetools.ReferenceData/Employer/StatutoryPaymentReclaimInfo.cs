// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

namespace Payetools.ReferenceData.Employer;

/// <summary>
/// Represents the reference data needed to determine the amounts that can be reclaimed against
/// statutory payments (e.g., SMP, SPP, etc).
/// </summary>
public class StatutoryPaymentReclaimInfo
{
    /// <summary>
    /// Gets the threshold up to which employers are eligible for Small Employers Relief.
    /// </summary>
    public decimal SmallEmployerReliefThreshold { get; init; }

    /// <summary>
    /// Gets the rate (as a decimal) at which employers can reclaim eligible statutory payments, if
    /// they are not entitled to Small Employers Relief.
    /// </summary>
    public decimal DefaultReclaimRate { get; init; }

    /// <summary>
    /// Gets the rate (as a decimal) at which employers can reclaim eligible statutory payments if
    /// they are entitled to Small Employers Relief.
    /// </summary>
    public decimal SmallEmployersReclaimRate { get; init; }
}
