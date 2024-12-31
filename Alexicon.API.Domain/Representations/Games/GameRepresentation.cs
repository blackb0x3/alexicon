namespace Alexicon.API.Domain.Representations.Games;

public class GameRepresentation
{
    public GameRepresentation()
    {
        Players = new Dictionary<string, GamePlayerRepresentation>();
    }
    
    public Guid Id { get; set; }

    public bool ValidateNewWords { get; set; }
    
    public GameStateRepresentation State { get; set; }

    public Dictionary<string, GamePlayerRepresentation> Players { get; set; }
}