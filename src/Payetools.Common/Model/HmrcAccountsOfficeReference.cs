﻿// Copyright (c) 2023-2025, Payetools Ltd.
//
// Payetools Ltd licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using System.Text.RegularExpressions;

namespace Payetools.Common.Model;

/// <summary>
/// Represents an HMRC Accounts Office Reference, as defined at
/// <see href="https://design.tax.service.gov.uk/hmrc-design-patterns/accounts-office-reference/"/>,
/// which is formatted as follows:
/// <list type="bullet">
///     <item><description>3 numbers</description></item>
///     <item><description>The letter 'P'</description></item>
///     <item><description>Another letter, A-Z</description></item>
///     <item><description>8 numbers, or 7 numbers and the letter ‘X’</description></item>
/// </list>.
/// </summary>
public partial record HmrcAccountsOfficeReference
{
    [GeneratedRegex(@"^[0-9]{3}P[A-Z]\d{7}[0-9X]$")]
    private static partial Regex GetValidationRegex();

    private readonly string _accountsOfficeReference;

    /// <summary>
    /// Operator for casting implicitly from a <see cref="HmrcAccountsOfficeReference"/> instance to its string equivalent.
    /// </summary>
    /// <param name="value">An instance of HmrcAccountsOfficeReference.</param>
    public static implicit operator string(in HmrcAccountsOfficeReference value) => value.ToString();

    /// <summary>
    /// Operator for casting implicitly from a string to a <see cref="HmrcAccountsOfficeReference"/>.
    /// </summary>
    /// <param name="value">A string that can be converted to a HmrcAccountsOfficeReference.</param>
    /// <exception cref="ArgumentException">Thrown if the supplied string is not a valid HMRC accounts
    /// office reference.</exception>
    public static implicit operator HmrcAccountsOfficeReference(in string value) => new HmrcAccountsOfficeReference(value);

    /// <summary>
    /// Initialises a new instance of <see cref="HmrcAccountsOfficeReference"/>.
    /// </summary>
    /// <param name="accountsOfficeReference">String value containing the HMRC Accounts Office Reference.</param>
    /// <exception cref="ArgumentException">Thrown if the supplied string value does not match the required pattern
    /// for valid HMRC Accounts Office Reference values.</exception>
    public HmrcAccountsOfficeReference(in string accountsOfficeReference)
    {
        var aor = accountsOfficeReference.ToUpperInvariant();

        if (!IsValid(aor))
            throw new ArgumentException("Argument is not a valid Accounts Office Reference", nameof(accountsOfficeReference));

        _accountsOfficeReference = aor;
    }

    /// <summary>
    /// Verifies whether the supplied string could be a valid HRMC Accounts Office Reference.
    /// </summary>
    /// <param name="value">String value to check.</param>
    /// <returns>True if the supplied value could be a valid HMRC Accounts Office Reference; false otherwise.</returns>
    /// <remarks>Although this method confirms whether the string supplied <em>could</em> be a valid HRMC Accounts Office
    /// Reference, it does not guarantee that the supplied value is registered with HMRC against a given company.</remarks>
    public static bool IsValid(in string value) => GetValidationRegex().IsMatch(value);

    /// <summary>
    /// Gets the string representation of this HmrcAccountsOfficeReference.
    /// </summary>
    /// <returns>The value of this <see cref="HmrcAccountsOfficeReference"/> as a string.</returns>
    public override string ToString() => _accountsOfficeReference;
}