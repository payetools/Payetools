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

using Paytools.NationalInsurance;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Paytools.ReferenceData.Serialization;

/// <summary>
/// JSON Converter for instances of <see cref="NiThresholdType"/> types.
/// </summary>
public class NiThresholdTypeJsonConverter : JsonConverter<NiThresholdType>
{
    /// <summary>
    /// Reads a National Insurance threshold element in string format and converts to the appropriate <see cref="NiThresholdType"/> value.
    /// </summary>
    /// <param name="reader">JSON reader (UTF-8 format).</param>
    /// <param name="typeToConvert">Type to convert (unused).</param>
    /// <param name="options">JSON serializer options (unused).</param>
    /// <returns><see cref="NiThresholdType"/> value.</returns>
    public override NiThresholdType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var entry = reader.GetString();

        return entry?.ToLowerInvariant() switch
        {
            "lower earnings limit" => NiThresholdType.LEL,
            "primary threshold" => NiThresholdType.PT,
            "secondary threshold" => NiThresholdType.ST,
            "freeport upper secondary threshold" => NiThresholdType.FUST,
            "upper secondary threshold" => NiThresholdType.UST,
            "apprentice upper secondary threshold" => NiThresholdType.AUST,
            "veterans upper secondary threshold" => NiThresholdType.VUST,
            "upper earnings limit" => NiThresholdType.UEL,
            "directors primary threshold" => NiThresholdType.DPT,
            _ => throw new ArgumentException($"Unrecognised Ni threshold type value '{entry}'")
        };
    }

    /// <summary>
    /// Writes a <see cref="NiThresholdType"/> value to the JSON stream in long form string format, e.g., "Lower Earnings Limit".
    /// </summary>
    /// <param name="writer">JSON writer (UTF-8 format).</param>
    /// <param name="value">NiThresholdType value to convert.</param>
    /// <param name="options">JSON serializer options (unused).</param>
    public override void Write(Utf8JsonWriter writer, NiThresholdType value, JsonSerializerOptions options) =>
        writer.WriteStringValue(value.GetFullName());
}
