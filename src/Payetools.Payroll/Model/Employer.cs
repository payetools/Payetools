// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;
using System.Collections.Immutable;

namespace Payetools.Payroll.Model;

/// <summary>
/// Represents an employer for payroll purposes.
/// </summary>
public record Employer : IEmployer
{
    /// <summary>
    /// Gets the legal name of the business, including any legally required suffix, e.g., Ltd, LLP, etc.
    /// </summary>
    public string? OfficialName { get; init; } = default!;

    /// <summary>
    /// Gets the name that the business is known by, omitting any official suffix, e.g., Ltd, LLP, etc.
    /// </summary>
    public string KnownAsName { get; init; }

    /// <summary>
    /// Gets the employer's HMRC PAYE reference, if known.
    /// </summary>
    public HmrcPayeReference? HmrcPayeReference { get; init; }

    /// <summary>
    /// Gets the employer's HMRC Accounts Office reference, if known.
    /// </summary>
    public HmrcAccountsOfficeReference? AccountsOfficeReference { get; init; }

    /// <summary>
    /// Gets an array of entries, one entry for each tax year of record, that indicates whether the employer
    /// is eligible to claim Employment Allowance.
    /// </summary>
    public ImmutableArray<EmploymentAllowanceHistoryEntry> EmploymentAllowanceEligibilities { get; init; }

    /// <summary>
    /// Initialises a new <see cref="Employer"/> with the supplied parameters.
    /// </summary>
    /// <param name="officialName">Legal name of the business, including any legally required suffix, e.g., Ltd, LLP, etc.</param>
    /// <param name="knownAsName">Name that the business is known by, omitting any official suffix, e.g., Ltd, LLP, etc.</param>
    /// <param name="hmrcPayeReference">Employer's HMRC PAYE reference, if known.
    /// Optional.</param>
    /// <param name="accountsOfficeReference">Employer's HMRC Accounts Office reference, if known.  Optional.</param>
    /// <param name="employmentAllowanceEligibilities">Array of entries, one entry for each tax year of record, that indicates whether
    /// the employer is eligible to claim Employment Allowance.  May be null if not known.</param>
    public Employer(
        string? officialName,
        string knownAsName,
        HmrcPayeReference? hmrcPayeReference = null,
        HmrcAccountsOfficeReference? accountsOfficeReference = null,
        ImmutableArray<EmploymentAllowanceHistoryEntry>? employmentAllowanceEligibilities = null)
    {
        OfficialName = officialName;
        KnownAsName = knownAsName;
        HmrcPayeReference = hmrcPayeReference;
        AccountsOfficeReference = accountsOfficeReference;
        EmploymentAllowanceEligibilities = employmentAllowanceEligibilities ??
            ImmutableArray.Create<EmploymentAllowanceHistoryEntry>();
    }
}