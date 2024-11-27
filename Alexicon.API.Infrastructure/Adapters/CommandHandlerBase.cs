using Microsoft.EntityFrameworkCore.Storage;

namespace Alexicon.API.Infrastructure.Adapters;

public abstract class CommandHandlerBase
{
    protected static async Task HandleRollback(IDbContextTransaction transaction, CancellationToken cancellationToken)
    {
        await transaction.RollbackAsync(cancellationToken);
    }
}