using Alexicon.API.Domain.Representations;
using Alexicon.API.Domain.Representations.Games;
using Mediator;
using OneOf;

namespace Alexicon.API.Domain.PrimaryPorts.CreateGame;

public record CreateGameRequest(
    Dictionary<string, NewPlayer> Players
) : IRequest<OneOf<GameRepresentation, ValidationRepresentation>>;