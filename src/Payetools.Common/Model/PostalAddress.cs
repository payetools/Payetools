﻿// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

namespace Payetools.Common.Model;

/// <summary>
/// Represents a postal address.  If it is a UK address, <see cref="Postcode"/> should be supplied
/// and <see cref="ForeignCountry"/> set to null; if the address is non-UK, then Postcode should
/// be null and ForeignCountry should be provided.
/// </summary>
public class PostalAddress
{
    /// <summary>Gets the first line of the address.</summary>
    public string AddressLine1 { get; init; }

    /// <summary>Gets the second line of the address.</summary>
    public string AddressLine2 { get; init; }

    /// <summary>Gets the third line of the address.</summary>
    public string? AddressLine3 { get; init; }

    /// <summary>Gets the fourth line of the address.</summary>
    public string? AddressLine4 { get; init; }

    /// <summary>Gets the postcode (UK addresses only).</summary>
    public UkPostcode? Postcode { get; init; }

    /// <summary>Gets the foreign country (non-UK addresses only).</summary>
    public string? ForeignCountry { get; init; }

    /// <summary>
    /// Initialises a new instance of <see cref="PostalAddress"/>.
    /// </summary>
    /// <param name="addressLine1">First line of the address. Mandatory.</param>
    /// <param name="addressLine2">Second line of the address. Mandatory.</param>
    /// <param name="addressLine3">Third line of the address. Optional, i.e., may be null.</param>
    /// <param name="addressLine4">Fourth line of the address. Optional, i.e., may be null.</param>
    /// <param name="postcode">Postcode for UK addresses only.  Should be set to null for overseas
    /// addresses.</param>
    /// <param name="foreignCountry">Foreign country for overseas addresses.  Should be null for UK
    /// addresses.</param>
    public PostalAddress(
        string addressLine1,
        string addressLine2,
        string? addressLine3,
        string? addressLine4,
        UkPostcode? postcode,
        string? foreignCountry)
    {
        AddressLine1 = addressLine1;
        AddressLine2 = addressLine2;
        AddressLine3 = addressLine3;
        AddressLine4 = addressLine4;
        Postcode = postcode;
        ForeignCountry = foreignCountry;
    }
}