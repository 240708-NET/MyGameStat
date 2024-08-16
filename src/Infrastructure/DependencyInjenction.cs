using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyGameStat.Infrastructure.Persistence;

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

        //services.AddScoped<IUserRepository, UserRepository>();
        return services;
    }
}
