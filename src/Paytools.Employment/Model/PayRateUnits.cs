// Copyright (c) 2023 Paytools Foundation.
//
// Paytools Foundation licenses this file to you under one of the following licenses:
//
//   * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//   * Paytools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

namespace Paytools.Employment.Model;

/// <summary>
/// Enum representing different pay units, i.e., per annum, per hour, etc.
/// </summary>
public enum PayRateUnits
{
    /// <summary>
    /// Per annum pay type for salaried employees.
    /// </summary>
    PerAnnum,

    /// <summary>
    /// Hourly pay type for hourly-paid employees.
    /// </summary>
    PerHour,

    /// <summary>
    /// Daily rate, typically for salaried employees with regular working patterns.
    /// </summary>
    PerDay
}
