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
public class PayRunSummary : IPayRunSummary
{
    /// <summary>
    /// Gets the pay date that this summary was paid on.
    /// </summary>
    public PayDate PayDate { get; }

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
}
