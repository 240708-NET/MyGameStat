using MyGameStat.Domain.Common;

namespace MyGameStat.Application.Repository;

public interface IRepository<TEntity, ID> : IQueryRepository<TEntity, ID> where TEntity : BaseEntity<ID>
{
    ID? Save(TEntity entity);
    int Update(TEntity entity);
    void Delete(ID id);
    TEntity? Retrieve(TEntity entity);
}
