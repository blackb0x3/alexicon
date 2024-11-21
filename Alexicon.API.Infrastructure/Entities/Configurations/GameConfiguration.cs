using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Alexicon.API.Infrastructure.Entities;

namespace Alexicon.API.Infrastructure.Entities.Configurations;

public class GameConfiguration : IEntityTypeConfiguration<Game>
{
    public void Configure(EntityTypeBuilder<Game> builder)
    {
        builder.HasKey(g => g.Id);

        builder
            .HasMany(g => g.Players)
            .WithOne(gp => gp.Game)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(g => g.MovesPlayed)
            .WithOne(gm => gm.Game)
            .OnDelete(DeleteBehavior.Cascade);
    }
}