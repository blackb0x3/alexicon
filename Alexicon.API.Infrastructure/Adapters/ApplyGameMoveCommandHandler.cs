using Alexicon.API.Infrastructure.DataAccess;
using Alexicon.API.Infrastructure.Entities;
using Alexicon.API.SecondaryPorts.Commands.ApplyGameMove;
using MapsterMapper;
using Mediator;
using Microsoft.EntityFrameworkCore;
using OneOf;

namespace Alexicon.API.Infrastructure.Adapters;

public class ApplyGameMoveCommandHandler : CommandHandlerBase, ICommandHandler<ApplyGameMoveCommand, OneOf<ApplyMoveSuccess, ApplyMoveFailure>>
{
    private readonly AlexiconContext _context;
    private readonly IMapper _mapper;

    public ApplyGameMoveCommandHandler(AlexiconContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async ValueTask<OneOf<ApplyMoveSuccess, ApplyMoveFailure>> Handle(ApplyGameMoveCommand command, CancellationToken cancellationToken)
    {
        var game = await _context.Games.SingleAsync(g => g.Id == command.GameId, cancellationToken);
        var gamePlayer = await _context.GamePlayers.SingleAsync(gp => gp.GameId == command.GameId && gp.PlayerUsername == command.NewMove.Player.Player.Username, cancellationToken);
        var mappedGameMove = _mapper.Map<GameMove>(command.NewMove);
        mappedGameMove.Player = gamePlayer;

        var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            game.MovesPlayed.Add(mappedGameMove);

            await _context.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            return new ApplyMoveSuccess();
        }
        catch (Exception e)
        {
            await HandleRollback(transaction, cancellationToken);

            return new ApplyMoveFailure { Reason = e.Message };
        }
    }
}