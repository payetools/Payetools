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
    /// Gets a value indicating whether the employer is currently eligible for Employment Allowance.
    /// </summary>
    bool IsEligibleForEmploymentAllowance { get; }

    /// <summary>
    /// Gets the applicable state aid qualifier for employment allowance.
    /// </summary>
    StateAidForEmploymentAllowance? EmploymentAllowanceStateAidClassification { get; }

    /// <summary>
    /// Gets a value indicating whether the employer is eligible for Small Employers Relief.
    /// </summary>
    bool IsEligibleForSmallEmployersRelief { get; }

    /// <summary>
    /// Gets a value indicating whether the employer must pay the Apprentice Levy.
    /// </summary>
    bool IsApprenticeLevyDue { get; }

    /// <summary>
    /// Gets the annual allowance available to the employer if the Apprentice Levy is payable.
    /// </summary>
    decimal? ApprenticeLevyAllowance { get; }

    /// <summary>
    /// Gets the employer's bank account used for HMRC refunds. May be null if unspecified.
    /// </summary>
    IBankAccount? BankAccount { get; }
}