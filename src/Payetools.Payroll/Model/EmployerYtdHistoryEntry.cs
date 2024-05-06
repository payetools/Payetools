// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

namespace Payetools.Payroll.Model;

/// <summary>
/// Represents summarised pay run information across all pay runs year-to-date.
/// </summary>`
public class EmployerYtdHistoryEntry : IEmployerYtdHistoryEntry
{
    /// <summary>
    /// Gets the applicable month number for this year-to-date entry.
    /// </summary>
    public int MonthNumber { get; init; }

    /// <summary>
    /// Gets the total amount of income tax for the tax month. May be zero.
    /// </summary>
    public decimal TotalIncomeTax { get; init; }

    /// <summary>
    /// Gets the total amount of student loan repayment for the tax month. May be zero.
    /// </summary>
    public decimal TotalStudentLoans { get; init; }

    /// <summary>
    /// Gets the total amount of postgraduate loan repayment for the tax month. May be zero.
    /// </summary>
    public decimal TotalPostgraduateLoans { get; init; }

    /// <summary>
    /// Gets the total amount of employer's National Insurance for the tax month. May be zero.
    /// </summary>
    public decimal EmployerNiTotal { get; init; }

    /// <summary>
    /// Gets the total amount employee's National Insurance for the tax month. May be zero.
    /// </summary>
    public decimal EmployeeNiTotal { get; init; }

    /// <summary>
    /// Gets the total Statutory Maternity Pay amount for the month. May be zero.
    /// </summary>
    public decimal TotalYtdSMP { get; init; }

    /// <summary>
    /// Gets the total Statutory Paternity Pay amount for the tax month. May be zero.
    /// </summary>
    public decimal TotalYtdSPP { get; init; }

    /// <summary>
    /// Gets the total Statutory Adoption Pay amount for the tax month. May be zero.
    /// </summary>
    public decimal TotalYtdSAP { get; init; }

    /// <summary>
    /// Gets the total Statutory Shared Parental Pay amount for the tax month. May be zero.
    /// </summary>
    public decimal TotalYtdShPP { get; init; }

    /// <summary>
    /// Gets the total Statutory Parental Bereavement Pay amount for the tax month. May be zero.
    /// </summary>
    public decimal TotalYtdSPBP { get; init; }

    /// <summary>
    /// Applies the supplied pay run summary to this history entry and returns a new updated <see cref="EmployerYtdHistoryEntry"/>.
    /// </summary>
    /// <param name="summary">Pay run summary to apply.</param>
    /// <returns>New <see cref="EmployerYtdHistoryEntry"/> with the supplied pay run summary applied.</returns>
    public EmployerYtdHistoryEntry Apply(IPayRunSummary summary) =>
        new EmployerYtdHistoryEntry
        {
            MonthNumber = MonthNumber,
            TotalIncomeTax = TotalIncomeTax + summary.IncomeTaxTotal,
            TotalStudentLoans = TotalStudentLoans + summary.StudentLoansTotal,
            TotalPostgraduateLoans = TotalPostgraduateLoans + summary.PostgraduateLoansTotal,
            EmployerNiTotal = EmployerNiTotal + summary.EmployerNiTotal,
            EmployeeNiTotal = EmployeeNiTotal + summary.EmployeeNiTotal,
            TotalYtdSMP = TotalYtdSMP + summary.StatutoryMaternityPayTotal,
            TotalYtdSPP = TotalYtdSPP + summary.StatutoryPaternityPayTotal,
            TotalYtdSAP = TotalYtdSAP + summary.StatutoryAdoptionPayTotal,
            TotalYtdShPP = TotalYtdShPP + summary.StatutorySharedParentalPayTotal,
            TotalYtdSPBP = TotalYtdSPBP + summary.StatutoryParentalBereavementPayTotal
        };

    /// <summary>
    /// Undoes the previous application of a pay run summary on this history entry and returns a new updated <see cref="EmployerYtdHistoryEntry"/>.
    /// </summary>
    /// <param name="summary">Pay run summary to un-apply.</param>
    /// <returns>New <see cref="EmployerYtdHistoryEntry"/> with the supplied pay run summary un-applied.</returns>
    public EmployerYtdHistoryEntry UndoApply(IPayRunSummary summary) =>
        new EmployerYtdHistoryEntry
        {
            MonthNumber = MonthNumber,
            TotalIncomeTax = TotalIncomeTax - summary.IncomeTaxTotal,
            TotalStudentLoans = TotalStudentLoans - summary.StudentLoansTotal,
            TotalPostgraduateLoans = TotalPostgraduateLoans - summary.PostgraduateLoansTotal,
            EmployerNiTotal = EmployerNiTotal - summary.EmployerNiTotal,
            EmployeeNiTotal = EmployeeNiTotal - summary.EmployeeNiTotal,
            TotalYtdSMP = TotalYtdSMP - summary.StatutoryMaternityPayTotal,
            TotalYtdSPP = TotalYtdSPP - summary.StatutoryPaternityPayTotal,
            TotalYtdSAP = TotalYtdSAP - summary.StatutoryAdoptionPayTotal,
            TotalYtdShPP = TotalYtdShPP - summary.StatutorySharedParentalPayTotal,
            TotalYtdSPBP = TotalYtdSPBP - summary.StatutoryParentalBereavementPayTotal
        };

    /// <summary>
    /// Creates a new <see cref="EmployerYtdHistoryEntry"/> from the supplied pay run summary. This method is used when
    /// the supplied pay run is the first pay run to be applied for the month number.
    /// </summary>
    /// <param name="monthNumber">Applicable month number.</param>
    /// <param name="summary">Pay run summary to use as basis for the result.</param>
    /// <returns>A new <see cref="EmployerYtdHistoryEntry"/> with the pay run summary results applied.</returns>
    public static EmployerYtdHistoryEntry FromPayRun(int monthNumber, IPayRunSummary summary) =>
        new EmployerYtdHistoryEntry
        {
            MonthNumber = monthNumber,
            TotalIncomeTax = summary.IncomeTaxTotal,
            TotalStudentLoans = summary.StudentLoansTotal,
            TotalPostgraduateLoans = summary.PostgraduateLoansTotal,
            EmployerNiTotal = summary.EmployerNiTotal,
            EmployeeNiTotal = summary.EmployeeNiTotal,
            TotalYtdSMP = summary.StatutoryMaternityPayTotal,
            TotalYtdSPP = summary.StatutoryPaternityPayTotal,
            TotalYtdSAP = summary.StatutoryAdoptionPayTotal,
            TotalYtdShPP = summary.StatutorySharedParentalPayTotal,
            TotalYtdSPBP = summary.StatutoryParentalBereavementPayTotal
        };
}