namespace Alexicon.API.SecondaryPorts.DTOs;

public class GameMove
{
    public GameMove()
    {
        LettersUsed = [];
        WordsCreated = [];
    }

    public GamePlayer Player { get; set; }
    
    public short LocationX { get; set; }

    public short LocationY { get; set; }

    public short Score { get; set; }
    
    public IReadOnlyCollection<char> LettersUsed { get; set; }

    public IReadOnlyCollection<string> WordsCreated { get; set; }
}