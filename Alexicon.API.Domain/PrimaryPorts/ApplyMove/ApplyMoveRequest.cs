using Alexicon.API.Domain.Representations;
using Alexicon.API.Domain.Representations.Games;
using Mediator;
using OneOf;

namespace Alexicon.API.Domain.PrimaryPorts.ApplyMove;

public record ApplyMoveRequest(
    string GameId,
    string Player,
    List<char> LettersUsed,
    (string, string) Location,
    List<char> NewRack
) : IRequest<OneOf<GameRepresentation, ValidationRepresentation, EntityNotFoundRepresentation, PlayerNotInGame, InvalidMove>>;