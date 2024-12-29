namespace Alexicon.API.Domain.PrimaryPorts.ApplyMove;

public class WordCreated
{
    public string Word { get; set; }
    
    public bool IsValid { get; set; }
    
    public string Reason { get; set; }
    
    public short WordScore { get; set; }

    public List<(short, short)> NewUsedTiles { get; set; } // Track new tiles
}