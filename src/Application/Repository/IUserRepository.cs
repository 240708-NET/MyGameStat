using MyGameStat.Domain.Entity;

namespace MyGameStat.Application.Repository;

public interface IUserRepository<Id> : IQueryRepository<User, Id>
{
    public Task<User?> GetByUserName(string UserName);
}
