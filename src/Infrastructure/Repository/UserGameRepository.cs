using Microsoft.EntityFrameworkCore;
using MyGameStat.Application.Repository;
using MyGameStat.Domain.Entity;
using MyGameStat.Infrastructure.Persistence;

namespace MyGameStat.Infrastructure.Repository;

public class UserGameRepository(ApplicationDbContext ctx) : Repository<UserGame, string>(ctx), IUserGameRepository
{
    public async Task<ICollection<UserGame>> GetUserGamesByUsername(string userName)
    {
        return await dbSet
                    .Where(u => u.User != null && userName.Equals(u.User.UserName))
                    .ToListAsync();
    }
}
