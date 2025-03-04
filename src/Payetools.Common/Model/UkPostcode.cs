// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using System.Text.RegularExpressions;

namespace Payetools.Common.Model;

/// <summary>
/// Represents a UK Postcode.
/// </summary>
public partial class UkPostcode
{
    [GeneratedRegex(@"^(([A-Z]{1,2}\d[A-Z\d]?|ASCN|STHL|TDCU|BBND|[BFS]IQQ|PCRN|TKCA) ?\d[A-Z]{2}|BFPO ?\d{1,4}|(KY\d|MSR|VG|AI)[ -]?\d{4}|[A-Z]{2} ?\d{2}|GE ?CX)$")]
    private static partial Regex GetValidationRegex();

    private readonly string _value;

    /// <summary>
    /// Operator for casting implicitly from a <see cref="UkPostcode"/> instance to its string equivalent.
    /// </summary>
    /// <param name="value">An instance of UkPostcode.</param>
    public static implicit operator string(in UkPostcode value) => value._value;

    /// <summary>
    /// Operator for casting implicitly from a string to a <see cref="UkPostcode"/>.
    /// </summary>
    /// <param name="value">A valid string representation of a UK postcode.</param>
    public static implicit operator UkPostcode(in string value) => new UkPostcode(value);

    /// <summary>
    /// Initialises a new instance of <see cref="UkPostcode"/>.
    /// </summary>
    /// <param name="value">Postcode as string.</param>
    /// <param name="validate">Set to false to disable postcode validation. Defaults to true.</param>
    public UkPostcode(in string value, bool validate = true)
    {
        var postcode = value.ToUpperInvariant().Trim();

        if (validate && !IsValid(postcode))
            throw new ArgumentException($"Argument '{value}' is not a valid UK postcode", nameof(value));

        _value = postcode;
    }

    /// <summary>
    /// Verifies whether the supplied string could be a valid UK postcode.
    /// </summary>
    /// <param name="value">String value to check.</param>
    /// <returns>True if the supplied value could be a valid UK postcode; false otherwise.</returns>
    /// <remarks>Although this method confirms whether the string supplied <em>could</em> be a valid UK
    /// postcode, it does not guarantee that the supplied value is an actual postcode.</remarks>
    public static bool IsValid(in string value) => GetValidationRegex().IsMatch(value);
}
