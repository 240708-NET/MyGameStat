using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyGameStat.Domain.Entity;

namespace MyGameStat.Infrastructure.Persistence;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
        .HasMany<Game>()
        .WithMany()
        .UsingEntity<UserGame>(
            l => l.HasOne<Game>().WithMany().HasForeignKey(g => g.GameId),
            r => r.HasOne<User>().WithMany().HasPrincipalKey(u => u.UserName).HasForeignKey(ug => ug.CreatorId)
        );
    }
}
