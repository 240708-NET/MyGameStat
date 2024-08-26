using Microsoft.EntityFrameworkCore;
using MyGameStat.Application.Repository;
using MyGameStat.Domain.Entity;
using MyGameStat.Infrastructure.Persistence;

namespace MyGameStat.Infrastructure.Repository;

public class GameRepository(ApplicationDbContext ctx) : Repository<Game, string>(ctx), IGameRepository
{
    public override Game? GetById(string? id)
    {
        return dbSet.SingleOrDefault(e => e.Id == id);
    }

    public ICollection<Game> GetByTitle(string title)
    {
        return [.. dbSet.Where(e => title.Equals(e.Title))];
    }

    public override Game? Retrieve(Game game)
    {
        return dbSet
               .Include(e => e.Platforms)
               .SingleOrDefault(g =>
                    g.Title.Equals(game.Title) &&
                    g.Genre.Equals(game.Genre) &&
                    g.ReleaseDate.Equals(game.ReleaseDate) &&
                    g.Developer.Equals(game.Developer) &&
                    g.Publisher.Equals(game.Publisher));
    }
}
