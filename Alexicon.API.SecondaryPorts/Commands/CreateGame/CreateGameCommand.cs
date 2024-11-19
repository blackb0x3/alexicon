using Alexicon.API.SecondaryPorts.DTOs;
using Mediator;
using OneOf;

namespace Alexicon.API.SecondaryPorts.Commands.CreateGame;

public record CreateGameCommand(
    Game Game
) : ICommand<OneOf<GameSaved, GameNotSaved>>;

public class GameSaved
{
    public Game Game { get; set; }
}

public class GameNotSaved
{
    public string Reason { get; set; }
}