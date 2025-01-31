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
    public override CountriesForTaxPurposes Read(
        ref Utf8JsonReader reader,
        /* in */ Type typeToConvert,
        /* in */ JsonSerializerOptions options) =>
        CountriesForTaxPurposesConverter.ToEnum(reader.GetString());

    /// <summary>
    /// Writes a <see cref="CountriesForTaxPurposes"/> enum value or values (as [Flags attribute is present), converted to the
    /// appropriate ISO-3166 space separated string format, e.g., "GB-ENG GB-WLS GB-NIR".
    /// </summary>
    /// <param name="writer">JSON writer (UTF-8 format).</param>
    /// <param name="value">Enum value(s) to convert.</param>
    /// <param name="options">JSON serializer options (unused).</param>
    public override void Write(
        /* in */ Utf8JsonWriter writer,
        /* in */ CountriesForTaxPurposes value,
        /* in */ JsonSerializerOptions options) =>
        writer.WriteStringValue(CountriesForTaxPurposesConverter.ToString(value));
}