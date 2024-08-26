using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyGameStat.Domain.Entity;

namespace MyGameStat.Infrastructure.Persistence;

public class GameConfiguration : IEntityTypeConfiguration<Game>
{
    public void Configure(EntityTypeBuilder<Game> builder)
    {
        builder
        .Property(e => e.Id)
        .ValueGeneratedOnAdd();

        builder
        .HasOne<User>()
        .WithMany()
        .HasForeignKey(e => e.CreatorId)
        .OnDelete(DeleteBehavior.NoAction);

        builder
        .HasOne<User>()
        .WithMany()
        .HasForeignKey(e => e.UpdaterId)
        .OnDelete(DeleteBehavior.NoAction);
    }
}
