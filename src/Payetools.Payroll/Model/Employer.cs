// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;

namespace Payetools.Payroll.Model;

/// <summary>
/// Represents an employer for payroll purposes.
/// </summary>
public class Employer : IEmployer
{
    /// <summary>
    /// Gets the legal name of the business, including any legally required suffix, e.g., Ltd, LLP, etc.
    /// </summary>
    public string? OfficialName { get; init; }

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
    /// Gets the employer's Corporation Tax reference, if known.
    /// </summary>
    public string? HmrcCorporationTaxReference { get; }

    /// <summary>
    /// Gets a value indicating whether the employer is currently eligible for Employment Allowance.
    /// </summary>
    public bool IsEligibleForEmploymentAllowance { get; }

    /// <summary>
    /// Gets the applicable state aid qualifier for employment allowance.
    /// </summary>
    public StateAidForEmploymentAllowance? EmploymentAllowanceStateAidClassification { get; }

    /// <summary>
    /// Gets a value indicating whether the employer is eligible for Small Employers Relief.
    /// </summary>
    public bool IsEligibleForSmallEmployersRelief { get; init; }

    /// <summary>
    /// Gets a value indicating whether the employer must pay the Apprentice Levy.
    /// </summary>
    public bool IsApprenticeLevyDue { get; init; }

    /// <summary>
    /// Gets the annual allowance available to the employer if the Apprentice Levy is payable.
    /// </summary>
    public decimal? ApprenticeLevyAllowance { get; init; }

    /// <summary>
    /// Gets the employer's bank account used for HMRC refunds. May be null if unspecified.
    /// </summary>
    public IBankAccount? BankAccount { get; init; }

    /// <summary>
    /// Initialises a new <see cref="Employer"/> with the supplied parameters.
    /// </summary>
    /// <param name="officialName">Legal name of the business, including any legally required suffix, e.g., Ltd, LLP, etc.</param>
    /// <param name="knownAsName">Name that the business is known by, omitting any official suffix, e.g., Ltd, LLP, etc.</param>
    /// <param name="hmrcPayeReference">Employer's HMRC PAYE reference, if known.
    /// Optional.</param>
    /// <param name="accountsOfficeReference">Employer's HMRC Accounts Office reference, if known. Optional.</param>
    /// <param name="corporationTaxReference">Employer's HMRC Corporation Tax reference, if known. Optional. </param>
    /// <param name="isEligibleForEmploymentAllowance">Indicates whether the employer is currently eligible to claim
    /// Employment Allowance.  Defaults to false.</param>
    /// <param name="employmentAllowanceStateAidClassification">Where applicable, the employer's classification in terms of
    /// state aid. Defaults to none (null).</param>
    /// <param name="isEligibleForSmallEmployersRelief">Indicates whether the employer is currently eligible to claim
    /// Small Employers Relief.  Defaults to false.</param>
    /// <param name="bankAccount">Employer's bank account details to be used in the case of HMRC refunds/repayments.
    /// Defaults to none (null).</param>
    public Employer(
        string? officialName,
        string knownAsName,
        HmrcPayeReference? hmrcPayeReference = null,
        HmrcAccountsOfficeReference? accountsOfficeReference = null,
        string? corporationTaxReference = null,
        bool isEligibleForEmploymentAllowance = false,
        StateAidForEmploymentAllowance? employmentAllowanceStateAidClassification = null,
        bool isEligibleForSmallEmployersRelief = false,
        IBankAccount? bankAccount = null)
    {
        OfficialName = officialName;
        KnownAsName = knownAsName;
        HmrcPayeReference = hmrcPayeReference;
        AccountsOfficeReference = accountsOfficeReference;
        HmrcCorporationTaxReference = corporationTaxReference;
        IsEligibleForEmploymentAllowance = isEligibleForEmploymentAllowance;
        EmploymentAllowanceStateAidClassification = employmentAllowanceStateAidClassification;
        IsEligibleForSmallEmployersRelief = isEligibleForSmallEmployersRelief;
        BankAccount = bankAccount;
    }
}