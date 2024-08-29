using MyGameStat.Domain.Common;

namespace MyGameStat.Application.Repository;

public interface IRepository<TEntity, ID> : IQueryRepository<TEntity, ID> where TEntity : BaseEntity<ID>
{
    TEntity? Save(TEntity entity);
    int Update(TEntity entity);
    int Delete(ID id);
    TEntity? Retrieve(TEntity entity);
}
