namespace Alexicon.API.Infrastructure.Entities;

public class Game : BaseEntity
{
    public Game()
    {
        Players = new List<GamePlayer>();
        MovesPlayed = new List<GameMove>();
    }

    public Guid Id { get; set; }

    public bool ValidateNewWords { get; set; }

    public virtual ICollection<GamePlayer> Players { get; set; }

    public virtual ICollection<GameMove> MovesPlayed { get; set; }
}