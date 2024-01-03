// Copyright (c) 2023-2024, Payetools Foundation.
//
// Paytools Foundation licenses this file to you under one of the following licenses:
//
//   * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//   * Paytools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

namespace Payetools.NationalInsurance.Model;

/// <summary>
/// Represents the method of director's NI calculation to be applied when calculating
/// National Insurance contributions for a director.
/// </summary>
public enum DirectorsNiCalculationMethod
{
    /// <summary>
    /// Standard annualised earnings method; common for directors who are paid irregularly.
    /// </summary>
    StandardAnnualisedEarningsMethod,

    /// <summary>
    /// Alternativee method; common for directors who are paid regularly.
    /// </summary>
    AlternativeMethod
}