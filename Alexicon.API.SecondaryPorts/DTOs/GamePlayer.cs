namespace Alexicon.API.SecondaryPorts.DTOs;

public class GamePlayer
{
    public GamePlayer()
    {
        CurrentRack = [];
    }
    
    public Player Player { get; set; }

    public IReadOnlyCollection<char> CurrentRack { get; set; }
}