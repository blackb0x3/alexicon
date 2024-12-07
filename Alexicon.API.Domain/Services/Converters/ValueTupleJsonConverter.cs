using System.Text.Json;
using System.Text.Json.Serialization;

namespace Alexicon.API.Domain.Services.Converters;

public class ValueTupleJsonConverter<T1, T2> : JsonConverter<(T1, T2)>
{
    public override (T1, T2) Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartArray)
        {
            throw new JsonException("Expected a JSON array to deserialize into a ValueTuple.");
        }

        reader.Read(); // Read the first element
        var item1 = JsonSerializer.Deserialize<T1>(ref reader, options);

        reader.Read(); // Read the second element
        var item2 = JsonSerializer.Deserialize<T2>(ref reader, options);

        reader.Read(); // Move past the end of the array
        if (reader.TokenType != JsonTokenType.EndArray)
        {
            throw new JsonException("Expected end of JSON array.");
        }

        return (item1, item2);
    }

    public override void Write(Utf8JsonWriter writer, (T1, T2) value, JsonSerializerOptions options)
    {
        writer.WriteStartArray();
        JsonSerializer.Serialize(writer, value.Item1, options);
        JsonSerializer.Serialize(writer, value.Item2, options);
        writer.WriteEndArray();
    }
}