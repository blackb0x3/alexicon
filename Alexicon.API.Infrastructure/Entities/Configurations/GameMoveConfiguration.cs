using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Alexicon.API.Infrastructure.Entities.Configurations;

public class GameMoveConfiguration : IEntityTypeConfiguration<GameMove>
{
    public void Configure(EntityTypeBuilder<GameMove> builder)
    {
        // Primary key
        builder.HasKey(gm => gm.Id);

        // Foreign key relationships
        builder
            .HasOne(gm => gm.Game)
            .WithMany(g => g.MovesPlayed)
            .HasForeignKey(gm => gm.GameId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(gm => gm.Player)
            .WithMany()
            .HasForeignKey(gm => gm.PlayerId)
            .OnDelete(DeleteBehavior.Cascade);

        // Property configurations
        builder.Property(gm => gm.LettersUsedForDb).IsRequired();
        builder.Property(gm => gm.WordsCreatedForDb).IsRequired();
    }
}