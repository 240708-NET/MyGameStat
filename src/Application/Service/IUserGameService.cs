using MyGameStat.Domain.Common;

namespace MyGameStat.Application.Service;

public interface IUserGameService<TEntity, ID> where TEntity : BaseEntity<ID>
{
    ICollection<TEntity> GetByUserId(ID? userId);
    ID? Upsert(string? userId, TEntity entity);
    int Update(string? userId, TEntity entity);
    void Delete(ID id);
}
