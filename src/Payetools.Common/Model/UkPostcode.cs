// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

namespace Payetools.Common.Model;

/// <summary>
/// Represents a UK Postcode.
/// </summary>
public record UkPostcode
{
    // ^(([A-Z]{1,2}\d[A-Z\d]?|ASCN|STHL|TDCU|BBND|[BFS]IQQ|PCRN|TKCA) ?\d[A-Z]{2}|BFPO ?\d{1,4}|(KY\d|MSR|VG|AI)[ -]?\d{4}|[A-Z]{2} ?\d{2}|GE ?CX)$

    private readonly string _value;

    /// <summary>
    /// Operator for casting implicitly from a <see cref="UkPostcode"/> instance to its string equivalent.
    /// </summary>
    /// <param name="value">An instance of UkPostcode.</param>
    public static implicit operator string(UkPostcode value) => value._value;

    /// <summary>
    /// Initialises a new instance of <see cref="UkPostcode"/>.
    /// </summary>
    /// <param name="value">Postcode as string.</param>
    public UkPostcode(string value)
    {
        _value = value;
    }
}
