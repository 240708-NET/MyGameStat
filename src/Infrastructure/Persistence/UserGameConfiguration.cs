using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyGameStat.Domain.Entity;

namespace MyGameStat.Infrastructure.Persistence;

public class UserGameConfiguration : IEntityTypeConfiguration<UserGame>
{
    public void Configure(EntityTypeBuilder<UserGame> builder)
    {
        builder.Ignore(u => u.Game);

        builder
        .Property(u => u.Id)
        .ValueGeneratedOnAdd();

        builder
        .HasOne<User>()
        .WithMany()
        .HasPrincipalKey(u => u.UserName)
        .HasForeignKey(g => g.CreatorId);
    }
}
