namespace MyGameStat.Application.Repository;

public interface IQueryRepository<TEntity, Id> where TEntity : class
{
    TEntity? GetById(Id? id);
    ICollection<TEntity> GetAll();
}
