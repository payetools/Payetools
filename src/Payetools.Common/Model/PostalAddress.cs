// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

namespace Payetools.Common.Model;

/// <summary>
/// Represents a postal address.  If it is a UK address, <see cref="Postcode"/> should be supplied
/// <see cref="ForeignCountry"/> set to null; if the address is non-UK, then Postcode should
/// be null and ForeignCountry should be provided.
/// </summary>
public class PostalAddress
{
    /// <summary>Gets or sets the first line of the address.</summary>
    public string AddressLine1 { get; set; } = null!;

    /// <summary>Gets or sets the second line of the address.</summary>
    public string AddressLine2 { get; set; } = null!;

    /// <summary>Gets or sets the third line of the address.</summary>
    public string? AddressLine3 { get; set; }

    /// <summary>Gets or sets the fourth line of the address.</summary>
    public string? AddressLine4 { get; set; }

    /// <summary>Gets or sets the postcode (UK addresses only).</summary>
    public UkPostcode? Postcode { get; set; }

    /// <summary>Gets or sets the TBA.</summary>
    public string? ForeignCountry { get; set; }
}