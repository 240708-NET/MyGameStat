using MyGameStat.Domain.Entity;

namespace MyGameStat.Application.Repository;

public interface IUserGameRepository : IRepository<UserGame, string>
{
    ICollection<UserGame> GetByUserIdAndFilter(string? id, Status? status, string? genre, string? platformName);
}
