namespace Alexicon.API.Models.Requests;

public class CreateGameHttpRequest
{
    public CreateGameHttpRequest()
    {
        Players = new Dictionary<string, NewPlayerDto>();
    }

    public Dictionary<string, NewPlayerDto> Players { get; set; }
}

public class NewPlayerDto
{
    public NewPlayerDto()
    {
        StartingRack = [];
    }

    public string DisplayName { get; set; }

    public List<char> StartingRack { get; set; }
}