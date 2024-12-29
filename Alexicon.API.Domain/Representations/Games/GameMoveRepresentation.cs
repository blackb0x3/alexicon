using System.Text.Json.Serialization;
using Alexicon.API.Domain.Services.Converters;

namespace Alexicon.API.Domain.Representations.Games;

public class GameMoveRepresentation
{
    public string PlayerUsername { get; set; }

    public List<char> LettersUsed { get; set; }

    public List<string> WordsCreated { get; set; }

    [JsonConverter(typeof(ValueTupleJsonConverter<string, string>))]
    public (string, string) Location { get; set; }

    public short Score { get; set; }
}