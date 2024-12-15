// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;

namespace Payetools.Payroll.Model;

/// <summary>
/// Represents the summary of a pay run.
/// </summary>
/// <remarks>Added Statutory Neonatal Care Pay from April 2025.</remarks>
public class PayRunSummary : IPayRunSummary
{
    /// <summary>
    /// Gets the pay date that this summary was paid on.
    /// </summary>
    public PayDate PayDate { get; init; }

    /// <summary>
    /// Gets the total amount of income tax for the pay run, if any. Zero otherwise.
    /// </summary>
    public decimal IncomeTaxTotal { get; init; }

    /// <summary>
    /// Gets the total amount of student loan repayment for the pay run, if any. Zero otherwise.
    /// </summary>
    public decimal StudentLoansTotal { get; init; }

    /// <summary>
    /// Gets the total amount of postgraduate loan repayment for the pay run, if any. Zero otherwise.
    /// </summary>
    public decimal PostgraduateLoansTotal { get; init; }

    /// <summary>
    /// Gets the total amount of employer's National Insurance for the pay run, if any. Zero otherwise.
    /// </summary>
    public decimal EmployerNiTotal { get; init; }

    /// <summary>
    /// Gets the total amount employee's National Insurance for the pay run, if any. Zero otherwise.
    /// </summary>
    public decimal EmployeeNiTotal { get; init; }

    /// <summary>
    /// Gets the total Statutory Maternity Pay amount for the pay run, if any. Zero otherwise.
    /// </summary>
    public decimal StatutoryMaternityPayTotal { get; init; }

    /// <summary>
    /// Gets the total Statutory Paternity Pay amount for the pay run, if any. Zero otherwise.
    /// </summary>
    public decimal StatutoryPaternityPayTotal { get; init; }

    /// <summary>
    /// Gets the totalStatutory Adoption Pay amount for the pay run, if any. Zero otherwise.
    /// </summary>
    public decimal StatutoryAdoptionPayTotal { get; init; }

    /// <summary>
    /// Gets the total Statutory Shared Parental Pay amount for the pay run, if any. Zero otherwise.
    /// </summary>
    public decimal StatutorySharedParentalPayTotal { get; init; }

    /// <summary>
    /// Gets the total Statutory Parental Bereavement Pay amount for the pay run, if any. Zero otherwise.
    /// </summary>
    public decimal StatutoryParentalBereavementPayTotal { get; init; }

    /// <summary>
    /// Gets the total Statutory Neonatal Care Pay amount for the pay run, if any. Zero otherwise.
    /// </summary>
    public decimal StatutoryNeonatalCarePayTotal { get; init; }
}
