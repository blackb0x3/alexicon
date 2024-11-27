using Alexicon.API.Domain.PrimaryPorts.ApplyMove;

namespace Alexicon.API.Domain.Models;

public class MoveValidationResult
{
    public MoveValidationResult()
    {
        WordsCreated = new List<WordCreated>();
    }

    public List<WordCreated> WordsCreated { get; set; }
    
    public short FirstLetterX { get; set; }
    
    public short FirstLetterY { get; set; }
    
    public short LastLetterX { get; set; }
    
    public short LastLetterY { get; set; }
    
    public short Score { get; set; }

    public bool IsValid => WordsCreated.All(wc => wc.IsValid);
}