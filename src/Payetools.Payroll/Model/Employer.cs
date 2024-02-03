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
public record Employer : IEmployer
{
    /// <summary>
    /// Gets or sets the legal name of the business, including any legally required suffix, e.g., Ltd, LLP, etc.
    /// </summary>
    public string OfficialName { get; set; } = default!;

    /// <summary>
    /// Gets or sets the employer's HMRC PAYE reference, if known.
    /// </summary>
    public HmrcPayeReference? HmrcPayeReference { get; set; }

    /// <summary>
    /// Gets or sets the employer's HMRC Accounts Office reference, if known.
    /// </summary>
    public HmrcAccountsOfficeReference? AccountsOfficeReference { get; set; }
}