using MyGameStat.Domain.Common;
using MyGameStat.Domain.Entity;

namespace MyGameStat.Application.Service;

public interface IUserGameService<TEntity, ID> where TEntity : BaseEntity<ID>
{
    ICollection<TEntity> GetByUserIdAndFilter(ID? id, Status? status, string? genre, string? platformName);
    TEntity? Upsert(string? userId, TEntity entity);
    int Update(string? userId, TEntity entity);
    int Delete(ID id);
}
