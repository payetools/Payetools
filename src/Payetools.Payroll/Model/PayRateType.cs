// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

namespace Payetools.Payroll.Model;

/// <summary>
/// Enum representing different pay types, i.e., salaried, hourly paid, etc.
/// </summary>
public enum PayRateType
{
    /// <summary>
    /// Per annum pay type for salaried employees.
    /// </summary>
    Salaried,

    /// <summary>
    /// Hourly pay type for hourly-paid employees.
    /// </summary>
    HourlyPaid,

    /// <summary>
    /// All pay rate type other than salaried and hourly paid.
    /// </summary>
    Other
}