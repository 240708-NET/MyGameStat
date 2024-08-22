using MyGameStat.Domain.Entity;

namespace MyGameStat.Application.Repository;

public interface IGameRepository : IRepository<Game, string>
{
    Task<ICollection<Game>> GetGamesByTitle(string title);
}
