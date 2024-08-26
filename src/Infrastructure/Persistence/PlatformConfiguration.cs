using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyGameStat.Domain.Entity;

namespace MyGameStat.Infrastructure.Persistence;

public class PlatformConfiguration : IEntityTypeConfiguration<Platform>
{
    public void Configure(EntityTypeBuilder<Platform> builder)
    {
        builder
        .Property(p => p.Id)
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
