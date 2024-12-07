using System.Text.Json;
using Alexicon.API.Domain.Services.Converters;

namespace Alexicon.API.Tests.Converters;

[TestFixture]
public class A_ValueTuple_Json_Converter
{
    public class Should_Serialize_And_Deserialize_ValueTuples
    {
        [Test]
        public void When_Valid_ValueTuple_Is_Serialized()
        {
            // Arrange
            var tuple = (42, "Answer");
            var options = new JsonSerializerOptions
            {
                Converters = { new ValueTupleJsonConverter<int, string>() },
                WriteIndented = false
            };

            // Act
            var json = JsonSerializer.Serialize(tuple, options);

            // Assert
            json.Should().Be("[42,\"Answer\"]");
        }

        [Test]
        public void When_Valid_JSON_Is_Deserialized()
        {
            // Arrange
            var json = "[42,\"Answer\"]";
            var options = new JsonSerializerOptions
            {
                Converters = { new ValueTupleJsonConverter<int, string>() }
            };

            // Act
            var result = JsonSerializer.Deserialize<(int, string)>(json, options);

            // Assert
            result.Should().Be((42, "Answer"));
        }
    }

    public class Should_Handle_Edge_Cases
    {
        [Test]
        public void When_JSON_Is_Not_An_Array()
        {
            // Arrange
            var json = "{ \"Key\": 42 }";
            var options = new JsonSerializerOptions
            {
                Converters = { new ValueTupleJsonConverter<int, string>() }
            };

            // Act
            var act = () => JsonSerializer.Deserialize<(int, string)>(json, options);

            // Assert
            act.Should().Throw<JsonException>().WithMessage("Expected a JSON array to deserialize into a ValueTuple.");
        }

        [Test]
        public void When_JSON_Array_Has_Incorrect_Length()
        {
            // Arrange
            var json = "[42]";
            var options = new JsonSerializerOptions
            {
                Converters = { new ValueTupleJsonConverter<int, string>() }
            };

            // Act
            var act = () => JsonSerializer.Deserialize<(int, string)>(json, options);

            // Assert
            act.Should().Throw<JsonException>();
        }

        [Test]
        public void When_ValueTuple_Contains_Null_Values()
        {
            // Arrange
            var tuple = (42, (string?)null);
            var options = new JsonSerializerOptions
            {
                Converters = { new ValueTupleJsonConverter<int, string?>() },
                WriteIndented = false
            };

            // Act
            var json = JsonSerializer.Serialize(tuple, options);
            var result = JsonSerializer.Deserialize<(int, string?)>(json, options);

            // Assert
            json.Should().Be("[42,null]");
            result.Should().Be((42, null));
        }
    }
}