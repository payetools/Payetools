// Copyright (c) 2023 Paytools Foundation.  All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License") ~
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System.Diagnostics.CodeAnalysis;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;

namespace Paytools.Common.Model;

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
public struct HmrcPayeReference
{
    private static readonly Regex _payeRefRegex = new Regex(@"^[0-9]{3}/[A-Z0-9]{1,10}$");

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
    public HmrcPayeReference(int hmrcOfficeNumber, string employerPayeReference)
    {
        HmrcOfficeNumber = hmrcOfficeNumber;
        EmployerPayeReference = employerPayeReference;
    }

    /// <summary>
    /// Operator for casting implicitly from a <see cref="HmrcPayeReference"/> instance to its string representation.
    /// </summary>
    /// <param name="value">An instance of HmrcPayeReference.</param>
    public static implicit operator string(HmrcPayeReference value) => value.ToString();

    /// <summary>
    /// Attempts to parse the supplied string into an <see cref="HmrcPayeReference"/> object.
    /// </summary>
    /// <param name="input">String value containing candidate full HMRC PAYE Reference.  Lower case characters are converted to
    /// upper case.</param>
    /// <param name="payeReference">Set to a new instance of HmrcPayeReference if parse succeeds; set to object default
    /// otherwise.</param>
    /// <returns>True if the string could be parsed into a valid HMRC PAYE Reference; false otherwise.</returns>
    public static bool TryParse(string? input, [NotNullWhen(true)] out HmrcPayeReference? payeReference)
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

        payeReference = new HmrcPayeReference(int.Parse(tokens[0]), tokens[1]);

        return true;
    }

    /// <summary>
    /// Verifies whether the supplied string could be a valid HMRC PAYE Reference.
    /// </summary>
    /// <param name="value">String value to check.</param>
    /// <returns>True if the supplied value could be a valid HMRC PAYE Reference; false otherwise.</returns>
    /// <remarks>Although this method confirms whether the string supplied <em>could</em> be a valid HMRC PAYE Reference,
    /// it does not guarantee that the supplied value is registered with HMRC against a given company.</remarks>
    public static bool IsValid(string? value) => value != null && _payeRefRegex.IsMatch(value);

    /// <summary>
    /// Gets the string representation of this HmrcPayeReference.
    /// </summary>
    /// <returns>The value of this <see cref="HmrcPayeReference"/> as a string.</returns>
    public override string ToString() => $"{HmrcOfficeNumber:000}/{EmployerPayeReference}";
}