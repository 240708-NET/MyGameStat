using MyGameStat.Domain.Common;

namespace MyGameStat.Application.Repository;

public interface IRepository<TEntity, Id> : IQueryRepository<TEntity, Id> where TEntity : BaseEntity<Id>
{
    Task Create(TEntity entity);

    Task<int> Update(TEntity entity);

    Task Delete(Id id);
}
