// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

namespace Payetools.Payroll.Model;

/// <summary>
/// Interface that represents summarised pay run information across all pay runs year-to-date.
/// </summary>`
public interface IEmployerYtdHistoryEntry
{
    /// <summary>
    /// Gets the total amount of income tax for the tax month. May be zero.
    /// </summary>
    decimal TotalIncomeTax { get; }

    /// <summary>
    /// Gets the total amount of student loan repayment for the tax month. May be zero.
    /// </summary>
    decimal TotalStudentLoans { get; }

    /// <summary>
    /// Gets the total amount of postgraduate loan repayment for the tax month. May be zero.
    /// </summary>
    decimal TotalPostgraduateLoans { get; }

    /// <summary>
    /// Gets the total amount of employer's National Insurance for the tax month. May be zero.
    /// </summary>
    decimal EmployerNiTotal { get; }

    /// <summary>
    /// Gets the total amount employee's National Insurance for the tax month. May be zero.
    /// </summary>
    decimal EmployeeNiTotal { get; }

    /// <summary>
    /// Gets the applicable month number for this year-to-date entry.
    /// </summary>
    int MonthNumber { get; }

    /// <summary>
    /// Gets the total Statutory Maternity Pay amount for the tax month.  May be zero.
    /// </summary>
    decimal TotalYtdSMP { get; }

    /// <summary>
    /// Gets the total Statutory Paternity Pay amount for the tax month.  May be zero.
    /// </summary>
    decimal TotalYtdSPP { get; }

    /// <summary>
    /// Gets the total Statutory Adoption Pay amount for the tax month.  May be zero.
    /// </summary>
    decimal TotalYtdSAP { get; }

    /// <summary>
    /// Gets the total Statutory Shared Parental Pay amount for the tax month.  May be zero.
    /// </summary>
    decimal TotalYtdShPP { get; }

    /// <summary>
    /// Gets the total Statutory Parental Bereavement Pay amount for the tax month.  May be zero.
    /// </summary>
    decimal TotalYtdSPBP { get; }

    /// <summary>
    /// Applies the supplied pay run summary to this history entry and returns a new updated <see cref="EmployerYtdHistoryEntry"/>.
    /// </summary>
    /// <param name="summary">Pay run summary to apply.</param>
    /// <returns>New <see cref="EmployerYtdHistoryEntry"/> with the supplied pay run summary applied.</returns>
    EmployerYtdHistoryEntry Apply(IPayRunSummary summary);

    /// <summary>
    /// Undoes the previous application of a pay run summary on this history entry and returns a new updated <see cref="EmployerYtdHistoryEntry"/>.
    /// </summary>
    /// <param name="summary">Pay run summary to un-apply.</param>
    /// <returns>New <see cref="EmployerYtdHistoryEntry"/> with the supplied pay run summary un-applied.</returns>
    EmployerYtdHistoryEntry UndoApply(IPayRunSummary summary);
}