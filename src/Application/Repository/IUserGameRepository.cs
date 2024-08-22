using MyGameStat.Domain.Entity;

namespace MyGameStat.Application.Repository;

public interface IUserGameRepository : IRepository<UserGame, string>
{
    Task<ICollection<UserGame>> GetUserGamesByUsername(string userName);
}
