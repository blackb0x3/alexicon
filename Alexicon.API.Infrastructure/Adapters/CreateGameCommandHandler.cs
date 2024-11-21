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

public class CreateGameCommandHandler : ICommandHandler<CreateGameCommand, OneOf<GameSaved, GameNotSaved>>
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

            await _context.AddAsync(gameEntity, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            var gameDto = _mapper.Map<SecondaryPorts.DTOs.Game>(gameEntity);

            return new GameSaved
            {
                Game = gameDto
            };
        }
        catch (UniqueConstraintException uce)
        {
            await HandleRollback(transaction, cancellationToken);

            throw;
        }
    }

    private async Task HandleRollback(IDbContextTransaction transaction, CancellationToken cancellationToken)
    {
        await transaction.RollbackAsync(cancellationToken);
    }
}