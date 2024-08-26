using MyGameStat.Domain.Entity;

namespace MyGameStat.Application.Repository;

public interface IGameRepository : IRepository<Game, string>
{
    ICollection<Game> GetByTitle(string title);
}
