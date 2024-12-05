using System.Text.Json;
using Alexicon.API.Domain.Services.Converters;

namespace Alexicon.API.Tests.Converters;

[TestFixture]
public class A_ByteArray_To_JsonArray_Converter
{
    public class Should_Deserialize_Json_To_ByteArray
    {
        [Test]
        public void When_Json_Is_Valid()
        {
            // Arrange
            var json = "[[1,2,3],[4,5,6]]";
            var converter = new ByteArrayToJsonArrayConverter();
            var options = new JsonSerializerOptions { Converters = { converter } };

            // Act
            var result = JsonSerializer.Deserialize<byte[,]>(json, options);

            // Assert
            result.Should().NotBeNull();
            result.GetLength(0).Should().Be(2);
            result.GetLength(1).Should().Be(3);
            result[0, 0].Should().Be(1);
            result[1, 2].Should().Be(6);
        }
    }

    public class Should_Serialize_ByteArray_To_Json
    {
        [Test]
        public void When_Array_Is_Valid()
        {
            // Arrange
            var array = new byte[,] { { 1, 2, 3 }, { 4, 5, 6 } };
            var converter = new ByteArrayToJsonArrayConverter();
            var options = new JsonSerializerOptions { Converters = { converter } };

            // Act
            var result = JsonSerializer.Serialize(array, options);

            // Assert
            var expectedJson = "[[1,2,3],[4,5,6]]";
            result.Should().Be(expectedJson);
        }
    }

    public class Should_Throw_Exception
    {
        [Test]
        public void When_Json_Is_Not_An_Array()
        {
            // Arrange
            var json = "{}";
            var converter = new ByteArrayToJsonArrayConverter();
            var options = new JsonSerializerOptions { Converters = { converter } };

            // Act
            var action = () => JsonSerializer.Deserialize<byte[,]>(json, options);

            // Assert
            action.Should().Throw<JsonException>();
        }

        [Test]
        public void When_Json_Contains_Inconsistent_Row_Lengths()
        {
            // Arrange
            var json = "[[1,2],[3]]";
            var converter = new ByteArrayToJsonArrayConverter();
            var options = new JsonSerializerOptions { Converters = { converter } };

            // Act
            var action = () => JsonSerializer.Deserialize<byte[,]>(json, options);

            // Assert
            action.Should().Throw<JsonException>();
        }

        [Test]
        public void When_Json_Contains_Invalid_Token()
        {
            // Arrange
            var json = "[[1,2,'invalid']]";
            var converter = new ByteArrayToJsonArrayConverter();
            var options = new JsonSerializerOptions { Converters = { converter } };

            // Act
            var action = () => JsonSerializer.Deserialize<byte[,]>(json, options);

            // Assert
            action.Should().Throw<JsonException>();
        }
    }

    public class Should_Handle_Edge_Cases
    {
        [Test]
        public void When_Json_Is_Empty_Array()
        {
            // Arrange
            var json = "[]";
            var converter = new ByteArrayToJsonArrayConverter();
            var options = new JsonSerializerOptions { Converters = { converter } };

            // Act
            var result = JsonSerializer.Deserialize<byte[,]>(json, options);

            // Assert
            result.Should().NotBeNull();
            result.GetLength(0).Should().Be(0);
            result.GetLength(1).Should().Be(0);
        }

        [Test]
        public void When_ByteArray_Is_Empty()
        {
            // Arrange
            var array = new byte[,] { };
            var converter = new ByteArrayToJsonArrayConverter();
            var options = new JsonSerializerOptions { Converters = { converter } };

            // Act
            var result = JsonSerializer.Serialize(array, options);

            // Assert
            var expectedJson = "[]";
            result.Should().Be(expectedJson);
        }
    }
}