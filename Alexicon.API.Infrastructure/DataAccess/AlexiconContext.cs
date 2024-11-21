using Alexicon.API.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Alexicon.API.Infrastructure.DataAccess;

public class AlexiconContext : DbContext
{
    public AlexiconContext(DbContextOptions<AlexiconContext> options) : base(options) { }

    public DbSet<Game> Games { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Apply all IEntityTypeConfiguration implementations automatically
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AlexiconContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var entries = ChangeTracker.Entries<BaseEntity>();
        var utcNow = DateTime.UtcNow;

        foreach (var entry in entries)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.Created = utcNow;
                    entry.Entity.LastUpdated = utcNow;
                    break;
                case EntityState.Modified:
                    entry.Entity.LastUpdated = utcNow;
                    break;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}