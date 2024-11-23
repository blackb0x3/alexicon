namespace Alexicon.API.Domain.Representations.Games;

public class GameStateRepresentation
{
    public byte[,] Board { get; set; }

    public List<GameMoveRepresentation> MovesPlayed { get; set; }
    
    public List<char> Bag { get; set; }
}