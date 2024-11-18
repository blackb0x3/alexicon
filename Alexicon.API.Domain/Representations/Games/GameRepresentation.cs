namespace Alexicon.API.Domain.Representations.Games;

public class GameRepresentation
{
    public GameRepresentation()
    {
        Players = new Dictionary<string, GamePlayerRepresentation>();
    }

    public Dictionary<string, GamePlayerRepresentation> Players { get; set; }
}

public class GamePlayerRepresentation
{
    public GamePlayerRepresentation()
    {
        CurrentRack = [];
    }

    public string DisplayName { get; set; }

    public List<char> CurrentRack { get; set; }
}