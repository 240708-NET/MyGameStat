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
        .Property(g => g.Id)
        .ValueGeneratedOnAdd();

        builder
        .HasMany<Platform>()
        .WithMany();

        builder
        .HasOne<User>()
        .WithMany()
        .HasPrincipalKey(u => u.UserName)
        .HasForeignKey(g => g.CreatorId);

        builder
        .HasOne<User>()
        .WithMany()
        .HasPrincipalKey(u => u.UserName)
        .HasForeignKey(g => g.UpdaterId);
    }
}
