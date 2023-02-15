﻿// Copyright (c) 2023 Paytools Foundation
//
// Licensed under the Apache License, Version 2.0 (the "License")~
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

using Paytools.Common.Model;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Paytools.Common.Serialization;

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