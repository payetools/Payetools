// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;

namespace Payetools.Payroll.Model;

/// <summary>
/// Interface that represents the summary of a pay run.
/// </summary>
public interface IPayRunSummary
{
    /// <summary>
    /// Gets the pay date that this summary was paid on.
    /// </summary>
    PayDate PayDate { get; }

    /// <summary>
    /// Gets the total amount of income tax for the pay run, if any. Zero otherwise.
    /// </summary>
    decimal IncomeTaxTotal { get; }

    /// <summary>
    /// Gets the total amount of student loan repayment for the pay run, if any. Zero otherwise.
    /// </summary>
    decimal StudentLoansTotal { get; }

    /// <summary>
    /// Gets the total amount of postgraduate loan repayment for the pay run, if any. Zero otherwise.
    /// </summary>
    decimal PostgraduateLoansTotal { get; }

    /// <summary>
    /// Gets the total amount of employer's National Insurance for the pay run, if any. Zero otherwise.
    /// </summary>
    decimal EmployerNiTotal { get; }

    /// <summary>
    /// Gets the total amount employee's National Insurance for the pay run, if any. Zero otherwise.
    /// </summary>
    decimal EmployeeNiTotal { get; }

    /// <summary>
    /// Gets the total Statutory Maternity Pay amount for the pay run, if any. Zero otherwise.
    /// </summary>
    decimal StatutoryMaternityPayTotal { get; }

    /// <summary>
    /// Gets the total Statutory Paternity Pay amount for the pay run, if any. Zero otherwise.
    /// </summary>
    decimal StatutoryPaternityPayTotal { get; }

    /// <summary>
    /// Gets the totalStatutory Adoption Pay amount for the pay run, if any. Zero otherwise.
    /// </summary>
    decimal StatutoryAdoptionPayTotal { get; }

    /// <summary>
    /// Gets the total Statutory Shared Parental Pay amount for the pay run, if any. Zero otherwise.
    /// </summary>
    decimal StatutorySharedParentalPayTotal { get; }

    /// <summary>
    /// Gets the total Statutory Parental Bereavement Pay amount for the pay run, if any. Zero otherwise.
    /// </summary>
    decimal StatutoryParentalBereavementPayTotal { get; }

    /// <summary>
    /// Gets the total Statutory Neonatal Care Pay amount for the pay run, if any. Zero otherwise.
    /// </summary>
    decimal StatutoryNeonatalCarePayTotal { get; }
}
