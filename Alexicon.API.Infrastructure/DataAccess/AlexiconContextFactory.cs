using Alexicon.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Alexicon.API.Infrastructure.DataAccess;

public class AlexiconContextFactory : IDbContextFactory<AlexiconContext>
{
    public AlexiconContext CreateDbContext()
    {
        var optionsBuilder = new DbContextOptionsBuilder<AlexiconContext>()
            .UseSqlite(DatabasePathHelper.GetSqliteConnectionString());
    }
}