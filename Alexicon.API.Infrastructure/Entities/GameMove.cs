namespace Alexicon.API.Infrastructure.Entities;

public class GameMove : BaseEntity
{
    public Guid Id { get; set; } // Unique identifier for the move

    public Guid GameId { get; set; }

    public Guid PlayerId { get; set; }
    
    public short LocationX { get; set; }

    public short LocationY { get; set; }

    public short Score { get; set; }

    public string LettersUsedForDb { get; set; }

    public string WordsCreatedForDb { get; set; }

    public virtual Game Game { get; set; }

    public virtual GamePlayer Player { get; set; }

    public List<char> LettersUsed
    {
        get => LettersUsedForDb.Split(ForDbDelimiter).Select(c => c[0]).ToList();
        set => LettersUsedForDb = string.Join(ForDbDelimiter, value);
    }

    public List<string> WordsCreated
    {
        get => WordsCreatedForDb.Split(ForDbDelimiter).ToList();
        set => WordsCreatedForDb = string.Join(ForDbDelimiter, value);
    }
}