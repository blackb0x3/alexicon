using Alexicon.API.SecondaryPorts.DTOs;
using Mediator;
using OneOf;

namespace Alexicon.API.SecondaryPorts.Commands.CreateGame;

public record CreateGameCommand(
    Game game
) : ICommand<OneOf<GameSaved, GameNotSaved>>;

public class GameSaved
{
    public Guid GameId { get; set; }
}

public class GameNotSaved
{
    public string Reason { get; set; }
}