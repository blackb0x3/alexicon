using Alexicon.API.Infrastructure.DataAccess;
using Alexicon.API.SecondaryPorts.DTOs;
using Alexicon.API.SecondaryPorts.Queries.GetGameById;
using MapsterMapper;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace Alexicon.API.Infrastructure.Adapters;

public class GetGameByIdQueryHandler : IQueryHandler<GetGameByIdQuery, Game?>
{
    private readonly AlexiconContext _context;
    private readonly IMapper _mapper;

    public GetGameByIdQueryHandler(AlexiconContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async ValueTask<Game?> Handle(GetGameByIdQuery query, CancellationToken cancellationToken)
    {
        var game = await _context.Games
            .AsNoTracking()
            .Include(g => g.Players)
            .ThenInclude(gp => gp.Player)
            .Include(g => g.MovesPlayed)
            .ThenInclude(gm => gm.Player)
            .ThenInclude(gp => gp.Player)
            .FirstOrDefaultAsync(g => g.Id == query.GameId, cancellationToken: cancellationToken);

        if (game is null)
        {
            return null;
        }

        return _mapper.Map<Game>(game);
    }
}