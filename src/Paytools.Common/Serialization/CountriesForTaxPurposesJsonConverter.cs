using System.Text.Json;
using System.Text.Json.Serialization;

namespace Paytools.Common.Serialization;

public class CountriesForTaxPurposesJsonConverter : JsonConverter<CountriesForTaxPurposes>
{
    public override CountriesForTaxPurposes Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        CountriesForTaxPurposesConverter.ToEnum(reader.GetString());

    public override void Write(Utf8JsonWriter writer, CountriesForTaxPurposes countriesForTaxPurposes, JsonSerializerOptions options) =>
        writer.WriteStringValue(CountriesForTaxPurposesConverter.ToString(countriesForTaxPurposes));
}
