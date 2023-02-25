// Copyright (c) 2023 Paytools Foundation.
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

namespace Paytools.Common.Model;

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
