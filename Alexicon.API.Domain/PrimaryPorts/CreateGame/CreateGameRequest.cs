using Alexicon.API.Domain.Representations;
using Alexicon.API.Domain.Representations.Games;
using Mediator;
using OneOf;

namespace Alexicon.API.Domain.PrimaryPorts.CreateGame;

public record CreateGameRequest(
    List<NewPlayer> Players
) : IRequest<OneOf<GameRepresentation, ValidationRepresentation>>;