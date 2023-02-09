// Copyright (c) 2023 Paytools Foundation
//
// Licensed under the Apache License, Version 2.0 (the "License");
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

using System.Text.Json;
using System.Text.Json.Serialization;
using Paytools.Common.Model;

namespace Paytools.Common.Serialization;

public class PayFrequencyJsonConverter : JsonConverter<PayFrequency>
{
    public override PayFrequency Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        Enum.Parse<PayFrequency>(reader.GetString() ?? String.Empty);

    public override void Write(Utf8JsonWriter writer, PayFrequency payFrequency, JsonSerializerOptions options) =>
        writer.WriteStringValue(payFrequency.ToString());
}