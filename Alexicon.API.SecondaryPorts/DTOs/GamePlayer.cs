namespace Alexicon.API.SecondaryPorts.DTOs;

public class GamePlayer
{
    public GamePlayer()
    {
        CurrentRack = [];
    }
    
    public Player Player { get; set; }

    public string Username { get; set; }

    public string DisplayName { get; set; }

    public IReadOnlyCollection<char> CurrentRack { get; set; }
}