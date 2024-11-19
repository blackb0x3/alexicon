namespace Alexicon.API.SecondaryPorts.DTOs;

public class Game
{
    public Game()
    {
        Players = [];
    }
    
    public Guid Id { get; set; }

    public GameState State { get; set; }
    
    public List<GamePlayer> Players { get; set; }
}