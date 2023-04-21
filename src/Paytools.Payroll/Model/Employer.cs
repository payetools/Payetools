// Copyright (c) 2023 Paytools Foundation.
//
// Paytools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Paytools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

using Paytools.Common.Model;
using Paytools.Employment.Model;

namespace Paytools.Payroll.Model;

/// <summary>
/// Represents an employer for payroll purposes.
/// </summary>
public record Employer : IEmployer
{
    /// <summary>
    /// Gets or sets the legal name of the business, including any legally required suffix, e.g., Ltd, LLP, etc.
    /// </summary>
    public string BusinessLegalName { get; set; } = default!;

    /// <summary>
    /// Gets or sets the employer's HMRC PAYE reference, if known.
    /// </summary>
    public HmrcPayeReference? HmrcPayeReference { get; set; }

    /// <summary>
    /// Gets or sets the employer's HMRC Accounts Office reference, if known.
    /// </summary>
    public HmrcAccountsOfficeReference? AccountsOfficeReference { get; set; }
}