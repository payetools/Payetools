// Copyright (c) 2023 Paytools Foundation
//
// Licensed under the Apache License, Version 2.0 (the "License");
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

using System.Text.RegularExpressions;

namespace Paytools.Common.Model;

// From HMRC's National Insurance Manual, section NIM39110 (https://www.gov.uk/hmrc-internal-manuals/national-insurance-manual/nim39110)
//
// A NINO is made up of 2 letters, 6 numbers and a final letter.
public readonly struct NiNumber
{
    private static readonly Regex _validationRegex = 
        new Regex("^(?!BG)(?!GB)(?!NK)(?!KN)(?!TN)(?!NT)(?!ZZ)(?:[A-CEGHJ-PR-TW-Z][A-CEGHJ-NPR-TW-Z])(?:\\s*\\d\\s*){6}([A-D]|\\s)$");

    private readonly string _value;

    public static implicit operator string(NiNumber niNumber) => niNumber._value;

    public static implicit operator NiNumber(string niNumber) => new NiNumber(niNumber);

    public NiNumber(string niNumber)
    {
        if (!IsValid(niNumber))
            throw new ArgumentException("Argument is not a valid Accounts Office Reference", nameof(niNumber));

        _value = niNumber;
    }

    public static bool IsValid(string value) => _validationRegex.IsMatch(value);

    public string ToString(bool asSpacedFormat = true) => 
        asSpacedFormat ? ToSpacedFormat(_value) : _value;

    private static string ToSpacedFormat(string value) =>
        value.Length == 9 ? $"{value[..2]} {value[2..4]} {value[4..6]} {value[6..8]} {value[^1]}" :
            throw new InvalidOperationException("NI numbers must be 9 characters in length");
}