using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MyGameStat.Infrastructure.Persistence;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<IdentityUser>(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Removing prefix 'AspNet' and suffix 's' from identity table names.
        // Setting schema name to 'id' for identity tables.
        builder
            .Entity<IdentityUser>(e => e.ToTable(name: "User", schema: "id"))
            .Entity<IdentityRole>(e => e.ToTable(name: "Role", schema: "id"))
            .Entity<IdentityUserClaim<string>>(e => e.ToTable(name: "UserClaim", schema: "id"))
            .Entity<IdentityUserLogin<string>>(e => e.ToTable(name: "UserLogin", schema: "id"))
            .Entity<IdentityUserRole<string>>(e => e.ToTable(name: "UserRole", schema: "id"))
            .Entity<IdentityUserToken<string>>(e => e.ToTable(name: "UserToken", schema: "id"))
            .Entity<IdentityRoleClaim<string>>(e => e.ToTable(name: "RoleClaim", schema: "id"));

       builder
            .ApplyConfiguration(new GameConfiguration())
            .ApplyConfiguration(new PlatformConfiguration())
            .ApplyConfiguration(new UserGameConfiguration());
    }
}