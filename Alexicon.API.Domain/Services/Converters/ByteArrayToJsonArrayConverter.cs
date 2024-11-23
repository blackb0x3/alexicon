using System.Text.Json;
using System.Text.Json.Serialization;

namespace Alexicon.API.Domain.Services.Converters;

public class ByteArrayToJsonArrayConverter : JsonConverter<byte[,]>
{
    public override byte[,] Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartArray)
        {
            throw new JsonException("Expected StartArray token.");
        }

        var rows = new List<byte[]>();

        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndArray)
            {
                break;
            }

            if (reader.TokenType != JsonTokenType.StartArray)
            {
                throw new JsonException("Expected StartArray token for row.");
            }

            var row = new List<byte>();
            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndArray)
                {
                    break;
                }

                if (reader.TokenType != JsonTokenType.Number || !reader.TryGetByte(out var value))
                {
                    throw new JsonException("Expected byte value.");
                }

                row.Add(value);
            }

            rows.Add(row.ToArray());
        }

        // Determine the dimensions of the resulting 2D array
        var rowCount = rows.Count;
        var columnCount = rowCount > 0 ? rows[0].Length : 0;

        var result = new byte[rowCount, columnCount];
        for (var i = 0; i < rowCount; i++)
        {
            if (rows[i].Length != columnCount)
            {
                throw new JsonException("All rows must have the same number of columns.");
            }

            for (var j = 0; j < columnCount; j++)
            {
                result[i, j] = rows[i][j];
            }
        }

        return result;
    }

    public override void Write(Utf8JsonWriter writer, byte[,] value, JsonSerializerOptions options)
    {
        writer.WriteStartArray();

        int rows = value.GetLength(0);
        int columns = value.GetLength(1);

        for (int i = 0; i < rows; i++)
        {
            writer.WriteStartArray();
            for (int j = 0; j < columns; j++)
            {
                writer.WriteNumberValue(value[i, j]);
            }
            writer.WriteEndArray();
        }

        writer.WriteEndArray();
    }
}