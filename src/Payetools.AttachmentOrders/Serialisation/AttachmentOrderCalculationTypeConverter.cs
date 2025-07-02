// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.AttachmentOrders.Model;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Payetools.AttachmentOrders.Serialisation;

/// <summary>
/// JSON Converter for the <see cref="AttachmentOrderCalculationBehaviours"/> enumeration.
/// </summary>
public class AttachmentOrderCalculationTypeConverter : JsonConverter<AttachmentOrderCalculationBehaviours>
{
    /// <summary>
    /// Reads a <see cref="AttachmentOrderCalculationBehaviours"/> enumerated value in string format and converts
    /// to the appropriate enum value.
    /// </summary>
    /// <param name="reader">JSON reader (UTF-8 format).</param>
    /// <param name="typeToConvert">Type to convert (unused).</param>
    /// <param name="options">JSON serializer options (unused).</param>
    /// <returns><see cref="AttachmentOrderCalculationBehaviours"/> value.</returns>
    public override AttachmentOrderCalculationBehaviours Read(
        ref Utf8JsonReader reader,
        /* in */ Type typeToConvert,
        /* in */ JsonSerializerOptions options) =>
        Enum.Parse<AttachmentOrderCalculationBehaviours>(reader.GetString() ?? string.Empty, true);

    /// <summary>
    /// Writes a <see cref="AttachmentOrderCalculationBehaviours"/> enum value, converted to its string equivalent,
    /// to the JSON stream.
    /// </summary>
    /// <param name="writer">JSON writer (UTF-8 format).</param>
    /// <param name="value">Enum value to convert.</param>
    /// <param name="options">JSON serializer options (unused).</param>
    public override void Write(
        /* in */ Utf8JsonWriter writer,
        /* in */ AttachmentOrderCalculationBehaviours value,
        /* in */ JsonSerializerOptions options) =>
        writer.WriteStringValue(value.ToString());
}