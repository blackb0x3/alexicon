namespace Alexicon.API.Infrastructure.Entities;

public class GamePlayer : BaseEntity
{
    public Guid Id { get; set; }

    public Guid GameId { get; set; }

    public string PlayerUsername { get; set; }

    public string CurrentRackForDb { get; set; }

    public virtual Game Game { get; set; }

    public virtual Player Player { get; set; }

    public List<char> CurrentRack
    {
        get => CurrentRackForDb.Split(ForDbDelimiter).Select(c => c[0]).ToList();
        set => CurrentRackForDb = string.Join(ForDbDelimiter, value);
    }
}