using System.Text.Json;
using System.Text.Json.Serialization;

namespace Paytools.Common.Serialization;

public class TaxYearEndingJsonConverter : JsonConverter<TaxYearEnding>
{
    public override TaxYearEnding Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        Enum.Parse<TaxYearEnding>(reader.GetString() ?? String.Empty);

    public override void Write(Utf8JsonWriter writer, TaxYearEnding taxYearEnding, JsonSerializerOptions options) =>
        writer.WriteStringValue(taxYearEnding.ToString());
}
