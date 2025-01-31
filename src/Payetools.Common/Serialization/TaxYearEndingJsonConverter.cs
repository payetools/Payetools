// Copyright (c) 2023-2025, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Payetools.Common.Serialization;

/// <summary>
/// JSON Converter for the <see cref="TaxYearEnding"/> enumeration.
/// </summary>
public class TaxYearEndingJsonConverter : JsonConverter<TaxYearEnding>
{
    /// <summary>
    /// Reads a <see cref="TaxYearEnding"/> enumerated value in string format and converts to the appropriate enum
    /// value.
    /// </summary>
    /// <param name="reader">JSON reader (UTF-8 format).</param>
    /// <param name="typeToConvert">Type to convert (unused).</param>
    /// <param name="options">JSON serializer options (unused).</param>
    /// <returns><see cref="TaxYearEnding"/> value.</returns>
    public override TaxYearEnding Read(
        ref Utf8JsonReader reader,
        /* in */ Type typeToConvert,
        /* in */ JsonSerializerOptions options) =>
        Enum.Parse<TaxYearEnding>(reader.GetString() ?? string.Empty);

    /// <summary>
    /// Writes a <see cref="TaxYearEnding"/> enum value, converted to its string equivalent, to the JSON stream.
    /// </summary>
    /// <param name="writer">JSON writer (UTF-8 format).</param>
    /// <param name="value">Enum value to convert.</param>
    /// <param name="options">JSON serializer options (unused).</param>
    public override void Write(
        /* in */ Utf8JsonWriter writer,
        /* in */ TaxYearEnding value,
        /* in */ JsonSerializerOptions options) =>
        writer.WriteStringValue(value.ToString());
}