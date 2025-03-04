// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using System.Text.RegularExpressions;

namespace Payetools.Common.Model;

/// <summary>
/// Represents a Uk National Insurance number, also referred to as NI number or NINO.  A NINO is made up of 2 letters, 6 numbers
/// and a final letter which is always A, B, C or D.  (See HMRC's National Insurance Manual, section NIM39110
/// (<see href="https://www.gov.uk/hmrc-internal-manuals/national-insurance-manual/nim39110"/>.)
/// </summary>
/// <remarks>Marked partial as using <em>GeneratedRegexAttribute</em> for .NET 7.0 or above.</remarks>
public readonly partial struct NiNumber
{
    private const string UNKNOWN = "UNKNOWN";

    [GeneratedRegex("^(?!BG)(?!GB)(?!NK)(?!KN)(?!TN)(?!NT)(?!ZZ)(?:[A-CEGHJ-PR-TW-Z][A-CEGHJ-NPR-TW-Z])(?:\\s*\\d\\s*){6}([A-D]|\\s)$")]
    private static partial Regex GetValidationRegex();

    private readonly string _value;

    /// <summary>
    /// Gets a value indicating whether this <see cref="NiNumber"/> instance represents an unknown National
    /// Insurance number.
    /// </summary>
    public bool IsUnknown => _value == UNKNOWN;

    private static readonly NiNumber _unknown = new NiNumber(true);

    /// <summary>
    /// Gets the NiNumber to be used when an employee does not know their National Insurance number.
    /// </summary>
    public static NiNumber Unknown => _unknown;

    private NiNumber(in bool isUnknown)
    {
        if (!isUnknown)
            throw new ArgumentException("This constructor can only be used for unknown NI numbers", nameof(isUnknown));

        _value = UNKNOWN;
    }

    /// <summary>
    /// Initialises a new <see cref="NiNumber"/> instance.
    /// </summary>
    /// <param name="niNumber">National insurance number as string.</param>
    /// <exception cref="ArgumentException">Thrown if the supplied string is not a valid NI number.</exception>
    public NiNumber(in string niNumber)
    {
        var tidiedNiNumber = niNumber.ToUpperInvariant().Replace(" ", string.Empty);

        if (!IsValid(tidiedNiNumber))
            throw new ArgumentException("Argument is not a valid National Insurance Number", nameof(niNumber));

        _value = tidiedNiNumber;
    }

    /// <summary>
    /// Operator for casting implicitly from a <see cref="NiNumber"/> instance to its string representation.
    /// </summary>
    /// <param name="niNumber">NI number.</param>
    public static implicit operator string(in NiNumber niNumber) => niNumber._value;

    /// <summary>
    /// Operator for casting implicitly from a string to an instance of a <see cref="NiNumber"/>.
    /// </summary>
    /// <param name="niNumber">NI number as string.</param>
    /// <exception cref="ArgumentException">Thrown if the supplied string is not a valid NI number.</exception>
    public static implicit operator NiNumber(in string niNumber) => new NiNumber(niNumber);

    /// <summary>
    /// Verifies whether the supplied string could be a valid NI number.
    /// </summary>
    /// <param name="value">String value to check.</param>
    /// <returns>True if the supplied value could be a valid NI number; false otherwise.</returns>
    /// <remarks>Although this method confirms whether the string supplied <em>could</em> be a valid Ni nummber,
    /// it does not guarantee that the supplied value is registered with HMRC against a given individual.</remarks>
    public static bool IsValid(in string value) => GetValidationRegex().IsMatch(value);

    /// <summary>
    /// Gets the string representation of this <see cref="NiNumber"/>.
    /// </summary>
    /// <returns>String representation of this NI number in non-spaced format.</returns>
    public override string ToString() => _value;

    /// <summary>
    /// Gets the string representation of this <see cref="NiNumber"/>.
    /// </summary>
    /// <param name="asSpacedFormat">True if spaced format is to be returned (e.g., "NA 12 34 67 C";
    /// false otherwise.</param>
    /// <returns>String representation of this NI number in either spaced or non-spaced format.</returns>
    public string ToString(in bool asSpacedFormat) =>
        asSpacedFormat ? ToSpacedFormat(_value) : _value;

    private static string ToSpacedFormat(in string value) =>
        value.Length == 9 ? $"{value[..2]} {value[2..4]} {value[4..6]} {value[6..8]} {value[^1]}" :
            throw new InvalidOperationException("NI numbers must be 9 characters in length");
}