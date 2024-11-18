namespace Alexicon.API.SecondaryPorts.DTOs;

public class Game
{
    public Game()
    {
        Players = [];
    }

    public GameState State { get; set; }
    
    public List<GamePlayer> Players { get; set; }
}