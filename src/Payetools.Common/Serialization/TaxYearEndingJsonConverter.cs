// Copyright (c) 2023-2024, Payetools Foundation.
//
// Paytools Foundation licenses this file to you under one of the following licenses:
//
//   * GNU Affero General Public License, see https://www.gnu.org/licenses/agpl-3.0.html
//   * Paytools Commercial Use license [TBA]
//
// For further information on licensing options, see https://paytools.dev/licensing-paytools.html

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
    public override TaxYearEnding Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        Enum.Parse<TaxYearEnding>(reader.GetString() ?? string.Empty);

    /// <summary>
    /// Writes a <see cref="TaxYearEnding"/> enum value, converted to its string equivalent, to the JSON stream.
    /// </summary>
    /// <param name="writer">JSON writer (UTF-8 format).</param>
    /// <param name="value">Enum value to convert.</param>
    /// <param name="options">JSON serializer options (unused).</param>
    public override void Write(Utf8JsonWriter writer, TaxYearEnding value, JsonSerializerOptions options) =>
        writer.WriteStringValue(value.ToString());
}
