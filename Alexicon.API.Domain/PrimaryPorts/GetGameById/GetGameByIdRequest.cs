using Alexicon.API.Domain.Representations;
using Alexicon.API.Domain.Representations.Games;
using Mediator;
using OneOf;

namespace Alexicon.API.Domain.PrimaryPorts.GetGameById;

public record GetGameByIdRequest(
    string GameId
) : IRequest<OneOf<GameRepresentation, ValidationRepresentation, EntityNotFoundRepresentation>>;