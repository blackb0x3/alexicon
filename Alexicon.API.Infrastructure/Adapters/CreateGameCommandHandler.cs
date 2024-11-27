using Alexicon.API.Infrastructure.DataAccess;
using Alexicon.API.Infrastructure.Entities;
using Alexicon.API.SecondaryPorts.Commands.CreateGame;
using EntityFramework.Exceptions.Common;
using MapsterMapper;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using OneOf;

namespace Alexicon.API.Infrastructure.Adapters;

public class CreateGameCommandHandler : CommandHandlerBase, ICommandHandler<CreateGameCommand, OneOf<GameSaved, GameNotSaved>>
{
    private readonly AlexiconContext _context;
    private readonly IMapper _mapper;

    public CreateGameCommandHandler(AlexiconContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async ValueTask<OneOf<GameSaved, GameNotSaved>> Handle(CreateGameCommand command, CancellationToken cancellationToken)
    {
        var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            var gameEntity = _mapper.Map<Game>(command.Game);

            // Ensure duplicate players are not inserted
            foreach (var gamePlayer in gameEntity.Players)
            {
                var existingPlayer = await _context.Players.SingleOrDefaultAsync(p => p.Username == gamePlayer.Player.Username, cancellationToken);

                if (existingPlayer != null)
                {
                    gamePlayer.Player = existingPlayer;
                }
            }

            await _context.AddAsync(gameEntity, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            var gameDto = _mapper.Map<SecondaryPorts.DTOs.Game>(gameEntity);

            return new GameSaved
            {
                Game = gameDto
            };
        }
        catch
        {
            await HandleRollback(transaction, cancellationToken);

            throw;
        }
    }
}