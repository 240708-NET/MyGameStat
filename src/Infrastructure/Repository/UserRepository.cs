using Microsoft.EntityFrameworkCore;
using MyGameStat.Application.Repository;
using MyGameStat.Domain.Entity;
using MyGameStat.Infrastructure.Persistence;

namespace MyGameStat.Infrastructure.Repository;

public class UserRepository(ApplicationDbContext ctx) : QueryRepository<User, string>(ctx), IUserRepository<string>
{
    public override User? GetById(string? id)
    {
        return dbSet
                    .Include(e => e.UserGames)
                    .SingleOrDefault(e => e.Id == id);
    }

    public User? GetByUserName(string UserName)
    {
        return dbSet
                    .Include(e => e.UserGames)
                    .SingleOrDefault(e => e.UserName == UserName);
    }
}
