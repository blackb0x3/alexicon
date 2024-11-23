using Alexicon.API.SecondaryPorts.DTOs;
using Mediator;

namespace Alexicon.API.SecondaryPorts.Queries.GetGameById;

public record GetGameByIdQuery(
    Guid GameId
) : IQuery<Game?>;