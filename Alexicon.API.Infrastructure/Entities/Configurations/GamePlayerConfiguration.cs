using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Alexicon.API.Infrastructure.Entities.Configurations;

public class GamePlayerConfiguration : IEntityTypeConfiguration<GamePlayer>
{
    public void Configure(EntityTypeBuilder<GamePlayer> builder)
    {
        // Composite key using IDs
        builder.HasKey(gp => gp.Id);

        // Foreign key relationships
        builder
            .HasOne(gp => gp.Game)
            .WithMany(g => g.Players)
            .HasForeignKey(gp => gp.GameId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(gp => gp.Player)
            .WithMany()
            .HasForeignKey(gp => gp.PlayerUsername)
            .OnDelete(DeleteBehavior.Cascade);

        // Property configuration
        builder.Property(gp => gp.CurrentRackForDb).IsRequired();
    }
}