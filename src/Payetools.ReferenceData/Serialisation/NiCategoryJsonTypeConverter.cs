// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Payetools.ReferenceData.Serialisation;

/// <summary>
/// JSON Converter for the <see cref="NiCategory"/> enumeration.
/// </summary>
public class NiCategoryJsonTypeConverter : JsonConverter<NiCategory>
{
    /// <summary>
    /// Reads a <see cref="NiCategory"/> enumerated value in string format and converts to the appropriate enum
    /// value.
    /// </summary>
    /// <param name="reader">JSON reader (UTF-8 format).</param>
    /// <param name="typeToConvert">Type to convert (unused).</param>
    /// <param name="options">JSON serializer options (unused).</param>
    /// <returns><see cref="PayFrequency"/> value.</returns>
    public override NiCategory Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        Enum.Parse<NiCategory>(reader.GetString() ?? string.Empty, true);

    /// <summary>
    /// Writes a <see cref="NiCategory"/> enum value, converted to its string equivalent, to the JSON stream.
    /// </summary>
    /// <param name="writer">JSON writer (UTF-8 format).</param>
    /// <param name="value">Enum value to convert.</param>
    /// <param name="options">JSON serializer options (unused).</param>
    public override void Write(Utf8JsonWriter writer, NiCategory value, JsonSerializerOptions options) =>
        writer.WriteStringValue(value.ToString());
}