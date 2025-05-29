// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

namespace Payetools.NationalInsurance.Model;

/// <summary>
/// Interface that provides access to an employee's year to date National Insurance history.
/// </summary>
public interface INiYtdHistory
{
    /// <summary>
    /// Gets the value of any Class 1A National Insurance contributions payable year to date.
    /// </summary>
    decimal? Class1ANicsYtd { get; }

    /// <summary>
    /// Gets the totals of employee and employer NI contributions paid to date across all entries.
    /// </summary>
    /// <returns>Totals of employee and employer NI contributions paid tear to date.</returns>
    (decimal employeeTotal, decimal employerTotal) GetNiYtdTotals();
}