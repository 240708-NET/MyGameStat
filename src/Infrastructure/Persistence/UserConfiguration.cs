using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyGameStat.Domain.Entity;

namespace MyGameStat.Infrastructure.Persistence;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasMany<Game>()
        .WithMany()
        .UsingEntity<UserGame>(
            l => l.HasOne(e => e.Game).WithMany().HasForeignKey(e => e.GameId),
            r => r.HasOne(e => e.User).WithMany().HasForeignKey(e => e.CreatorId)
        );
    }
}
