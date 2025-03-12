// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Payetools.Common.Model;

/// <summary>
/// Represents an HMRC PAYE Reference, as defined at
/// <see href="https://design.tax.service.gov.uk/hmrc-design-patterns/employer-paye-reference/"/>,
/// which is formatted as follows:
/// <list type="bullet">
///     <item><description>3 numbers</description></item>
///     <item><description>a forward slash ('/')</description></item>
///     <item><description>between 1 and 10 characters, which can be letters and numbers</description></item>
/// </list>
/// </summary>
public partial struct HmrcPayeReference
{
    [GeneratedRegex(@"^[0-9]{3}/[A-Z0-9]{1,10}$")]
    private static partial Regex GetPayeRefRegex();

    /// <summary>
    /// Gets or sets the HMRC office number portion of the full PAYE Reference.  Always 3 digits.
    /// </summary>
    public int HmrcOfficeNumber { get; set; }

    /// <summary>
    /// Gets or sets the PAYE reference portion of the full HMRC PAYE Reference, i.e., the portion on the right hand of '/'.
    /// </summary>
    public string EmployerPayeReference { get; set; }

    /// <summary>
    /// Initialises a new instance of <see cref="HmrcPayeReference"/> using the Office number and Employer PAYE reference
    /// elements.
    /// </summary>
    /// <param name="hmrcOfficeNumber">HMRC Office Number (always 3 digits).</param>
    /// <param name="employerPayeReference">Employer PAYE reference (the part after the '/').</param>
    public HmrcPayeReference(in int hmrcOfficeNumber, in string employerPayeReference)
    {
        HmrcOfficeNumber = hmrcOfficeNumber;
        EmployerPayeReference = employerPayeReference;
    }

    /// <summary>
    /// Operator for casting implicitly from a <see cref="HmrcPayeReference"/> instance to its string representation.
    /// </summary>
    /// <param name="value">An instance of HmrcPayeReference.</param>
    public static implicit operator string(in HmrcPayeReference value) => value.ToString();

    /// <summary>
    /// Operator for casting implicitly from a string to a <see cref="HmrcPayeReference"/>.
    /// </summary>
    /// <param name="value">String representation of Hmrc PAYE reference.</param>
    /// <exception cref="ArgumentException">Thrown if the supplied string is not a valid PAYE reference.</exception>
    public static implicit operator HmrcPayeReference(in string value) => HmrcPayeReference.Parse(value);

    /// <summary>
    /// Parses the supplied string into a <see cref="HmrcPayeReference"/>. If the supplied string cannot be parsed, then
    /// an <see cref="ArgumentException"/> is thrown, except when the supplied string is null, then an <see
    /// cref="ArgumentNullException"/> is thrown.
    /// </summary>
    /// <param name="input">String value containing candidate full HMRC PAYE Reference.  Lower case characters are converted to
    /// upper case.</param>
    /// <returns>A new <see cref="HmrcPayeReference"/> if a valid HMRC PAYE Reference was supplied.</returns>
    /// <exception cref="ArgumentNullException">Thrown if null was supplied.</exception>
    /// <exception cref="ArgumentException">Thrown if an invalid PAYE reference was supplied.</exception>
    public static HmrcPayeReference Parse(in string? input)
    {
        if (input == null) throw new ArgumentNullException(nameof(input), "PAYE reference may not be null");

        if (TryParse(input, out var payeReference))
            return (HmrcPayeReference)payeReference;

        throw new ArgumentException("PAYE reference is invalid");
    }

    /// <summary>
    /// Attempts to parse the supplied string into an <see cref="HmrcPayeReference"/> object.
    /// </summary>
    /// <param name="input">String value containing candidate full HMRC PAYE Reference.  Lower case characters are converted to
    /// upper case.</param>
    /// <param name="payeReference">Set to a new instance of HmrcPayeReference if parse succeeds; set to object default
    /// otherwise.</param>
    /// <returns>True if the string could be parsed into a valid HMRC PAYE Reference; false otherwise.</returns>
    public static bool TryParse(in string? input, [NotNullWhen(true)] out HmrcPayeReference? payeReference)
    {
        payeReference = null;

        if (string.IsNullOrEmpty(input))
            return false;

        var tidiedInput = input.ToUpperInvariant().Replace(" ", string.Empty);

        if (!IsValid(tidiedInput))
            return false;

        var tokens = tidiedInput.Split('/', 2, StringSplitOptions.RemoveEmptyEntries);

        if (tokens == null || tokens.Length != 2)
            throw new InvalidOperationException($"Unexpected parsing error in HmrcPayeReference.TryParse()");

        payeReference = new HmrcPayeReference(int.Parse(tokens[0], CultureInfo.InvariantCulture), tokens[1]);

        return true;
    }

    /// <summary>
    /// Verifies whether the supplied string could be a valid HMRC PAYE Reference.
    /// </summary>
    /// <param name="value">String value to check.</param>
    /// <returns>True if the supplied value could be a valid HMRC PAYE Reference; false otherwise.</returns>
    /// <remarks>Although this method confirms whether the string supplied <em>could</em> be a valid HMRC PAYE Reference,
    /// it does not guarantee that the supplied value is registered with HMRC against a given company.</remarks>
    public static bool IsValid(in string? value) => value != null && GetPayeRefRegex().IsMatch(value);

    /// <summary>
    /// Gets the string representation of this HmrcPayeReference.
    /// </summary>
    /// <returns>The value of this <see cref="HmrcPayeReference"/> as a string.</returns>
    public override readonly string ToString() => $"{HmrcOfficeNumber:000}/{EmployerPayeReference}";
}