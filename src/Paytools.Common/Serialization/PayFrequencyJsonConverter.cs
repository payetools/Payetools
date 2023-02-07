using System.Text.Json;
using System.Text.Json.Serialization;

namespace Paytools.Common.Serialization;

public class PayFrequencyJsonConverter : JsonConverter<PayFrequency>
{
    public override PayFrequency Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        Enum.Parse<PayFrequency>(reader.GetString() ?? String.Empty);

    public override void Write(Utf8JsonWriter writer, PayFrequency payFrequency, JsonSerializerOptions options) =>
        writer.WriteStringValue(payFrequency.ToString());
}