using MyGameStat.Domain.Entity;

namespace MyGameStat.Application.Repository;

public interface IUserGameRepository : IRepository<UserGame, string>
{
    ICollection<UserGame> GetByUserId(string? id);
}
