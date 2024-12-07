using System.Text.Json.Serialization;
using Alexicon.API.Domain.Services.Converters;

namespace Alexicon.API.Models.Requests;

public class ApplyMoveHttpRequest
{
    public string Player { get; set; }

    public List<char> LettersUsed { get; set; }

    [JsonConverter(typeof(ValueTupleJsonConverter<string, string>))]
    public (string, string) Location { get; set; }

    public List<char> NewRack { get; set; }
}