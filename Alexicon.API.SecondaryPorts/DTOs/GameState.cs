namespace Alexicon.API.SecondaryPorts.DTOs;

public class GameState
{
    public GameState()
    {
        MovesPlayed = new List<GameMove>();
        RemainingTiles = new List<char>();
    }

    public byte[,] Board { get; set; }

    public List<GameMove> MovesPlayed { get; set; }

    public List<char> RemainingTiles { get; set; }
}