using MyGameStat.Domain.Common;
using MyGameStat.Domain.Entity;

namespace MyGameStat.Application.Service;

public interface IUserGameService<TEntity, ID> where TEntity : BaseEntity<ID>
{
    ICollection<TEntity> GetByUserIdAndStatus(ID? id, Status? status);
    ID? Upsert(string? userId, TEntity entity);
    int Update(string? userId, TEntity entity);
    void Delete(ID id);
}
