using Alexicon.API.SecondaryPorts.DTOs;
using Mediator;
using OneOf;

namespace Alexicon.API.SecondaryPorts.Commands.ApplyGameMove;

public record ApplyGameMoveCommand(
    Guid GameId,
    GameMove NewMove
) : ICommand<OneOf<ApplyMoveSuccess, ApplyMoveFailure>>;