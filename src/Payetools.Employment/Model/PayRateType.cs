// Copyright (c) 2023 Payetools Foundation.
//
// Payetools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Payetools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

namespace Payetools.Employment.Model;

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