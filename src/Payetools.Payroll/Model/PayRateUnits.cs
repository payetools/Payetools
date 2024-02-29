// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

namespace Payetools.Payroll.Model;

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
    PerDay,

    /// <summary>
    /// Weekly rate, commonly used for some statutory payments.
    /// </summary>
    PerWeek,

    /// <summary>
    /// Per pay period, useful for fixed amounts each pay period.
    /// </summary>
    PerPayPeriod
}
