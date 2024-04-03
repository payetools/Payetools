// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

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
    public override PayFrequency Read(
        ref Utf8JsonReader reader,
        /* in */ Type typeToConvert,
        /* in */ JsonSerializerOptions options) =>
        Enum.Parse<PayFrequency>(reader.GetString() ?? string.Empty);

    /// <summary>
    /// Writes a <see cref="PayFrequency"/> enum value, converted to its string equivalent, to the JSON stream.
    /// </summary>
    /// <param name="writer">JSON writer (UTF-8 format).</param>
    /// <param name="value">Enum value to convert.</param>
    /// <param name="options">JSON serializer options (unused).</param>
    public override void Write(
        /* in */ Utf8JsonWriter writer,
        /* in */ PayFrequency value,
        /* in */ JsonSerializerOptions options) =>
        writer.WriteStringValue(value.ToString());
}