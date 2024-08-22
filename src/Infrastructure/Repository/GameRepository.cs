using Microsoft.EntityFrameworkCore;
using MyGameStat.Application.Repository;
using MyGameStat.Domain.Entity;
using MyGameStat.Infrastructure.Persistence;

namespace MyGameStat.Infrastructure.Repository;

public class GameRepository(ApplicationDbContext ctx) : Repository<Game, string>(ctx), IGameRepository
{
    public override async Task<Game?> GetById(string id)
    {
        return await dbSet
                    .Where(g => g.Id == id)
                    .Include(g => g.Platforms)
                    .SingleOrDefaultAsync();
    }

    public async Task<ICollection<Game>> GetGamesByTitle(string title)
    {
        return await dbSet
                    .Where(g => title.Equals(g.Title))
                    .Include(g => g.Platforms)
                    .ToListAsync();
    }
}
