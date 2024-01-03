// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Payetools.Common.Serialization;

/// <summary>
/// JSON Converter for instances of <see cref="DateOnly"/> types.
/// </summary>
public class DateOnlyJsonConverter : JsonConverter<DateOnly>
{
    private const string DateOnlyJsonFormat = "yyyy-MM-dd";

    /// <summary>
    /// Reads an ISO 8601 format date in string format and converts to the appropriate <see cref="DateOnly"/> value.
    /// </summary>
    /// <param name="reader">JSON reader (UTF-8 format).</param>
    /// <param name="typeToConvert">Type to convert (unused).</param>
    /// <param name="options">JSON serializer options (unused).</param>
    /// <returns><see cref="DateOnly"/> value.</returns>
    public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        DateOnly.ParseExact(reader.GetString() ?? string.Empty, DateOnlyJsonFormat, CultureInfo.InvariantCulture);

    /// <summary>
    /// Writes a <see cref="DateOnly"/> value to the JSON stream in ISO 8601 format date in string format.
    /// </summary>
    /// <param name="writer">JSON writer (UTF-8 format).</param>
    /// <param name="value">DateOnly value to convert.</param>
    /// <param name="options">JSON serializer options (unused).</param>
    public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options) =>
        writer.WriteStringValue(value.ToString(DateOnlyJsonFormat, CultureInfo.InvariantCulture));
}
