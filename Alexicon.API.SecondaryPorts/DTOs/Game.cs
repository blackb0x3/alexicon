namespace Alexicon.API.SecondaryPorts.DTOs;

public class Game
{
    public Game()
    {
        Players = [];
        MovesPlayed = [];
    }
    
    public Guid Id { get; set; }

    public bool ValidateNewWords { get; set; }

    public IReadOnlyCollection<GamePlayer> Players { get; set; }

    public IReadOnlyCollection<GameMove> MovesPlayed { get; set; }
}