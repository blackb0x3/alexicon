namespace Alexicon.API.SecondaryPorts.DTOs;

public class GameState
{
    public GameState()
    {
        MovesPlayed = new List<GameMove>();
    }

    public List<GameMove> MovesPlayed { get; set; }

    public static GameState NewGame => new()
    {
        MovesPlayed = new List<GameMove>()
    };
}