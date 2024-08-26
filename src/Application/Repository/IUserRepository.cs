using MyGameStat.Domain.Entity;

namespace MyGameStat.Application.Repository;

public interface IUserRepository<Id> : IQueryRepository<User, Id>
{
    User? GetByUserName(string UserName);
}
