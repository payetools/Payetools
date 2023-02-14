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

using System.Text.Json;
using System.Text.Json.Serialization;
using Paytools.Common.Model;

namespace Paytools.Common.Serialization;

/// <summary>
/// JSON Converter for the <see cref="CountriesForTaxPurposes"/> enumeration.  When encoded in a JSON payload,
/// values are represented using space separated ISO-3166 format, e.g. "GB-ENG GB-NIR".
/// </summary>
public class CountriesForTaxPurposesJsonConverter : JsonConverter<CountriesForTaxPurposes>
{
    /// <summary>
    /// Reads a <see cref="CountriesForTaxPurposes"/> value in ISO-3166 format and converts to the appropriate enum
    /// value(s).
    /// </summary>
    /// <param name="reader">JSON reader (UTF-8 format).</param>
    /// <param name="typeToConvert">Type to convert (unused).</param>
    /// <param name="options">JSON serializer options (unused).</param>
    /// <returns><see cref="CountriesForTaxPurposes"/> value.</returns>
    public override CountriesForTaxPurposes Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        CountriesForTaxPurposesConverter.ToEnum(reader.GetString());

    /// <summary>
    /// Writes a <see cref="CountriesForTaxPurposes"/> enum value or values (as [Flags attribute is present), converted to the
    /// appropriate ISO-3166 space separated string format, e.g., "GB-ENG GB-WLS GB-NIR".
    /// </summary>
    /// <param name="writer">JSON writer (UTF-8 format).</param>
    /// <param name="countriesForTaxPurposes">Enum value(s) to convert.</param>
    /// <param name="options">JSON serializer options (unused).</param>
    public override void Write(Utf8JsonWriter writer, CountriesForTaxPurposes countriesForTaxPurposes, JsonSerializerOptions options) =>
        writer.WriteStringValue(CountriesForTaxPurposesConverter.ToString(countriesForTaxPurposes));
}