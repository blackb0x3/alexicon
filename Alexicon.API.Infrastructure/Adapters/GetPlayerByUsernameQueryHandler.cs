using Alexicon.API.Infrastructure.DataAccess;
using Alexicon.API.SecondaryPorts.DTOs;
using Alexicon.API.SecondaryPorts.Queries.GetPlayerByUsername;
using MapsterMapper;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace Alexicon.API.Infrastructure.Adapters;

public class GetPlayerByUsernameQueryHandler : IQueryHandler<GetPlayerByUsernameQuery, Player?>
{
    private readonly AlexiconContext _context;
    private readonly IMapper _mapper;

    public GetPlayerByUsernameQueryHandler(AlexiconContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async ValueTask<Player?> Handle(GetPlayerByUsernameQuery query, CancellationToken cancellationToken)
    {
        var player = await _context.Players.SingleOrDefaultAsync(p => p.Username == query.Username, cancellationToken);

        if (player is null)
        {
            return null;
        }

        return _mapper.Map<Player>(player);
    }
}