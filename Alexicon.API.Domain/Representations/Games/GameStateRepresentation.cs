using System.Text.Json.Serialization;
using Alexicon.API.Domain.Services.Converters;

namespace Alexicon.API.Domain.Representations.Games;

public class GameStateRepresentation
{
    [JsonConverter(typeof(ByteArrayToJsonArrayConverter))]
    public byte[,] Board { get; set; }

    public List<GameMoveRepresentation> MovesPlayed { get; set; }
    
    public List<char> Bag { get; set; }
}