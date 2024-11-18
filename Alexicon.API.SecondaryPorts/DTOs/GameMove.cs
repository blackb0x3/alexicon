namespace Alexicon.API.SecondaryPorts.DTOs;

public class GameMove
{
    public string Player { get; set; }

    public List<char> LettersUsed { get; set; }

    public List<string> WordsCreated { get; set; }
    
    public Tuple<string, string> Location { get; set; }

    public int Score { get; set; }
}