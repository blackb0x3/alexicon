using Alexicon.API.SecondaryPorts.DTOs;
using Mediator;

namespace Alexicon.API.SecondaryPorts.Queries.GetPlayerByUsername;

public record GetPlayerByUsernameQuery(
    string Username
) : IQuery<Player?>;
