namespace Alexicon.API.SecondaryPorts.DTOs;

public class GameMove
{
    public GameMove()
    {
        LettersUsed = [];
        WordsCreated = [];
    }

    public GamePlayer Player { get; set; }
    
    public short FirstLetterX { get; set; }

    public short FirstLetterY { get; set; }

    public short LastLetterX { get; set; }

    public short LastLetterY { get; set; }

    public short Score { get; set; }
    
    public IReadOnlyCollection<char> LettersUsed { get; set; }

    public IReadOnlyCollection<string> WordsCreated { get; set; }
}