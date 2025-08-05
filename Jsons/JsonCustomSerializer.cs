using System.Text.Json;
using System.Text.Json.Serialization;

namespace Jsons;

public class JsonCustomSerializer : JsonConverter<Json>
{
    public override Json Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var doc = JsonDocument.ParseValue(ref reader);
        return new Json(doc.RootElement.GetRawText());
    }

    public override void Write(Utf8JsonWriter writer, Json value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}