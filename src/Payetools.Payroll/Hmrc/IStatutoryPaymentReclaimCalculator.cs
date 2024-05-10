// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Payroll.Model;

namespace Payetools.Payroll.Hmrc;

/// <summary>
/// Interface for types that provide statutory payment reclaim calculations.
/// </summary>
public interface IStatutoryPaymentReclaimCalculator
{
    /// <summary>
    /// Calculates the allowable reclaim amounts for all reclaimable statutory payments.
    /// </summary>
    /// <param name="employer">Employer that this calculation pertains to.</param>
    /// <param name="employerMonthEntry">Aggregated month's figures for a given employer.</param>
    /// <param name="reclaim">New instance of object that implements <see cref="IEmployerHistoryEntry"/> containing
    /// the reclaimable amounts for each statutory payment.</param>
    void Calculate(
        in IEmployer employer,
        in IEmployerHistoryEntry employerMonthEntry,
        out IStatutoryPaymentReclaim reclaim);
}