using MyGameStat.Domain.Entity;

namespace MyGameStat.Application.Repository;

public interface IUserRepository<Id> : IQueryRepository<User, Id>
{
    Task<User?> GetByUserName(string UserName);
}
