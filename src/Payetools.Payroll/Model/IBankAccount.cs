// Copyright (c) 2023-2025, Payetools Ltd.
//
// Payetools Ltd licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

namespace Payetools.Payroll.Model;

/// <summary>
/// Interface that represents a UK bank account.
/// </summary>
public interface IBankAccount
{
    /// <summary>
    /// Gets the account holder name.
    /// </summary>
    string AccountName { get; }

    /// <summary>
    /// Gets the account number. Should be 8 characters, all numeric, with leading zeroes where appropriate.
    /// </summary>
    string AccountNumber { get; }

    /// <summary>
    /// Gets the sort code for the account.  Should be 6 characters, all numeric, with leading zeroes where
    /// appropriate.
    /// </summary>
    string SortCode { get; }

    /// <summary>
    /// Gets the optional building society reference, where appropriate.
    /// </summary>
    string? BuildingSocietyReference { get; }
}