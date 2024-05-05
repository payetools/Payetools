// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;

namespace Payetools.Payroll.Model;

/// <summary>
/// Represents summarised pay run information across all pay runs year-to-date.
/// </summary>`
public class EmployerYtdHistoryEntry : IEmployerYtdHistoryEntry
{
    /// <summary>
    /// Gets the applicable month number for this year-to-date entry.
    /// </summary>
    public int MonthNumber { get; }

    /// <summary>
    /// Gets the total year-to-date Statutory Maternity Pay amount. May be zero.
    /// </summary>
    public decimal TotalYtdSMP { get; }

    /// <summary>
    /// Gets the total year-to-date Statutory Paternity Pay amount. May be zero.
    /// </summary>
    public decimal TotalYtdSPP { get; }

    /// <summary>
    /// Gets the total year-to-date Statutory Adoption Pay amount. May be zero.
    /// </summary>
    public decimal TotalYtdSAP { get; }

    /// <summary>
    /// Gets the total year-to-date Statutory Shared Parental Pay amount. May be zero.
    /// </summary>
    public decimal TotalYtdShPP { get; }

    /// <summary>
    /// Gets the total year-to-date Statutory Parental Bereavement Pay amount. May be zero.
    /// </summary>
    public decimal TotalYtdSPBP { get; }
}
