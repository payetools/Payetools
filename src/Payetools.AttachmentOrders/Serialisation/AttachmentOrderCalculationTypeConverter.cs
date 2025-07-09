// Copyright (c) 2023-2025, Payetools Ltd.
//
// Payetools Ltd licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.AttachmentOrders.Model;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Payetools.AttachmentOrders.Serialisation;

/// <summary>
/// JSON Converter for the <see cref="AttachmentOrderCalculationTraits"/> enumeration.
/// </summary>
public class AttachmentOrderCalculationTypeConverter : JsonConverter<AttachmentOrderCalculationTraits>
{
    /// <summary>
    /// Reads a <see cref="AttachmentOrderCalculationTraits"/> enumerated value in string format and converts
    /// to the appropriate enum value.
    /// </summary>
    /// <param name="reader">JSON reader (UTF-8 format).</param>
    /// <param name="typeToConvert">Type to convert (unused).</param>
    /// <param name="options">JSON serializer options (unused).</param>
    /// <returns><see cref="AttachmentOrderCalculationTraits"/> value.</returns>
    public override AttachmentOrderCalculationTraits Read(
        ref Utf8JsonReader reader,
        /* in */ Type typeToConvert,
        /* in */ JsonSerializerOptions options) =>
        Enum.Parse<AttachmentOrderCalculationTraits>(reader.GetString() ?? string.Empty, true);

    /// <summary>
    /// Writes a <see cref="AttachmentOrderCalculationTraits"/> enum value, converted to its string equivalent,
    /// to the JSON stream.
    /// </summary>
    /// <param name="writer">JSON writer (UTF-8 format).</param>
    /// <param name="value">Enum value to convert.</param>
    /// <param name="options">JSON serializer options (unused).</param>
    public override void Write(
        /* in */ Utf8JsonWriter writer,
        /* in */ AttachmentOrderCalculationTraits value,
        /* in */ JsonSerializerOptions options) =>
        writer.WriteStringValue(value.ToString());
}