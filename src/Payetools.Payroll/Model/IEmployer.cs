// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;
using System.Collections.Immutable;

namespace Payetools.Payroll.Model;

/// <summary>
/// Interface that represents an employer for payroll purposes.
/// </summary>
public interface IEmployer
{
    /// <summary>
    /// Gets the official or legal name of the business, including any official suffix, e.g., Ltd, LLP, etc.
    /// </summary>
    string? OfficialName { get; }

    /// <summary>
    /// Gets the name that the business is known by, omitting any official suffix, e.g., Ltd, LLP, etc.
    /// </summary>
    string KnownAsName { get; }

    /// <summary>
    /// Gets the employer's HMRC PAYE reference, if known.
    /// </summary>
    HmrcPayeReference? HmrcPayeReference { get; }

    /// <summary>
    /// Gets the employer's HMRC Accounts Office reference, if known.
    /// </summary>
    HmrcAccountsOfficeReference? AccountsOfficeReference { get; }

    /// <summary>
    /// Gets the employer's Corporation Tax reference, if known.
    /// </summary>
    string? HmrcCorporationTaxReference { get; }

    /// <summary>
    /// Gets an array of entries, one entry for each tax year of record.
    /// </summary>
    ImmutableArray<EmploymentAllowanceHistoryEntry> EmploymentAllowanceEligibilities { get; }
}