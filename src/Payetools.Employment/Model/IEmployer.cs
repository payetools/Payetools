// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Payetools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

using Payetools.Common.Model;

namespace Payetools.Employment.Model;

/// <summary>
/// Interface that represents an employer for payroll purposes.
/// </summary>
public interface IEmployer
{
    /// <summary>
    /// Gets the legal name of the business, including any legally required suffix, e.g., Ltd, LLP, etc.
    /// </summary>
    string BusinessLegalName { get; }

    /// <summary>
    /// Gets the employer's HMRC PAYE reference, if known.
    /// </summary>
    HmrcPayeReference? HmrcPayeReference { get; }

    /// <summary>
    /// Gets the employer's HMRC Accounts Office reference, if known.
    /// </summary>
    HmrcAccountsOfficeReference? AccountsOfficeReference { get; }
}