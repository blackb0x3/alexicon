namespace Alexicon.API.Domain.Representations.Games;

public class GamePlayerRepresentation
{
    public GamePlayerRepresentation()
    {
        CurrentRack = [];
    }

    public PlayerRepresentation Player { get; set; }

    public List<char> CurrentRack { get; set; }
    
    public short CurrentScore { get; set; }
}