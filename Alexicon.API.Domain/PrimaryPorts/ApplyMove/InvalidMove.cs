namespace Alexicon.API.Domain.PrimaryPorts.ApplyMove;

public class InvalidMove
{
    public InvalidMove()
    {
        WordsCreated = new List<WordCreated>();
        AttemptedLetters = new List<char>();
    }
    
    public List<WordCreated> WordsCreated { get; set; }

    public List<char> AttemptedLetters { get; set; }

    public IEnumerable<string> ValidWords => WordsCreated.Where(wc => wc.IsValid).Select(wc => wc.Word);

    public IEnumerable<string> InvalidWords => WordsCreated.Where(wc => !wc.IsValid).Select(wc => wc.Word);
}