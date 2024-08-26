using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyGameStat.Domain.Entity;

namespace MyGameStat.Infrastructure.Persistence;

public class UserGameConfiguration : IEntityTypeConfiguration<UserGame>
{
    public void Configure(EntityTypeBuilder<UserGame> builder)
    {
        builder
        .Property(e => e.Id)
        .ValueGeneratedOnAdd();

        builder
        .HasOne(e => e.User)
        .WithMany(e => e.UserGames)
        .HasForeignKey(e => e.CreatorId);

        builder
        .HasOne<User>()
        .WithMany()
        .HasForeignKey(e => e.UpdaterId)
        .OnDelete(DeleteBehavior.NoAction);
    }
}
