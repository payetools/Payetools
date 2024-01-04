// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

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