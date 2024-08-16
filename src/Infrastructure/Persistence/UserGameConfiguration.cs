using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyGameStat.Domain.Entity;

namespace MyGameStat.Infrastructure.Persistence;

public class UserGameConfiguration : IEntityTypeConfiguration<UserGame>
{
    public void Configure(EntityTypeBuilder<UserGame> builder)
    {
        builder
        .HasOne<Game>()
        .WithMany()
        .HasForeignKey(ug => ug.GameId);

        builder
        .HasOne<IdentityUser>()
        .WithMany()
        .HasForeignKey(ug => ug.CreatorId);

        builder
        .HasOne<IdentityUser>()
        .WithMany()
        .HasForeignKey(ug => ug.ModifierId);
    }
}