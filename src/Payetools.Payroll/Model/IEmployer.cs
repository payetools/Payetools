// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;

namespace Payetools.Payroll.Model;

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