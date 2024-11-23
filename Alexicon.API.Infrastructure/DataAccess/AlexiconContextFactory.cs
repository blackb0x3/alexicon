using Alexicon.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Alexicon.API.Infrastructure.DataAccess;

public class AlexiconContextFactory : IDesignTimeDbContextFactory<AlexiconContext>
{
    public AlexiconContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AlexiconContext>()
            .UseSqlite(DatabasePathHelper.GetSqliteConnectionString());

        return new AlexiconContext(optionsBuilder.Options);
    }
}