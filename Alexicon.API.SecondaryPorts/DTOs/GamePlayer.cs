namespace Alexicon.API.SecondaryPorts.DTOs;

public class GamePlayer
{
    public GamePlayer()
    {
        CurrentRack = new List<char>();
    }

    public string Username { get; set; }

    public string DisplayName { get; set; }

    public List<char> CurrentRack { get; set; }
}