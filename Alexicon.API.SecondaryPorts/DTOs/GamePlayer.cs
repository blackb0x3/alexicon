namespace Alexicon.API.SecondaryPorts.DTOs;

public class GamePlayer
{
    public GamePlayer()
    {
        CurrentRack = [];
    }

    public Player Player { get; set; }

    public List<char> CurrentRack { get; set; }
}