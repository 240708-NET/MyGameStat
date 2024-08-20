using Microsoft.EntityFrameworkCore;
using MyGameStat.Application.Repository;
using MyGameStat.Domain.Entity;
using MyGameStat.Infrastructure.Persistence;

namespace MyGameStat.Infrastructure.Repository;

public class UserRepository(ApplicationDbContext ctx) : QueryRepository<User, string>(ctx), IUserRepository<string>
{
    public override async Task<User?> GetById(string id)
    {
        return await dbSet
                    .Where(u => u.Id == id)
                    .Include(u => u.UserGames)
                    .SingleOrDefaultAsync();
    }

    public async Task<User?> GetByUserName(string UserName)
    {
        return await dbSet
                    .Where(u => u.UserName == UserName)
                    .Include(u => u.UserGames)
                    .SingleOrDefaultAsync();
    }
}