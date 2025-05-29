// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.NationalInsurance.Model;

namespace Payetools.Payroll.Model;

/// <summary>
/// Provides access to the core year-to-date figures for an employee's payroll
/// for the current tax year.
/// </summary>
public interface IEmployeeCoreYtdFigures
{
    /// <summary>
    /// Gets the taxable pay paid to date this tax year.
    /// </summary>
    decimal TaxablePayYtd { get; init; }

    /// <summary>
    /// Gets the income tax paid to date this tax year.
    /// </summary>
    decimal TaxPaidYtd { get; init; }

    /// <summary>
    /// Gets the tax that it has not been possible to collect so far this tax year due to the
    /// regulatory limit on income tax deductions.
    /// </summary>
    decimal TaxUnpaidDueToRegulatoryLimit { get; init; }

    /// <summary>
    /// Gets the NI-able pay paid to date this tax year.
    /// </summary>
    decimal NicablePayYtd { get; init; }

    /// <summary>
    /// Gets the National Insurance paid to date this tax year, by NI category.
    /// </summary>
    INiYtdHistory NiHistory { get; init; }
}