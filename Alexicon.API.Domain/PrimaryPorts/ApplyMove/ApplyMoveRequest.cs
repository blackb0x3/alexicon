using Alexicon.API.Domain.Representations;
using Alexicon.API.Domain.Representations.Games;
using Mediator;
using OneOf;

namespace Alexicon.API.Domain.PrimaryPorts.ApplyMove;

public record ApplyMoveRequest(
    string GameId,
    string Player,
    List<char> LettersUsed,
    Tuple<string, string> Location,
    List<char> NewRack
) : IRequest<OneOf<GameRepresentation, ValidationRepresentation, EntityNotFoundRepresentation, InvalidMove>>;


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

public class WordCreated
{
    public string Word { get; set; }
    
    public bool IsValid { get; set; }
}