// Copyright (c) 2023 Payetools Foundation.
//
// Payetools Foundation licenses this file to you under one of the following licenses:
//
//  * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//  * Payetools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

using Payetools.Common.Model;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Payetools.Common.Serialization;

/// <summary>
/// JSON Converter for the <see cref="PayFrequency"/> enumeration.
/// </summary>
public class PayFrequencyJsonConverter : JsonConverter<PayFrequency>
{
    /// <summary>
    /// Reads a <see cref="PayFrequency"/> enumerated value in string format and converts to the appropriate enum
    /// value.
    /// </summary>
    /// <param name="reader">JSON reader (UTF-8 format).</param>
    /// <param name="typeToConvert">Type to convert (unused).</param>
    /// <param name="options">JSON serializer options (unused).</param>
    /// <returns><see cref="PayFrequency"/> value.</returns>
    public override PayFrequency Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        Enum.Parse<PayFrequency>(reader.GetString() ?? string.Empty);

    /// <summary>
    /// Writes a <see cref="PayFrequency"/> enum value, converted to its string equivalent, to the JSON stream.
    /// </summary>
    /// <param name="writer">JSON writer (UTF-8 format).</param>
    /// <param name="value">Enum value to convert.</param>
    /// <param name="options">JSON serializer options (unused).</param>
    public override void Write(Utf8JsonWriter writer, PayFrequency value, JsonSerializerOptions options) =>
        writer.WriteStringValue(value.ToString());
}