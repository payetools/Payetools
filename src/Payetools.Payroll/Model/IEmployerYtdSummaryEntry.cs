// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

namespace Payetools.Payroll.Model;

/// <summary>
/// Interface that represents summarised pay run information across all pay runs year-to-date.
/// </summary>`
public interface IEmployerYtdSummaryEntry
{
    /// <summary>
    /// Gets the applicable month number for this year-to-date entry.
    /// </summary>
    int MonthNumber { get; }

    /// <summary>
    /// Gets the total year-to-date Statutory Maternity Pay amount. May be zero.
    /// </summary>
    decimal TotalYtdSMP { get; }

    /// <summary>
    /// Gets the total year-to-date Statutory Paternity Pay amount. May be zero.
    /// </summary>
    decimal TotalYtdSPP { get; }

    /// <summary>
    /// Gets the total year-to-date Statutory Adoption Pay amount. May be zero.
    /// </summary>
    decimal TotalYtdSAP { get; }

    /// <summary>
    /// Gets the total year-to-date Statutory Shared Parental Pay amount. May be zero.
    /// </summary>
    decimal TotalYtdShPP { get; }

    /// <summary>
    /// Gets the total year-to-date Statutory Parental Bereavement Pay amount. May be zero.
    /// </summary>
    decimal TotalYtdSPBP { get; }
}