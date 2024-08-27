using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using MyGameStat.Infrastructure.Persistence;

namespace MyGameStat.Web.API.Extension;

[ExcludeFromCodeCoverageAttribute]
public static class MigrationExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();
        using ApplicationDbContext context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        context.Database.Migrate();
    }
}
