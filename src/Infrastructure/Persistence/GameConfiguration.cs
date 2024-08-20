using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyGameStat.Domain.Entity;

namespace MyGameStat.Infrastructure.Persistence;

public class GameConfiguration : IEntityTypeConfiguration<Game>
{
    public void Configure(EntityTypeBuilder<Game> builder)
    {
        builder.Ignore(g => g.Platforms);

        builder
        .HasMany<Platform>()
        .WithMany();

        builder
        .HasOne<User>()
        .WithMany()
        .HasForeignKey(g => g.CreatorId);

        builder
        .HasOne<User>()
        .WithMany()
        .HasForeignKey(g => g.ModifierId);
    }
}
