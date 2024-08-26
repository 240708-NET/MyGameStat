using Microsoft.EntityFrameworkCore;
using MyGameStat.Application.Repository;
using MyGameStat.Domain.Entity;
using MyGameStat.Infrastructure.Persistence;

namespace MyGameStat.Infrastructure.Repository;

public class UserGameRepository(ApplicationDbContext ctx) : Repository<UserGame, string>(ctx), IUserGameRepository
{
    public override UserGame? GetById(string? id)
    {
        return dbSet
                     .Include(u => u.Game)
                     .Include(u => u.Platform)
                     .Include(u => u.User)
                     .SingleOrDefault(e => id != null && id.Equals(e.Id));
    }
    public override UserGame? Retrieve(UserGame userGame)
    {
        return dbSet
                .Include(u => u.Game)
                .Include(u => u.Platform)
                .SingleOrDefault(e =>
                    e.CreatorId.Equals(userGame.CreatorId) &&
                    e.Game.Id != null && e.Game.Id.Equals(userGame.Game.Id) &&
                    e.Platform.Id != null && e.Platform.Id.Equals(userGame.Platform.Id));
    }

    public ICollection<UserGame> GetByUserIdAndStatus(string? id, Status? status)
    {
        return [.. dbSet
                    .Where(e => id != null && id.Equals(e.CreatorId))
                    .Where(e => status == null || status == e.Status)
                    .Include(e => e.Game)
                    .Include(e => e.Platform)
                    .Include(e => e.User)];
    }
}
