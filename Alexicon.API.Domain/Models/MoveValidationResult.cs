using Alexicon.API.Domain.PrimaryPorts.ApplyMove;

namespace Alexicon.API.Domain.Models;

public class MoveValidationResult
{
    public MoveValidationResult()
    {
        WordsCreated = new List<WordCreated>();
    }

    public List<WordCreated> WordsCreated { get; set; }

    public bool IsValid => WordsCreated.All(wc => wc.IsValid);
}