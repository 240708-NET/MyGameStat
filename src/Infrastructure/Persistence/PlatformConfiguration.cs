using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyGameStat.Domain.Entity;

namespace MyGameStat.Infrastructure.Persistence;

public class PlatformConfiguration : IEntityTypeConfiguration<Platform>
{
    public void Configure(EntityTypeBuilder<Platform> builder)
    {
        builder.Ignore(p => p.Games);

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
