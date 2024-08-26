using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyGameStat.Application.Repository;
using MyGameStat.Application.Service;
using MyGameStat.Domain.Entity;
using MyGameStat.Infrastructure.Persistence;
using MyGameStat.Infrastructure.Repository;

namespace MyGameStat.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options
            .UseSqlServer(
                configuration.GetConnectionString("Database"),
                // TODO: Need to get the migration assembly dynamically.
                // b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)
                b => b.MigrationsAssembly("MyGameStat.API")
            )
        );

        services.AddScoped<IUserRepository<string>, UserRepository>();
        services.AddScoped<IUserGameRepository, UserGameRepository>();
        services.AddScoped<IGameRepository, GameRepository>();
        services.AddScoped<IPlatformRepository, PlatformRepository>();
        services.AddScoped<IUserGameService<UserGame, string>, UserGameService>();
        return services;
    }
}
