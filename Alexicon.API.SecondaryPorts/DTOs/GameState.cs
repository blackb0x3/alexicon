namespace Alexicon.API.SecondaryPorts.DTOs;

public class GameState
{
    public GameState()
    {
        MovesPlayed = new List<GameMove>();
    }

    public byte[,] Board { get; set; }

    public List<GameMove> MovesPlayed { get; set; }

    public static GameState NewGame => new()
    {
        Board = new byte[15, 15],
        MovesPlayed = new List<GameMove>()
    };
}