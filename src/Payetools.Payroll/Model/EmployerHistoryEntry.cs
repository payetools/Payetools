// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

namespace Payetools.Payroll.Model;

/// <summary>
/// Represents summarised pay run information across all pay runs for a given tax month.
/// </summary>`
public class EmployerHistoryEntry : IEmployerHistoryEntry
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
    public decimal TotalStatutoryMaternityPay { get; init; }

    /// <summary>
    /// Gets the total Statutory Paternity Pay amount for the tax month. May be zero.
    /// </summary>
    public decimal TotalStatutoryPaternityPay { get; init; }

    /// <summary>
    /// Gets the total Statutory Adoption Pay amount for the tax month. May be zero.
    /// </summary>
    public decimal TotalStatutoryAdoptionPay { get; init; }

    /// <summary>
    /// Gets the total Statutory Shared Parental Pay amount for the tax month. May be zero.
    /// </summary>
    public decimal TotalStatutorySharedParentalPay { get; init; }

    /// <summary>
    /// Gets the total Statutory Parental Bereavement Pay amount for the tax month. May be zero.
    /// </summary>
    public decimal TotalStatutoryParentalBereavementPay { get; init; }

    /// <summary>
    /// Applies the supplied pay run summary to this history entry and returns a new updated <see cref="EmployerHistoryEntry"/>.
    /// </summary>
    /// <param name="summary">Pay run summary to apply.</param>
    /// <returns>New <see cref="EmployerHistoryEntry"/> with the supplied pay run summary applied.</returns>
    public EmployerHistoryEntry Apply(IPayRunSummary summary) =>
        new EmployerHistoryEntry
        {
            MonthNumber = MonthNumber,
            TotalIncomeTax = TotalIncomeTax + summary.IncomeTaxTotal,
            TotalStudentLoans = TotalStudentLoans + summary.StudentLoansTotal,
            TotalPostgraduateLoans = TotalPostgraduateLoans + summary.PostgraduateLoansTotal,
            EmployerNiTotal = EmployerNiTotal + summary.EmployerNiTotal,
            EmployeeNiTotal = EmployeeNiTotal + summary.EmployeeNiTotal,
            TotalStatutoryMaternityPay = TotalStatutoryMaternityPay + summary.StatutoryMaternityPayTotal,
            TotalStatutoryPaternityPay = TotalStatutoryPaternityPay + summary.StatutoryPaternityPayTotal,
            TotalStatutoryAdoptionPay = TotalStatutoryAdoptionPay + summary.StatutoryAdoptionPayTotal,
            TotalStatutorySharedParentalPay = TotalStatutorySharedParentalPay + summary.StatutorySharedParentalPayTotal,
            TotalStatutoryParentalBereavementPay = TotalStatutoryParentalBereavementPay + summary.StatutoryParentalBereavementPayTotal
        };

    /// <summary>
    /// Undoes the previous application of a pay run summary on this history entry and returns a new updated <see cref="EmployerHistoryEntry"/>.
    /// </summary>
    /// <param name="summary">Pay run summary to un-apply.</param>
    /// <returns>New <see cref="EmployerHistoryEntry"/> with the supplied pay run summary un-applied.</returns>
    public EmployerHistoryEntry UndoApply(IPayRunSummary summary) =>
        new EmployerHistoryEntry
        {
            MonthNumber = MonthNumber,
            TotalIncomeTax = TotalIncomeTax - summary.IncomeTaxTotal,
            TotalStudentLoans = TotalStudentLoans - summary.StudentLoansTotal,
            TotalPostgraduateLoans = TotalPostgraduateLoans - summary.PostgraduateLoansTotal,
            EmployerNiTotal = EmployerNiTotal - summary.EmployerNiTotal,
            EmployeeNiTotal = EmployeeNiTotal - summary.EmployeeNiTotal,
            TotalStatutoryMaternityPay = TotalStatutoryMaternityPay - summary.StatutoryMaternityPayTotal,
            TotalStatutoryPaternityPay = TotalStatutoryPaternityPay - summary.StatutoryPaternityPayTotal,
            TotalStatutoryAdoptionPay = TotalStatutoryAdoptionPay - summary.StatutoryAdoptionPayTotal,
            TotalStatutorySharedParentalPay = TotalStatutorySharedParentalPay - summary.StatutorySharedParentalPayTotal,
            TotalStatutoryParentalBereavementPay = TotalStatutoryParentalBereavementPay - summary.StatutoryParentalBereavementPayTotal
        };
}