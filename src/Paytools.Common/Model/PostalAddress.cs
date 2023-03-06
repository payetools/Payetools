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